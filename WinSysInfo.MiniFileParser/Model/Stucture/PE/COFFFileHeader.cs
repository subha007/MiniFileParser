using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// At the beginning of an object file, or immediately after the 
    /// signature of an image file, is a standard COFF file header in 
    /// the following format. Note that the Windows loader limits the 
    /// number of sections to 96.
    /// <see cref="http://osxr.org:8080/android/source/external/llvm/include/llvm/Support/COFF.h"/>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct COFFFileHeader
    {
        /// <summary>
        /// The number that identifies the type of target machine
        /// </summary>
        public EnumCOFFHeaderMachineTypes Machine { get; set; }

        /// <summary>
        /// The number of sections. This indicates the size of the section 
        /// table, which immediately follows the headers
        /// </summary>
        public ushort NumberOfSections { get; set; }

        /// <summary>
        /// The low 32 bits of the number of seconds since 00:00 January 1, 1970
        /// (a C run-time time_t value), that indicates when the file was created
        /// </summary>
        public uint TimeDateStamp { get; set; }

        /// <summary>
        /// The file offset of the COFF symbol table, or zero if no COFF symbol 
        /// table is present. This value should be zero for an image because COFF 
        /// debugging information is deprecated
        /// </summary>
        public uint PointerToSymbolTable { get; set; }

        /// <summary>
        /// The number of entries in the symbol table. This data can be used to 
        /// locate the string table, which immediately follows the symbol table. 
        /// This value should be zero for an image because COFF debugging information 
        /// is deprecated
        /// </summary>
        public uint NumberOfSymbols { get; set; }

        /// <summary>
        /// The size of the optional header, which is required for executable files 
        /// but not for object files. This value should be zero for an object file.
        /// </summary>
        public ushort SizeOfOptionalHeader { get; set; }

        /// <summary>
        /// The flags that indicate the attributes of the file
        /// </summary>
        public EnumCOFFHeaderCharacteristics Characteristics { get; set; }
    }
}
