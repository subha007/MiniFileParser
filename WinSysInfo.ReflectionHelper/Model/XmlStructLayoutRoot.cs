using System;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace WinSysInfo.ReflectionHelper.Model
{
    /// <summary>
    /// The class models the xml tag <see cref="Struct"/>
    /// This specifies the ValueType Struct layout
    /// </summary>
    [Serializable]
    [XmlRoot("Struct")]
    public class XmlStructLayoutRoot
    {
        #region Properties

        /// <summary>
        /// The type of structure id
        /// </summary>
        [XmlAttribute("structid")]
        public EnumPECOFFStructType StructId { get; set; }

        /// <summary>
        /// Name of the struct in .NET
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Controls the layout of an object when exported to unmanaged code
        /// </summary>
        [XmlAttribute("kind")]
        public LayoutKind Kind { get; set; }

        /// <summary>
        /// Controls the alignment of data fields of a class or structure in memory
        /// </summary>
        [XmlAttribute("pack")]
        public PackingSize Pack { get; set; }

        /// <summary>
        /// The list of public fields
        /// </summary>
        [XmlElement("Field")]
        public XmlStructFieldLayout[] Fields;

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
        /// Default value of Layout Kind is <see cref="LayoutKind.Sequential"/> 
        /// Default value of Pack is 1.
        /// </remarks>
        public XmlStructLayoutRoot()
        {
            this.Kind = LayoutKind.Sequential;
            this.Pack = PackingSize.Size1;
        }

        #endregion Constructor
    }
}
