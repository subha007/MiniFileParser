using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The import header contains the following fields and offsets.
    /// This structure is followed by two null-terminated strings that describe the imported 
    /// symbol’s name and the DLL from which it came.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ImportHeaderLayout
    {
        /// <summary>
        /// Must be IMAGE_FILE_MACHINE_UNKNOWN
        /// </summary>
        public ushort Sig1{ get; set; }

        /// <summary>
        /// Must be 0xFFFF.
        /// </summary>
        public ushort Sig2{ get; set; }

        /// <summary>
        /// The structure version.
        /// </summary>
        public ushort Version{ get; set; }

        /// <summary>
        /// The number that identifies the type of target machine. 
        /// </summary>
        public ushort Machine{ get; set; }

        /// <summary>
        /// The time and date that the file was created.
        /// </summary>
        public uint TimeDateStamp{ get; set; }

        /// <summary>
        /// The size of the strings that follow the header.
        /// </summary>
        public uint SizeOfData{ get; set; }

        /// <summary>
        /// Either the ordinal or the hint for the import, determined by the value in the Name Type field.
        /// </summary>
        public ushort OrdinalHint{ get; set; }

        /// <summary>
        /// The import type. For specific values <see cref="EnumImportType"/>
        /// </summary>
        public byte TypeInfo{ get; set; }

        /// <summary>
        /// The import name type. 
        /// </summary>
        public byte NameType { get; set; }

        /// <summary>
        /// Reserved, must be 0.
        /// </summary>
        public ushort Reserved { get; set; }
    }
}
