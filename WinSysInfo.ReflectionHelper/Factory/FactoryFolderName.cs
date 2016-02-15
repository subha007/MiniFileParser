using WinSysInfo.ReflectionHelper.Model;

namespace WinSysInfo.ReflectionHelper.Factory
{
    /// <summary>
    /// Generate the folder name where the xml file resides
    /// </summary>
    public static class FactoryFolderName
    {
        /// <summary>
        /// Get the folder name
        /// </summary>
        /// <returns></returns>
        public static string GetFolderName(EnumXmlFilePathType pathType)
        {
            switch(pathType)
            {
                case EnumXmlFilePathType.FILE_PARSE:
                    return "FileParse";
                default:
                    return "";
            }
        }
    }
}
