using System;
using System.Xml.Serialization;

namespace WinSysInfo.ReflectionHelper.Model
{
    /// <summary>
    /// The class which contains a list of all namespace objects
    /// </summary>
    [Serializable]
    [XmlRoot("Namespace")]
    public class XmlModelNamespaceRoot
    {
        #region Properties

        /// <summary>
        /// The .NET compatible namespace name
        /// </summary>
        [XmlAttribute("name")]
        public string Namespace { get; set; }

        /// <summary>
        /// List of structs
        /// </summary>
        [XmlElement("Struct")]
        public XmlStructLayoutRoot[] Structs;

        /// <summary>
        /// List of structs
        /// </summary>
        [XmlElement("Enum")]
        public XmlEnumLayout[] Enums;

        #endregion Properties
    }
}
