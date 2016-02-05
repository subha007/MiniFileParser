namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The Selection field of the section definition auxiliary format 
    /// is applicable if the section is a COMDAT section. A COMDAT section
    /// is a section that can be defined by more than one object file. 
    /// (The flag IMAGE_SCN_LNK_COMDAT is set in the Section Flags field of 
    /// the section header.) The Selection field determines the way in which
    /// the linker resolves the multiple definitions of COMDAT sections.
    /// </summary>
    public enum EnumCOMDATType
    {
        IMAGE_COMDAT_SELECT_NODUPLICATES = 1,
        IMAGE_COMDAT_SELECT_ANY,
        IMAGE_COMDAT_SELECT_SAME_SIZE,
        IMAGE_COMDAT_SELECT_EXACT_MATCH,
        IMAGE_COMDAT_SELECT_ASSOCIATIVE,
        IMAGE_COMDAT_SELECT_LARGEST,
        IMAGE_COMDAT_SELECT_NEWEST
    }
}
