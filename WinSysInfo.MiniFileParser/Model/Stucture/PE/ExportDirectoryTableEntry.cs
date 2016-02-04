using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The delay-load directory table is the counterpart to the import
    /// directory table. It can be retrieved through the Delay Import 
    /// Descriptor entry in the optional header data directories list (offset 200). 
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ExportDirectoryTableEntry
    {
        /// <summary>
        /// Reserved, must be 0
        /// </summary>
        public uint ExportFlags { get; set; }

        /// <summary>
        /// The time and date that the export data was created
        /// </summary>
        public uint TimeDateStamp { get; set; }

        /// <summary>
        /// The major version number. The major and minor version numbers 
        /// can be set by the user.
        /// </summary>
        public ushort MajorVersion { get; set; }

        /// <summary>
        /// The minor version number
        /// </summary>
        public ushort MinorVersion { get; set; }

        /// <summary>
        /// The address of the ASCII string that contains the name of the DLL.
        /// This address is relative to the image base.
        /// </summary>
        public uint NameRVA { get; set; }

        /// <summary>
        /// The starting ordinal number for exports in this image. This field 
        /// specifies the starting ordinal number for the export address table. 
        /// It is usually set to 1.
        /// </summary>
        public uint OrdinalBase { get; set; }

        /// <summary>
        /// The number of entries in the export address table.
        /// </summary>
        public uint AddressTableEntries { get; set; }

        /// <summary>
        /// The number of entries in the name pointer table. This is also the number 
        /// of entries in the ordinal table.The number of entries in the name pointer 
        /// table. This is also the number of entries in the ordinal table.
        /// </summary>
        public uint NumberOfNamePointers { get; set; }

        /// <summary>
        /// The address of the export address table, relative to the image base.
        /// </summary>
        public uint ExportAddressTableRVA { get; set; }

        /// <summary>
        /// The address of the export name pointer table, relative to the image base.
        /// The table size is given by the Number of Name Pointers field.
        /// </summary>
        public uint NamePointerRVA { get; set; }

        /// <summary>
        /// The address of the ordinal table, relative to the image base.
        /// </summary>
        public uint OrdinalTableRVA { get; set; }
    }
}
