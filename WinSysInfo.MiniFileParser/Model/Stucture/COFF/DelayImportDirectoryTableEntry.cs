using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DelayImportDirectoryTableEntry
    {
        /// <summary>
        /// Must be zero
        /// </summary>
        public uint Attributes { get; set; }

        /// <summary>
        /// The RVA of the name of the DLL to be loaded. The name resides 
        /// in the read-only data section of the image
        /// </summary>
        public uint Name { get; set; }

        /// <summary>
        /// The RVA of the module handle (in the data section of the image) 
        /// of the DLL to be delay-loaded. It is used for storage by the routine
        /// that is supplied to manage delay-loading
        /// </summary>
        public uint ModuleHandle { get; set; }

        /// <summary>
        /// The RVA of the delay-load import address table. For more information
        /// </summary>
        public uint DelayImportAddressTable { get; set; }

        /// <summary>
        /// The RVA of the delay-load name table, which contains the names of the 
        /// imports that might need to be loaded. This matches the layout of the import name table
        /// </summary>
        public uint DelayImportNameTable { get; set; }

        /// <summary>
        /// The RVA of the bound delay-load address table, if it exists.
        /// </summary>
        public uint BoundDelayImportTable { get; set; }

        /// <summary>
        /// The RVA of the unload delay-load address table, if it exists. This is an exact 
        /// copy of the delay import address table. If the caller unloads the DLL, this table 
        /// should be copied back over the delay import address table so that subsequent calls 
        /// to the DLL continue to use the thunking mechanism correctly.
        /// </summary>
        public uint UnloadDelayImportTable { get; set; }

        /// <summary>
        /// The timestamp of the DLL to which this image has been bound
        /// </summary>
        public uint TimeStamp { get; set; }
    }
}
