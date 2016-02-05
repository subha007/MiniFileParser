using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The symbol table in this section is inherited from the traditional COFF format. 
    /// It is distinct from Microsoft Visual C++® debug information. A file can contain 
    /// both a COFF symbol table and Visual C++ debug information, and the two are kept separate. 
    /// Some Microsoft tools use the symbol table for limited but important purposes, such as 
    /// communicating COMDAT information to the linker. Section names and file names, as well 
    /// as code and data symbols, are listed in the symbol table.
    /// The location of the symbol table is indicated in the COFF header.The symbol table is an 
    /// array of records, each 18 bytes long. Each record is either a standard or auxiliary symbol-table 
    /// record. A standard record defines a symbol or name and has the following format.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct COFFSymbolTableLayout
    {
        /// <summary>
        /// The name of the symbol, represented by a union of three structures. An array of 8 bytes is 
        /// used if the name is not more than 8 bytes long. 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)EnumCOFFSizes.NameSize)]
        public char[] Name;

        /// <summary>
        /// The value that is associated with the symbol. The interpretation of this field depends on 
        /// SectionNumber and StorageClass. A typical meaning is the relocatable address.
        /// </summary>
        public uint Value { get; set; }

        /// <summary>
        /// The signed integer that identifies the section, using a one-based index into the section table.
        /// </summary>
        public ushort SectionNumber { get; set; }

        /// <summary>
        /// A number that represents type. Microsoft tools set this field to 0x20 (function) or 0x0 
        /// (not a function). 
        /// </summary>
        public ushort Type { get; set; }

        /// <summary>
        /// An enumerated value that represents storage class.
        /// </summary>
        public EnumSymbolStorageClass StorageClass { get; set; }

        /// <summary>
        /// The number of auxiliary symbol table entries that follow this record.
        /// </summary>
        public byte NumberOfAuxSymbols { get; set; }
    }
}
