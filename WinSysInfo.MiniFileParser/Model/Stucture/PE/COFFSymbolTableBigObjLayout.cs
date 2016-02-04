using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct COFFSymbolTableBigObjLayout
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
        public uint SectionNumber { get; set; }

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
