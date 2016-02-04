using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The MSDOS stub is a valid application that runs under MSDOS. 
    /// It is placed at the front of the EXE image. The linker places a 
    /// default stub here, which prints out the message “This program 
    /// cannot be run in DOS mode” when the image is run in MSDOS. The 
    /// user can specify a different stub by using the /STUB linker option.
    /// At location 0x3c, the stub has the file offset to the PE signature. 
    /// This information enables Windows to properly execute the image file, 
    /// even though it has an MSDOS stub. This file offset is placed at 
    /// location 0x3c during linking.
    /// </summary>
    /// <remarks>The DOS compatible header at the front of all PEs.</remarks>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MSDOSHeaderLayout
    {
        /// <summary>
        /// The first field, e_magic, is the so-called magic number.
        /// This is the DOS executable signature. This field is used to identify an 
        /// MS-DOS-compatible file type. All MS-DOS-compatible executable files 
        /// set this value to 0x54AD, which represents the ASCII characters MZ.
        /// It stands for Mark Zbikowski
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public char[] Magic;

        /// <summary>
        /// Number of bytes in last 512-byte page of executable
        /// </summary>
        public ushort UsedBytesInTheLastPage;

        /// <summary>
        /// Total number of 512-byte pages in executable (including the last page)
        /// </summary>
        public ushort FileSizeInPages;

        /// <summary>
        /// Number of relocation entries
        /// </summary>
        public ushort NumberOfRelocationItems;

        /// <summary>
        /// Header size in paragraphs
        /// </summary>
        public ushort HeaderSizeInParagraphs;

        /// <summary>
        /// Minimum paragraphs of memory allocated in addition to the code size
        /// </summary>
        public ushort MinimumExtraParagraphs;

        /// <summary>
        /// Maximum number of paragraphs allocated in addition to the code size
        /// </summary>
        public ushort MaximumExtraParagraphs;

        /// <summary>
        /// Initial SS relative to start of executable
        /// </summary>
        public ushort InitialRelativeSS;

        /// <summary>
        /// Initial SP
        /// </summary>
        public ushort InitialSP;

        /// <summary>
        /// Checksum (or 0) of executable
        /// </summary>
        public ushort Checksum;

        /// <summary>
        /// CS:IP relative to start of executable (entry point). Initial value of
        /// the IP register.
        /// </summary>
        public ushort InitialIP;

        /// <summary>
        /// Initial value of the CS register, relative to the segment the program
        /// was loaded at
        /// </summary>
        public ushort InitialRelativeCS;

        /// <summary>
        /// Offset of relocation table; 40h for new-(NE,LE,LX,W3,PE etc.) 
        /// executable
        /// </summary>
        public ushort AddressOfRelocationTable;

        /// <summary>
        /// Overlay number (0h = main program)
        /// </summary>
        public ushort OverlayNumber;

        /// <summary>
        /// Reserved words
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        private ushort[] Reserved;

        /// <summary>
        /// OEM identifier for <paramref name="OEMinfo"/>
        /// </summary>
        public ushort OEMid;

        /// <summary>
        /// OEM information
        /// </summary>
        public ushort OEMinfo;

        /// <summary>
        /// Reserved words
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        private ushort[] Reserved2;

        /// <summary>
        /// File address of new exe header
        /// </summary>
        public uint AddressOfNewExeHeader;
    }
}
