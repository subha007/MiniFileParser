using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// A symbol table record marks the beginning of a function definition if it has all of the following:
    /// a storage class of EXTERNAL (2), a Type value that indicates it is a function (0x20), and a section
    /// number that is greater than zero. Note that a symbol table record that has a section number of 
    /// UNDEFINED (0) does not define the function and does not have an auxiliary record. Function-definition 
    /// symbol records are followed by an auxiliary record in the format described in the following table.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AuxiliaryFunctionDefinitionLayout
    {
        /// <summary>
        /// The symbol-table index of the corresponding .bf (begin function) symbol record
        /// </summary>
        public uint TagIndex;

        /// <summary>
        /// The size of the executable code for the function itself. If the function is in its own section, 
        /// the SizeOfRawData in the section header is greater or equal to this field, depending on alignment 
        /// considerations.
        /// </summary>
        public uint TotalSize;

        /// <summary>
        /// The file offset of the first COFF line-number entry for the function, or zero if none exists. 
        /// </summary>
        public uint PointerToLinenumber;

        /// <summary>
        /// The symbol-table index of the record for the next function. If the function is the last in the symbol 
        /// table, this field is set to zero.
        /// </summary>
        public uint PointerToNextFunction;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        private char[] unused;
    }
}
