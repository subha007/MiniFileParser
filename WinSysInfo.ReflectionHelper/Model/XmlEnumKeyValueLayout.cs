using System;
using System.Collections.Generic;
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
    [XmlRoot("Key")]
    public class XmlEnumKeyValueLayout
    {
        #region Properties

        /// <summary>
        /// Name of the property of the struct in .NET
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// the value of the enum key
        /// </summary>
        [XmlAttribute("value")]
        public string Value { get; set; }

        #endregion Properties
    }
}
