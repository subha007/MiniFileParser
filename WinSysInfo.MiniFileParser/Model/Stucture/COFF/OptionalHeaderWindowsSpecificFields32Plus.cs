using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The next 21 fields are an extension to the COFF optional header format.
    /// They contain additional information that is required by the linker and 
    /// loader in Windows
    /// Used for PE32+
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OptionalHeaderWindowsSpecificFields32Plus
    {
        /// <summary>
        /// The preferred address of the first byte of image when loaded 
        /// into memory; must be a multiple of 64 K. The default for DLLs 
        /// is 0x10000000. The default for Windows CE EXEs is 0x00010000. 
        /// The default for Windows NT, Windows 2000, Windows XP, Windows 95,
        /// Windows 98, and Windows Me is 0x00400000
        /// </summary>
        public ulong ImageBase;

        /// <summary>
        /// Common for 32 and 32+ format
        /// </summary>
        public OptionalHeaderWindowsSpecificFieldsCommon1 CommonData1;

        /// <summary>
        /// The size of the stack to reserve. Only SizeOfStackCommit is committed; 
        /// the rest is made available one page at a time until the reserve size is reached
        /// </summary>
        public ulong SizeOfStackReserve;

        /// <summary>
        /// The size of the stack to commit.
        /// </summary>
        public ulong SizeOfStackCommit;

        /// <summary>
        /// The size of the local heap space to reserve. Only SizeOfHeapCommit is committed;
        /// the rest is made available one page at a time until the reserve size is reached
        /// </summary>
        public ulong SizeOfHeapReserve;

        /// <summary>
        /// The size of the local heap space to commit
        /// </summary>
        public ulong SizeOfHeapCommit;

        /// <summary>
        /// Common for 32 and 32+ format
        /// </summary>
        public OptionalHeaderWindowsSpecificFieldsCommon2 CommonData2;
    }
}
