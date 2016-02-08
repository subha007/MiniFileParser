using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// bigobj option Increases the number of sections that an object file 
    /// can contain. It increases the address capacity to 4,294,967,296(2**32).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct COFFBigObjHeader
    {
        /// <summary>
        /// Must be IMAGE_FILE_MACHINE_UNKNOWN (0)
        /// </summary>
        public ushort Sig1;

        /// <summary>
        /// Must be 0xFFFF.
        /// </summary>
        public ushort Sig2;

        /// <summary>
        /// Version
        /// </summary>
        public ushort Version;
        public ushort Machine;
        public uint TimeDateStamp;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] UUID;

        public uint Unused1;
        public uint Unused2;
        public uint Unused3;
        public uint Unused4;
        public uint NumberOfSections;
        public uint PointerToSymbolTable;
        public uint NumberOfSymbols;
    }
}
