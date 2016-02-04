namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// Normally, the Section Value field in a symbol table entry is a 
    /// one-based index into the section table. However, this field is 
    /// a signed integer and can take negative values. The following 
    /// values, less than one, have special meanings.
    /// </summary>
    public enum EnumSymbolSectionNumber
    {
        /// <summary>
        /// The symbol record is not yet assigned a section. A value of 
        /// zero indicates that a reference to an external symbol is 
        /// defined elsewhere. A value of non-zero is a common symbol with 
        /// a size that is specified by the value.
        /// </summary>
        IMAGE_SYM_UNDEFINED = 0,

        /// <summary>
        /// The symbol has an absolute (non-relocatable) value and is not 
        /// an address
        /// </summary>
        IMAGE_SYM_ABSOLUTE = -1,

        /// <summary>
        /// The symbol provides general type or debugging information but 
        /// does not correspond to a section. Microsoft tools use this setting 
        /// along with .file records (storage class FILE).
        /// </summary>
        IMAGE_SYM_DEBUG = -2,
    }
}
