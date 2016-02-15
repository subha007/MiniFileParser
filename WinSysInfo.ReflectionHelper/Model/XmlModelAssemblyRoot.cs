using System;
using System.Xml.Serialization;

namespace WinSysInfo.ReflectionHelper.Model
{
    /// <summary>
    /// The class which contains a the assembly data
    /// </summary>
    [Serializable]
    [XmlRoot("Assembly")]
    public class XmlModelAssemblyRoot
    {
        #region Properties

        /// <summary>
        /// The PE file name of the assembly if generated on disk.
        /// </summary>
        [XmlAttribute("file")]
        public string FileName { get; set; }

        /// <summary>
        /// The .NET compatible assembly version
        /// The format is 'major.minor[.build[.revision]]'
        /// </summary>
        [XmlAttribute("version")]
        public string Version { get; set; }

        /// <summary>
        /// List of namespaces
        /// </summary>
        [XmlElement("Namespace")]
        public XmlModelNamespaceRoot[] Namespaces { get; set; }

        #endregion Properties
    }
}
