using System;
using System.Xml.Serialization;

namespace WinSysInfo.ReflectionHelper.Model
{
    /// <summary>
    /// Class contains information on the model
    /// </summary>
    [Serializable]
    [XmlRoot("Metadata")]
    public class XmlMetadataLayout
    {
        /// <summary>
        /// A resource name or non-localized text, such as a tool tip, 
        /// that is displayed to users to help them understand a member
        /// </summary>
        [XmlAttribute("description")]
        public string Description { get; set; }

        /// <summary>
        /// A resource name or non-localized text that is used as a member 
        /// label for elements that are bound to the member
        /// </summary>
        [XmlAttribute("displayname")]
        public string DisplayName { get; set; }
    }
}
