using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// For each function definition in the symbol table, three items describe the beginning, 
    /// ending, and number of lines. Each of these symbols has storage class FUNCTION (101):
    /// <para> A symbol record named .bf (begin function). The Value field is unused. A symbol record 
    /// named .lf (lines in function). The Value field gives the number of lines in the function. </para>
    /// <para> A symbol record named .ef (end of function). The Value field has the same number as the 
    /// Total Size field in the function-definition symbol record. The .bf and .ef symbol records 
    /// (but not .lf records) are followed by an auxiliary record with the following format. </para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AuxiliarybfAndefSymbolLayout
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        private byte[] unused1;

        /// <summary>
        /// The actual ordinal line number (1, 2, 3, and so on) within the source file, corresponding
        /// to the .bf or .ef record.
        /// </summary>
        public ushort Linenumber;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        private byte[] unused2;

        /// <summary>
        /// The symbol-table index of the next .bf symbol record. If the function is the last in the symbol 
        /// table, this field is set to zero. It is not used for .ef records.
        /// </summary>
        public uint PointerToNextFunction;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        private byte[] unused3;
    }
}
