using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The class to represent Stub
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public struct MSDOSStubLayout
    {
        /// <summary>
        /// A Stub which generally contains a message and
        /// a assembly code section to execute
        /// </summary>
        public byte[] Stub;
    }
}
