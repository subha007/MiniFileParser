using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// Each data directory gives the address and size of a table or 
    /// string that Windows uses. These data directory entries are all 
    /// loaded into memory so that the system can use them at run time. 
    /// A data directory is an 8byte field that has the following declaration.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OptionalHeaderDataDirImageOnly
    {
        /// <summary>
        /// The first field, VirtualAddress, is actually the RVA of the table.
        /// The RVA is the address of the table relative to the base address 
        /// of the image when the table is loaded
        /// </summary>
        public uint RelativeVirtualAddress { get; set; }

        /// <summary>
        /// The second field gives the size in bytes. 
        /// </summary>
        public uint Size { get; set; }
    }
}
