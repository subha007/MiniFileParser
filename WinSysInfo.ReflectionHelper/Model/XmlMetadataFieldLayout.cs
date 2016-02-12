using System;
using System.Xml.Serialization;

namespace WinSysInfo.ReflectionHelper.Model
{
    /// <summary>
    /// Class contains information on the model
    /// </summary>
    [Serializable]
    [XmlRoot("Metadata")]
    public class XmlMetadataFieldLayout
    {
        /// <summary>
        /// One liner help string on the field
        /// </summary>
        [XmlAttribute("help")]
        public string HelpString { get; set; }
    }
}
