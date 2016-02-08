using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// These are common fields for 32 and 32+ extension to the COFF optional header format.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OptionalHeaderWindowsSpecificFieldsCommon2
    {
        /// <summary>
        /// Reserved, must be zero
        /// </summary>
        public uint LoaderFlags;

        /// <summary>
        /// The number of data-directory entries in the remainder of the optional header. 
        /// Each describes a location and size
        /// </summary>
        public uint NumberOfRvaAndSizes;
    }
}
