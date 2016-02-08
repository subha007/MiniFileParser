using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// This auxiliary symbol generally follows the IMAGE_SYM_CLASS_CLR_TOKEN. It is used to
    /// associate a token with the COFF symbol table’s namespace
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AuxiliaryCLRTokenLayout
    {
        /// <summary>
        /// Must be IMAGE_AUX_SYMBOL_TYPE_TOKEN_DEF (1).
        /// </summary>
        public EnumAuxSymbolType AuxType;

        /// <summary>
        /// Reserved, must be zero.
        /// </summary>
        public byte Reserved1;

        /// <summary>
        /// The symbol index of the COFF symbol to which this CLR token definition refers.
        /// </summary>
        public uint SymbolTableIndex;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        private char[] unused;
    }
}
