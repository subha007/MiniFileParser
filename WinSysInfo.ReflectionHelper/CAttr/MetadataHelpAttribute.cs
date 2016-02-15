using System;

namespace WinSysInfo.ReflectionHelper.CAttr
{
    /// <summary>
    /// The class which stores all help related data for a class or member
    /// </summary>
    public class MetadataHelpAttribute : Attribute
    {
        /// <summary>
        /// A Tooltip like description text
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// An alternate name to display. If empty then use the
        /// maiden name
        /// </summary>
        public string DisplayName { get; set; }
    }
}
