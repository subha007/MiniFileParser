using WinSysInfo.MiniFileParser.Interface;
using WinSysInfo.MiniFileParser.Model;

namespace WinSysInfo.MiniFileParser.Factory
{
    /// <summary>
    /// A factory class to determine the Data store to save file parsing structures
    /// </summary>
    public static class FileDataStoreFactory
    {
        /// <summary>
        /// The default data store
        /// </summary>
        /// <returns></returns>
        public static IFileDataStore Default()
        {
            return null;
        }

        public static COFFFileDataStore COFFStore()
        {
            return new COFFFileDataStore();
        }
    }
}
