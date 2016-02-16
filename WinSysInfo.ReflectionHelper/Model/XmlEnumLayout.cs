using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WinSysInfo.ReflectionHelper.Model
{
    /// <summary>
    /// The class models the xml tag <see cref="Property"/>
    /// This specifies a property of ValueType Struct layout
    /// </summary>
    [Serializable]
    [XmlRoot("Enum")]
    public class XmlEnumLayout
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
        [XmlAttribute("dtype")]
        public TypeCode NetType { get; set; }

        /// <summary>
        /// The format of the value
        /// </summary>
        [XmlAttribute("numbase")]
        public int NumberBase;

        /// <summary>
        /// The list of public fields
        /// </summary>
        [XmlElement("Key")]
        public XmlEnumKeyValueLayout[] Keys;

        /// <summary>
        /// The meta information on the field
        /// </summary>
        [XmlElement("Metadata")]
        public XmlMetadataLayout Metadata { get; set; }

        #endregion Properties
    }
}
