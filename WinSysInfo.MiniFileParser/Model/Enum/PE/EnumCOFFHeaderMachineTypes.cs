namespace WinSysInfo.MiniFileParser.Model
{
    public enum EnumCOFFHeaderMachineTypes : ushort
    {
        MT_Invalid = 0XFFFF,

        /// <summary>
        /// The contents of this field are assumed to be applicable to 
        /// any machine type
        /// </summary>
        IMAGE_FILE_MACHINE_UNKNOWN = 0x0,

        /// <summary>
        /// Matsushita AM33
        /// </summary>
        IMAGE_FILE_MACHINE_AM33 = 0x13,

        /// <summary>
        /// x64
        /// </summary>
        IMAGE_FILE_MACHINE_AMD64 = 0x8664,

        /// <summary>
        /// ARM Little Endian
        /// </summary>
        IMAGE_FILE_MACHINE_ARM = 0x1C0,

        /// <summary>
        /// ARMv7 (or higher) Thumb mode only
        /// </summary>
        IMAGE_FILE_MACHINE_ARMNT = 0x1C4,

        /// <summary>
        /// ARMv8 in 64-bit mode
        /// </summary>
        IMAGE_FILE_MACHINE_ARM64 = 0xAA64,

        /// <summary>
        /// EFI byte code
        /// </summary>
        IMAGE_FILE_MACHINE_EBC = 0xEBC,

        /// <summary>
        /// Intel 386 or later processors and compatible processors
        /// </summary>
        IMAGE_FILE_MACHINE_I386 = 0x14C,

        /// <summary>
        /// Intel Itanium processor family
        /// </summary>
        IMAGE_FILE_MACHINE_IA64 = 0x200,

        /// <summary>
        /// Mitsubishi M32R little endian
        /// </summary>
        IMAGE_FILE_MACHINE_M32R = 0x9041,

        /// <summary>
        /// MIPS16
        /// </summary>
        IMAGE_FILE_MACHINE_MIPS16 = 0x266,

        /// <summary>
        /// MIPS with FPU
        /// </summary>
        IMAGE_FILE_MACHINE_MIPSFPU = 0x366,

        /// <summary>
        /// MIPS16 with FPU
        /// </summary>
        IMAGE_FILE_MACHINE_MIPSFPU16 = 0x466,

        /// <summary>
        /// Power PC little endian
        /// </summary>
        IMAGE_FILE_MACHINE_POWERPC = 0x1F0,

        /// <summary>
        /// Power PC with floating point support
        /// </summary>
        IMAGE_FILE_MACHINE_POWERPCFP = 0x1F1,

        /// <summary>
        /// MIPS little endian
        /// </summary>
        IMAGE_FILE_MACHINE_R4000 = 0x166,

        /// <summary>
        /// Hitachi SH3
        /// </summary>
        IMAGE_FILE_MACHINE_SH3 = 0x1A2,

        /// <summary>
        /// Hitachi SH3 DSP
        /// </summary>
        IMAGE_FILE_MACHINE_SH3DSP = 0x1A3,

        /// <summary>
        /// Hitachi SH4
        /// </summary>
        IMAGE_FILE_MACHINE_SH4 = 0x1A6,

        /// <summary>
        /// Hitachi SH5
        /// </summary>
        IMAGE_FILE_MACHINE_SH5 = 0x1A8,

        /// <summary>
        /// ARM or Thumb (“interworking”)
        /// </summary>
        IMAGE_FILE_MACHINE_THUMB = 0x1C2,

        /// <summary>
        /// MIPS little-endian WCE v2
        /// </summary>
        IMAGE_FILE_MACHINE_WCEMIPSV2 = 0x169
    }
}
