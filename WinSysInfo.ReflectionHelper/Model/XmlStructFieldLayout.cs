using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace WinSysInfo.ReflectionHelper.Model
{
    /// <summary>
    /// The class models the xml tag <see cref="Property"/>
    /// This specifies a property of ValueType Struct layout
    /// </summary>
    [Serializable]
    [XmlRoot("Field")]
    public class XmlStructFieldLayout
    {
        #region Properties

        /// <summary>
        /// Name of the property of the struct in .NET
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// The .NET Data Type
        /// </summary>
        [XmlAttribute("type")]
        public EnumNETDataType NetType { get; set; }

        /// <summary>
        /// If the property is an Array
        /// </summary>
        [XmlAttribute("isarray")]
        public bool IsArray { get; set; }

        /// <summary>
        /// Identifies how to marshal parameters or fields to unmanaged code
        /// Used only in  case of structure array field which needs to be marshalled
        /// </summary>
        /// <remarks>
        /// Ignored if <see cref="IsArray"/> value is false
        /// </remarks>
        [XmlAttribute("unmanagedtype")]
        public UnmanagedType MarshalAsUnmanagedType { get; set; }

        /// <summary>
        /// Identifies how to marshal parameters or fields to unmanaged code
        /// Used only in  case of structure array field which needs to be marshalled
        /// </summary>
        /// <remarks>
        /// Ignored if <see cref="IsArray"/> value is false
        /// </remarks>
        [XmlAttribute("sizeconst")]
        public int MarshalAsSizeConst { get; set; }

        /// <summary>
        /// The meta information on the field
        /// </summary>
        [XmlElement("Metadata")]
        public XmlMetadataFieldLayout Metadata { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Specifies default value
        /// </summary>
        /// <remarks>
        /// Default property type is an int type 
        /// </remarks>
        public XmlStructFieldLayout()
        {
            this.NetType = EnumNETDataType.INT;
            this.IsArray = false;
            this.MarshalAsUnmanagedType = UnmanagedType.ByValArray;
            this.MarshalAsSizeConst = 1;
        }

        #endregion Constructor
    }
}
