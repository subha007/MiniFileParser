using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// This format follows a symbol-table record that defines a section. Such a record has a 
    /// symbol name that is the name of a section (such as .text or .drectve) and has storage 
    /// class STATIC (3). The auxiliary record provides information about the section to which
    /// it refers. Thus, it duplicates some of the information in the section header.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AuxiliarySectionDefinitionsLayout
    {
        /// <summary>
        /// The size of section data; the same as SizeOfRawData in the section header.
        /// </summary>
        public uint Length;

        /// <summary>
        /// The number of relocation entries for the section
        /// </summary>
        public ushort NumberOfRelocations;

        /// <summary>
        /// The number of line-number entries for the section.
        /// </summary>
        public ushort NumberOfLinenumbers;

        /// <summary>
        /// The checksum for communal data. It is applicable if the IMAGE_SCN_LNK_COMDAT flag
        /// is set in the section header.
        /// </summary>
        public uint CheckSum;

        /// <summary>
        /// One-based index into the section table for the associated section. This is used when
        /// the COMDAT selection setting is 5.
        /// </summary>
        public uint Number;

        /// <summary>
        /// The COMDAT selection number. This is applicable if the section is a COMDAT section
        /// </summary>
        public byte Selection;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        private char[] unused;
    }
}
