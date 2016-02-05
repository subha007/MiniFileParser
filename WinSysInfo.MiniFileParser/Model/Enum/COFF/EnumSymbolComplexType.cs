namespace WinSysInfo.MiniFileParser.Model
{
    public enum EnumSymbolComplexType
    {
        IMAGE_SYM_DTYPE_NULL = 0, ///< No complex type; simple scalar variable.
        IMAGE_SYM_DTYPE_POINTER = 1, ///< A pointer to base type.
        IMAGE_SYM_DTYPE_FUNCTION = 2, ///< A function that returns a base type.
        IMAGE_SYM_DTYPE_ARRAY = 3, ///< An array of base type.

        /// Type is formed as (base + (derived << SCT_COMPLEX_TYPE_SHIFT))
        SCT_COMPLEX_TYPE_SHIFT = 4
    }
}
