using System;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Threading;
using WinSysInfo.ReflectionHelper.Factory;
using WinSysInfo.ReflectionHelper.Model;
using WinSysInfo.ReflectionHelper.CAttr;
using System.Collections.Generic;

namespace WinSysInfo.ReflectionHelper.Process
{
    public class RuntimeAssemblyProcess
    {
        #region Properties

        /// <summary>
        /// Use this as a referece to the Xml data containing assembly information
        /// </summary>
        public XmlModelAssemblyRoot XmlAssemblyData { get; protected set; }

        /// <summary>
        /// Builds up the Assembly
        /// </summary>
        protected AssemblyBuilder RTAssemblyBuilder { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Create object with the Xml data as reference
        /// </summary>
        /// <param name="xmlData"></param>
        public RuntimeAssemblyProcess(XmlModelAssemblyRoot xmlData)
        {
            this.XmlAssemblyData = xmlData;
        }

        /// <summary>
        /// Create object using the Assembly Xml file name
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <param name="pathType"></param>
        public RuntimeAssemblyProcess(string xmlFile, EnumXmlFilePathType pathType)
        {
            LoadAssemblyXmlData(ConstructXmlFilePath(xmlFile, pathType));
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Load the xml data
        /// </summary>
        /// <param name="filePath"></param>
        private void LoadAssemblyXmlData(string filePath)
        {
            if (File.Exists(filePath) == false)
                throw new FileNotFoundException(filePath + " not found");

            XmlSerializerProcess serialize = new XmlSerializerProcess(filePath);
            this.XmlAssemblyData = serialize.ReadDataStructure<XmlModelAssemblyRoot>();
        }

        /// <summary>
        /// Create the Xml file full path
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <param name="pathType"></param>
        /// <returns></returns>
        private string ConstructXmlFilePath(string xmlFile, EnumXmlFilePathType pathType)
        {
            // Get application path in both ASp.NET or console application
            Uri uri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string currentAppPath = Path.GetDirectoryName(uri.LocalPath);

            // We want the Model
            string filePath = Path.Combine(currentAppPath, @"App_Data\Models");

            // Get the folder name from the Model type
            filePath = Path.Combine(filePath, FactoryFolderName.GetFolderName(pathType));
            filePath = Path.Combine(filePath, xmlFile);

            return filePath;
        }

        /// <summary>
        /// Create the runtime assembly
        /// </summary>
        public void Create()
        {
            CreateRTAssemblyAndModule();
        }

        /// <summary>
        /// Internal Assmebly runtime creation
        /// </summary>
        private void CreateRTAssemblyAndModule()
        {
            AssemblyName assemblyName = new AssemblyName(this.XmlAssemblyData.FileName);
            Version outVersion;
            if (Version.TryParse(this.XmlAssemblyData.Version, out outVersion) == false)
                throw new InvalidDataException("unable to parse the Assembly Version data");
            assemblyName.Version = outVersion;

            RTAssemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);

            // For a single-module assembly, the module name is usually
            // the assembly name plus an extension.
            ModuleBuilder moduleBuilder = RTAssemblyBuilder.DefineDynamicModule(this.XmlAssemblyData.FileName + ".dll");

            // foreach namespace create type
            foreach(XmlModelNamespaceRoot xmlNamespace in this.XmlAssemblyData.Namespaces)
            {
                CreateRTNamespaceType(moduleBuilder, xmlNamespace);
            }

            RTAssemblyBuilder.Save(this.XmlAssemblyData.FileName + ".dll");
        }

        /// <summary>
        /// Runtime type builder
        /// </summary>
        /// <param name="moduleBuilder"></param>
        /// <param name="xmlNamespace"></param>
        /// <returns></returns>
        private void CreateRTNamespaceType(ModuleBuilder moduleBuilder, XmlModelNamespaceRoot xmlNamespace)
        {
            // Foreach enum types
            foreach (XmlEnumLayout xmlEnum in xmlNamespace.Enums)
            {
                EnumBuilder typeBuilder = CreateRTEnumType(moduleBuilder, xmlNamespace, xmlEnum);
                var type = typeBuilder.CreateType();
                if (type.IsValueType == false)
                    throw new TypeAccessException("Some issue with creating value type");
            }

            // Foreach structure types
            foreach(XmlStructLayoutRoot xmlStruct in xmlNamespace.Structs)
            {
                TypeBuilder typeBuilder = CreateRTStructType(moduleBuilder, xmlNamespace, xmlStruct);
                var type = typeBuilder.CreateType();
                if (type.IsValueType == false)
                    throw new TypeAccessException("Some issue with creating value type");
            }
        }

        /// <summary>
        /// Runtime enum type builder
        /// </summary>
        /// <param name="moduleBuilder"></param>
        /// <param name="xmlNamespace"></param>
        /// <param name="xmlEnum"></param>
        /// <returns></returns>
        private EnumBuilder CreateRTEnumType(ModuleBuilder moduleBuilder, XmlModelNamespaceRoot xmlNamespace, XmlEnumLayout xmlEnum)
        {
            Type enumType = FactoryStandardType.Get(xmlEnum.NetType);
            EnumBuilder enumBuilder = moduleBuilder.DefineEnum(string.Format("{0}.{1}", xmlNamespace.Namespace, xmlEnum.Name),
                        TypeAttributes.Public, enumType);

            // Foreach field
            foreach (XmlEnumKeyValueLayout xmlkeyValue in xmlEnum.Keys)
            {
                if (FactoryStandardType.IsNumericTypes(xmlEnum.NetType))
                {
                    enumBuilder.DefineLiteral(xmlkeyValue.Name, FactoryStandardType.ConvertToNumeric(xmlEnum.NetType, xmlkeyValue.Value, xmlEnum.NumberBase));
                }
            }

            return enumBuilder;
        }

        /// <summary>
        /// Runtime struct type builder
        /// </summary>
        /// <param name="moduleBuilder"></param>
        /// <param name="xmlNamespace"></param>
        /// <param name="xmlStruct"></param>
        /// <returns></returns>
        private TypeBuilder CreateRTStructType(ModuleBuilder moduleBuilder, XmlModelNamespaceRoot xmlNamespace, XmlStructLayoutRoot xmlStruct)
        {
            TypeBuilder typeBuilder = moduleBuilder.DefineType(string.Format("{0}.{1}", xmlNamespace.Namespace, xmlStruct.Name),
                        TypeAttributes.Public | TypeAttributes.SequentialLayout | TypeAttributes.Serializable |
                          TypeAttributes.AnsiClass, typeof(ValueType), xmlStruct.Pack);

            if (xmlStruct.Metadata != null)
            {
                CreateRTMetadataAttribute(xmlStruct, typeBuilder);
            }
            
            // Foreach field
            foreach (XmlStructFieldLayout xmlField in xmlStruct.Fields)
            {
                FieldBuilder fieldBuilder = CreateRTFieldType(typeBuilder, xmlStruct, xmlField);
            }

            return typeBuilder;
        }

        /// <summary>
        /// Create the metadata attribute
        /// </summary>
        /// <param name="xmlStruct"></param>
        /// <param name="typeBuilder"></param>
        private void CreateRTMetadataAttribute(XmlStructLayoutRoot xmlStruct, TypeBuilder typeBuilder)
        {
            // Find MarshalAsAttribute's constructor by signature, then invoke
            var metadataType = typeof(MetadataHelpAttribute);
            var ctorInfo = metadataType.GetConstructor(Type.EmptyTypes);

            List<PropertyInfo> lstPropInfo = new List<PropertyInfo>();
            List<object> lstPropValues = new List<object>();
            if (string.IsNullOrEmpty(xmlStruct.Metadata.Description) == false)
            {
                lstPropInfo.Add(metadataType.GetProperty("Description"));
                lstPropValues.Add(xmlStruct.Metadata.Description);
            }
            if (string.IsNullOrEmpty(xmlStruct.Metadata.DisplayName) == false)
            {
                lstPropInfo.Add(metadataType.GetProperty("DisplayName"));
                lstPropValues.Add(xmlStruct.Metadata.DisplayName);
            }

            var fields = metadataType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            var marshalAsAttr = new CustomAttributeBuilder(ctorInfo,
                new object[] { },
                lstPropInfo.ToArray(),
                lstPropValues.ToArray());

            typeBuilder.SetCustomAttribute(marshalAsAttr);
        }

        /// <summary>
        /// Create field
        /// </summary>
        /// <param name="typeBuilder"></param>
        /// <param name="xmlStruct"></param>
        /// <param name="xmlField"></param>
        /// <returns></returns>
        private FieldBuilder CreateRTFieldType(TypeBuilder typeBuilder, XmlStructLayoutRoot xmlStruct, XmlStructFieldLayout xmlField)
        {
            Type fieldType = null;

            if (xmlField.NetType != TypeCode.Empty)
                fieldType = FactoryStandardType.Get(xmlField.NetType);

            if (string.IsNullOrEmpty(xmlField.RefType) == false)
            {
                // Find the type from current assembly
                fieldType = RTAssemblyBuilder.GetType(xmlField.RefType, true);
            }

            if (xmlField.IsArray)
                fieldType = fieldType.MakeArrayType();

            FieldBuilder fieldBuilder = typeBuilder.DefineField(
                            xmlField.Name,
                            fieldType,
                            FieldAttributes.Public);

            if (xmlField.CanMarshallAsStaticSize == true)
            {
                CreateRTMarshalAsAttribute(xmlField, fieldBuilder);
            }

            if(xmlField.Metadata != null)
            {
                CreateRTMetadataAttribute(xmlField, fieldBuilder);
            }

            return fieldBuilder;
        }

        /// <summary>
        /// Add MarshalAs attribute
        /// </summary>
        /// <param name="xmlField"></param>
        /// <param name="fieldBuilder"></param>
        private void CreateRTMarshalAsAttribute(XmlStructFieldLayout xmlField, FieldBuilder fieldBuilder)
        {
            // Find MarshalAsAttribute's constructor by signature, then invoke
            var ctorParameters = new Type[] { typeof(UnmanagedType) };
            var ctorInfo = typeof(MarshalAsAttribute).GetConstructor(ctorParameters);

            var fields = typeof(MarshalAsAttribute).GetFields(BindingFlags.Public | BindingFlags.Instance);
            var sizeConst = (from f in fields
                             where f.Name == "SizeConst"
                             select f).ToArray();
            var marshalAsAttr = new CustomAttributeBuilder(ctorInfo,
                new object[] { xmlField.MarshalAsUnmanagedType }, sizeConst, new object[] { xmlField.MarshalAsSizeConst });

            fieldBuilder.SetCustomAttribute(marshalAsAttr);
        }

        /// <summary>
        /// Add Metadata attribute
        /// </summary>
        /// <param name="xmlField"></param>
        /// <param name="fieldBuilder"></param>
        private void CreateRTMetadataAttribute(XmlStructFieldLayout xmlField, FieldBuilder fieldBuilder)
        {
            // Find MarshalAsAttribute's constructor by signature, then invoke
            var metadataType = typeof(MetadataHelpAttribute);
            var ctorInfo = metadataType.GetConstructor(Type.EmptyTypes);

            List<PropertyInfo> lstPropInfo = new List<PropertyInfo>();
            List<object> lstPropValues = new List<object>();
            if(string.IsNullOrEmpty(xmlField.Metadata.Description) == false)
            {
                lstPropInfo.Add(metadataType.GetProperty("Description"));
                lstPropValues.Add(xmlField.Metadata.Description);
            }
            if (string.IsNullOrEmpty(xmlField.Metadata.DisplayName) == false)
            {
                lstPropInfo.Add(metadataType.GetProperty("DisplayName"));
                lstPropValues.Add(xmlField.Metadata.DisplayName);
            }

            var fields = metadataType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            var marshalAsAttr = new CustomAttributeBuilder(ctorInfo,
                new object[] { },
                lstPropInfo.ToArray(),
                lstPropValues.ToArray());

            fieldBuilder.SetCustomAttribute(marshalAsAttr);
        }

        #endregion Methods
    }
}
