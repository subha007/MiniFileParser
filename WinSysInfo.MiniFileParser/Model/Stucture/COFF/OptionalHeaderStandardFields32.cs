using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// PE32 contains this additional field, which is absent in PE32+, 
    /// following BaseOfCode.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OptionalHeaderStandardFields32
    {
        /// <summary>
        /// The address that is relative to the image base of the beginning-of-data
        /// section when it is loaded into memory
        /// </summary>
        public uint BaseOfData { get; set; }
    }
}
