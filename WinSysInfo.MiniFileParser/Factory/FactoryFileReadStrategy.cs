using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSysInfo.MiniFileParser.Interface;

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
            switch (readerProperty.ReaderType)
            {
                case EnumFileReaderType.MEMORY_SEQ_READ:
                    readStrategy = new MemorySequentialAccess(readerProperty);
                    break;

                case EnumFileReaderType.MEMORY_ACCESSOR_READ:
                    readStrategy = new MemoryRandomAccess(readerProperty);
                    break;

                case EnumFileReaderType.BINARY_READ:
                    break;

                default:
                    throw new NotImplementedException("Unreachable code");
            }

            return readStrategy;
        }
    }
}
