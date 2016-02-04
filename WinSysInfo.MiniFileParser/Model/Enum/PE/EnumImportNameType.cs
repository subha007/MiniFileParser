namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The null-terminated import symbol name immediately follows its 
    /// associated import header. The following values are defined for 
    /// the Name Type field in the import header. They indicate how the 
    /// name is to be used to generate the correct symbols that represent 
    /// the import.
    /// </summary>
    public enum EnumImportNameType
    {
        /// Import is by ordinal. This indicates that the value in the Ordinal/Hint
        /// field of the import header is the import's ordinal. If this constant is
        /// not specified, then the Ordinal/Hint field should always be interpreted
        /// as the import's hint.
        IMPORT_ORDINAL = 0,
        /// The import name is identical to the public symbol name
        IMPORT_NAME = 1,
        /// The import name is the public symbol name, but skipping the leading ?,
        /// @, or optionally _.
        IMPORT_NAME_NOPREFIX = 2,
        /// The import name is the public symbol name, but skipping the leading ?,
        /// @, or optionally _, and truncating at the first @.
        IMPORT_NAME_UNDECORATE = 3
    }
}
