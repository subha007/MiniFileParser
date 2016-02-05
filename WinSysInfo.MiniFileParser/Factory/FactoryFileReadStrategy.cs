using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            switch (readerProperty.BufferType)
            {
                case EnumReaderBufferType.MEMORY_MAPPED_VIEW_ACCESSOR:
                    readStrategy = new MemorySequentialAccess(readerProperty);
                    break;

                default:
                    throw new NotImplementedException("Unreachable code");
            }

            return readStrategy;
        }
    }
}
