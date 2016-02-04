using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// After the MSDOS stub, at the file offset specified at offset 0x3c,
    /// is a 4-byte signature that identifies the file as a PE format image file
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NTHeaderLayout
    {
        /// <summary>
        /// This signature is “PE\0\0” (the letters “P” and “E” followed by two 
        /// null bytes).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Signature;
    }
}
