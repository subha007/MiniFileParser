namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// This auxiliary symbol generally follows the IMAGE_SYM_CLASS_CLR_TOKEN. It is used to
    /// associate a token with the COFF symbol table’s namespace
    /// </summary>
    public class AuxiliaryCLRTokenLayout
    {
        /// <summary>
        /// Must be IMAGE_AUX_SYMBOL_TYPE_TOKEN_DEF (1).
        /// </summary>
        public EnumAuxSymbolType AuxType { get; set; }

        /// <summary>
        /// Reserved, must be zero.
        /// </summary>
        public byte Reserved1 { get; set; }

        /// <summary>
        /// The symbol index of the COFF symbol to which this CLR token definition refers.
        /// </summary>
        public uint SymbolTableIndex { get; set; }

        private char[] unused = new char[12];
        public char[] Unused
        {
            get { return this.unused; }
            set { this.unused = value; }
        }
    }
}
