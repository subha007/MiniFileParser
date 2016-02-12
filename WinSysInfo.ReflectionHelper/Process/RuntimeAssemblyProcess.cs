using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinSysInfo.ReflectionHelper.Factory;
using WinSysInfo.ReflectionHelper.Model;

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
        /// <param name="p"></param>
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

            AssemblyBuilder assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);

            // For a single-module assembly, the module name is usually
            // the assembly name plus an extension.
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(this.XmlAssemblyData.FileName + ".dll");

            // foreach namespace create type
            foreach(XmlModelNamespaceRoot xmlNamespace in this.XmlAssemblyData.Namespaces)
            {
                CreateRTNamespaceType(moduleBuilder, xmlNamespace);
            }
        }

        /// <summary>
        /// Runtime type builder
        /// </summary>
        /// <param name="moduleBuilder"></param>
        /// <param name="xmlNamespace"></param>
        /// <returns></returns>
        private void CreateRTNamespaceType(ModuleBuilder moduleBuilder, XmlModelNamespaceRoot xmlNamespace)
        {
            // Foreach structure types
            foreach(XmlStructLayoutRoot xmlStruct in xmlNamespace.Structs)
            {
                TypeBuilder typeBuilder = CreateRTStructType(moduleBuilder, xmlNamespace, xmlStruct);
            }
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
                        TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.SequentialLayout | TypeAttributes.Serializable |
                          TypeAttributes.AnsiClass, typeof(ValueType), (int)xmlStruct.Pack);

            // Foreach field
            foreach (XmlStructFieldLayout xmlField in xmlStruct.Fields)
            {
                FieldBuilder fieldBuilder = CreateRTFieldType(typeBuilder, xmlStruct, xmlField);
            }

            return typeBuilder;
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
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
