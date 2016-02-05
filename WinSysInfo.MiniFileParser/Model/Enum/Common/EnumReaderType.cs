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
        TEXT_READER = 2,
        XML_READER = 3,
        XML_TEXT_READER = 4,
        XML_DOCUMENT_READER = 5,
        STREAM_READER = 6,
        FILE_STREAM = 7,
        MEMORY_MAPPED_VIEW_ACCESSOR = 8,
    }
}
