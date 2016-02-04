namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The following values are defined for base type, although Microsoft
    /// tools generally do not use this field and set the LSB to 0. Instead,
    /// Visual C++ debug information is used to indicate types. However, the 
    /// possible COFF values are listed here for completeness.
    /// </summary>
    public enum EnumSymbolBaseType
    {
        IMAGE_SYM_TYPE_NULL = 0,  ///< No type information or unknown base type.
        IMAGE_SYM_TYPE_VOID = 1,  ///< Used with void pointers and functions.
        IMAGE_SYM_TYPE_CHAR = 2,  ///< A character (signed byte).
        IMAGE_SYM_TYPE_SHORT = 3,  ///< A 2-byte signed integer.
        IMAGE_SYM_TYPE_INT = 4,  ///< A natural integer type on the target.
        IMAGE_SYM_TYPE_LONG = 5,  ///< A 4-byte signed integer.
        IMAGE_SYM_TYPE_FLOAT = 6,  ///< A 4-byte floating-point number.
        IMAGE_SYM_TYPE_DOUBLE = 7,  ///< An 8-byte floating-point number.
        IMAGE_SYM_TYPE_STRUCT = 8,  ///< A structure.
        IMAGE_SYM_TYPE_UNION = 9,  ///< An union.
        IMAGE_SYM_TYPE_ENUM = 10, ///< An enumerated type.
        IMAGE_SYM_TYPE_MOE = 11, ///< A member of enumeration (a specific value).
        IMAGE_SYM_TYPE_BYTE = 12, ///< A byte; unsigned 1-byte integer.
        IMAGE_SYM_TYPE_WORD = 13, ///< A word; unsigned 2-byte integer.
        IMAGE_SYM_TYPE_UINT = 14, ///< An unsigned integer of natural size.
        IMAGE_SYM_TYPE_DWORD = 15  ///< An unsigned 4-byte integer.
    }
}
