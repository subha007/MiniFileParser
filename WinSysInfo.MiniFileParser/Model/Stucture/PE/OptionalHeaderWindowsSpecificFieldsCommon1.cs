using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// These are common fields for 32 and 32+ extension to the COFF optional header format.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OptionalHeaderWindowsSpecificFieldsCommon1
    {
        /// <summary>
        /// The alignment (in bytes) of sections when they are loaded into 
        /// memory. It must be greater than or equal to FileAlignment. The 
        /// default is the page size for the architecture
        /// </summary>
        public uint SectionAlignment { get; set; }

        /// <summary>
        /// The alignment factor (in bytes) that is used to align the raw data
        /// of sections in the image file. The value should be a power of 2 
        /// between 512 and 64 K, inclusive. The default is 512. If the 
        /// SectionAlignment is less than the architecture’s page size, then 
        /// FileAlignment must match SectionAlignment.
        /// </summary>
        public uint FileAlignment { get; set; }

        /// <summary>
        /// The major version number of the required operating system
        /// </summary>
        public ushort MajorOperatingSystemVersion { get; set; }

        /// <summary>
        /// The minor version number of the required operating system
        /// </summary>
        public ushort MinorOperatingSystemVersion { get; set; }

        /// <summary>
        /// The major version number of the image
        /// </summary>
        public ushort MajorImageVersion { get; set; }

        /// <summary>
        /// The minor version number of the image
        /// </summary>
        public ushort MinorImageVersion { get; set; }

        /// <summary>
        /// The major version number of the subsystem
        /// </summary>
        public ushort MajorSubsystemVersion { get; set; }

        /// <summary>
        /// The minor version number of the subsystem
        /// </summary>
        public ushort MinorSubsystemVersion { get; set; }

        /// <summary>
        /// Reserved, must be zero
        /// </summary>
        public uint Win32VersionValue { get; set; }

        /// <summary>
        /// The size (in bytes) of the image, including all headers, as the 
        /// image is loaded in memory. It must be a multiple of SectionAlignment
        /// </summary>
        public uint SizeOfImage { get; set; }

        /// <summary>
        /// The combined size of an MSDOS stub, PE header, and section headers 
        /// rounded up to a multiple of FileAlignment
        /// </summary>
        public uint SizeOfHeaders { get; set; }

        /// <summary>
        /// The image file checksum. The algorithm for computing the checksum is 
        /// incorporated into IMAGHELP.DLL. The following are checked for validation
        /// at load time: all drivers, any DLL loaded at boot time, and any DLL
        /// that is loaded into a critical Windows process
        /// </summary>
        public uint CheckSum { get; set; }

        /// <summary>
        /// The subsystem that is required to run this image
        /// </summary>
        public ushort Subsystem { get; set; }

        /// <summary>
        /// Dll characteristics
        /// </summary>
        public EnumCOFFDllCharacteristics DllCharacteristics { get; set; }
    }
}
