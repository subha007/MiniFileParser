using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// Types of binary reader
    /// </summary>
    public enum EnumReaderType
    {
        BINARY_READER = 1,
        STREAM = 2,
        FILE_STREAM = 3,
        MEMORY_SEQ_ACCESS = 4,
        MEMORY_RANDOM_ACCESS = 5
    }
}
