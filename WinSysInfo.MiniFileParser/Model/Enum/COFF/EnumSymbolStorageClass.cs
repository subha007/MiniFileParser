namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The StorageClass field of the symbol table indicates what kind of 
    /// definition a symbol represents. The following table shows possible 
    /// values. Note that the StorageClass field is an unsigned 1-byte integer.
    /// The special value -1 should therefore be taken to mean its unsigned 
    /// equivalent, 0xFF.
    /// </summary>
    public enum EnumSymbolStorageClass : byte
    {
        IMAGE_SYM_CLASS_END_OF_FUNCTION = 0xff,  ///< Physical end of function
        IMAGE_SYM_CLASS_NULL = 0,   ///< No symbol
        IMAGE_SYM_CLASS_AUTOMATIC = 1,   ///< Stack variable
        IMAGE_SYM_CLASS_EXTERNAL = 2,   ///< External symbol
        IMAGE_SYM_CLASS_STATIC = 3,   ///< Static
        IMAGE_SYM_CLASS_REGISTER = 4,   ///< Register variable
        IMAGE_SYM_CLASS_EXTERNAL_DEF = 5,   ///< External definition
        IMAGE_SYM_CLASS_LABEL = 6,   ///< Label
        IMAGE_SYM_CLASS_UNDEFINED_LABEL = 7,   ///< Undefined label
        IMAGE_SYM_CLASS_MEMBER_OF_STRUCT = 8,   ///< Member of structure
        IMAGE_SYM_CLASS_ARGUMENT = 9,   ///< Function argument
        IMAGE_SYM_CLASS_STRUCT_TAG = 10,  ///< Structure tag
        IMAGE_SYM_CLASS_MEMBER_OF_UNION = 11,  ///< Member of union
        IMAGE_SYM_CLASS_UNION_TAG = 12,  ///< Union tag
        IMAGE_SYM_CLASS_TYPE_DEFINITION = 13,  ///< Type definition
        IMAGE_SYM_CLASS_UNDEFINED_STATIC = 14,  ///< Undefined static
        IMAGE_SYM_CLASS_ENUM_TAG = 15,  ///< Enumeration tag
        IMAGE_SYM_CLASS_MEMBER_OF_ENUM = 16,  ///< Member of enumeration
        IMAGE_SYM_CLASS_REGISTER_PARAM = 17,  ///< Register parameter
        IMAGE_SYM_CLASS_BIT_FIELD = 18,  ///< Bit field
        /// ".bb" or ".eb" - beginning or end of block
        IMAGE_SYM_CLASS_BLOCK = 100,
        /// ".bf" or ".ef" - beginning or end of function
        IMAGE_SYM_CLASS_FUNCTION = 101,
        IMAGE_SYM_CLASS_END_OF_STRUCT = 102, ///< End of structure
        IMAGE_SYM_CLASS_FILE = 103, ///< File name
        /// Line number, reformatted as symbol
        IMAGE_SYM_CLASS_SECTION = 104,
        IMAGE_SYM_CLASS_WEAK_EXTERNAL = 105, ///< Duplicate tag
        /// External symbol in dmert public lib
        IMAGE_SYM_CLASS_CLR_TOKEN = 107
    }
}
