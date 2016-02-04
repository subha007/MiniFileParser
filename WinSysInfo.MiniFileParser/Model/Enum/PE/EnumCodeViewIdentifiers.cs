namespace WinSysInfo.MiniFileParser.Model
{
    public enum EnumCodeViewIdentifiers
    {
        DEBUG_LINE_TABLES_HAVE_COLUMN_RECORDS = 0x1,
        DEBUG_SECTION_MAGIC = 0x4,
        DEBUG_SYMBOL_SUBSECTION = 0xF1,
        DEBUG_LINE_TABLE_SUBSECTION = 0xF2,
        DEBUG_STRING_TABLE_SUBSECTION = 0xF3,
        DEBUG_INDEX_SUBSECTION = 0xF4,

        // Symbol subsections are split into records of different types.
        DEBUG_SYMBOL_TYPE_PROC_START = 0x1147,
        DEBUG_SYMBOL_TYPE_PROC_END = 0x114F
    }
}
