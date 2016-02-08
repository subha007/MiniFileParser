using WinSysInfo.MiniFileParser.Interface;
using WinSysInfo.MiniFileParser.Model;

namespace WinSysInfo.MiniFileParser.Factory
{
    /// <summary>
    /// Factory class used to determine the type of reader
    /// </summary>
    public static class FactoryReaderType
    {
        /// <summary>
        /// Intelligently dtermine the default reader type from the file type
        /// </summary>
        /// <param name="fileExtension"></param>
        public static EnumReaderType FromFileExtension(IFileReaderProperty fileProperty)
        {
            switch (fileProperty.FileType)
            {
                case EnumFileType.EXE:
                    return EnumReaderType.BINARY_READER;

                default:
                    return EnumReaderType.BINARY_READER;
            }
        }
    }
}
