using System;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// Represents the reader buffer type
    /// </summary>
    [Flags]
    public enum EnumReaderBufferType
    {
        NONE = 0,
        MEMORY_MAPPED_VIEW_ACCESSOR = 1,
        TEMP_FILE = 2
    }
}
