using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The base relocation table contains entries for all base relocations
    /// in the image. The Base Relocation Table field in the optional header
    /// data directories gives the number of bytes in the base relocation table.
    /// The base relocation table is divided into blocks. Each block represents
    /// the base relocations for a 4K page. Each block must start on a 32-bit boundary.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct COFFBaseRelocBlockHeader
    {
        /// <summary>
        /// The image base plus the page RVA is added to each offset to create the 
        /// VA where the base relocation must be applied
        /// </summary>
        public uint PageRVA;

        /// <summary>
        /// The total number of bytes in the base relocation block, including the 
        /// Page RVA and Block Size fields and the Type/Offset fields that follow
        /// </summary>
        public uint BlockSize;
    }
}
