using System;
using WinSysInfo.MiniFileParser.Interface;
using WinSysInfo.MiniFileParser.Model;
using WinSysInfo.MiniFileParser.Process;

namespace WinSysInfo.MiniFileParser.Factory
{
    /// <summary>
    /// Factory class to get the best file reader
    /// </summary>
    public static class FactoryFileReadStrategy
    {
        /// <summary>
        /// get the file reader from the file property
        /// </summary>
        /// <param name="readerProperty"></param>
        /// <returns></returns>
        public static IFileReadStrategy Instance(IFileReaderProperty readerProperty)
        {
            IFileReadStrategy readStrategy = null;

            if(readerProperty.BufferType.HasFlag(EnumReaderBufferType.MEMORY_MAPPED_VIEW_ACCESSOR))
                readStrategy = new MemorySequentialAccess(readerProperty);
            else
                throw new NotImplementedException("Unreachable code");

            return readStrategy;
        }
    }
}
