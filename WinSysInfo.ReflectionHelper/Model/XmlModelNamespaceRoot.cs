using System;
using System.Xml.Serialization;

namespace WinSysInfo.ReflectionHelper.Model
{
    /// <summary>
    /// The class which contains a list of all namespace objects
    /// </summary>
    [Serializable]
    [XmlRoot("Assembly")]
    public class XmlModelNamespaceRoot
    {
        #region Properties

        /// <summary>
        /// The .NET compatible namespace name
        /// </summary>
        [XmlAttribute("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// List of structs
        /// </summary>
        [XmlElement("Struct")]
        public XmlStructFieldLayout[] Structs;

        #endregion Properties
    }
}
