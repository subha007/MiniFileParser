namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The following relocation type indicators are defined for ARM processors.
    /// </summary>
    public enum EnumRelocationTypesARM
    {
        IMAGE_REL_ARM_ABSOLUTE = 0x0000,
        IMAGE_REL_ARM_ADDR32 = 0x0001,
        IMAGE_REL_ARM_ADDR32NB = 0x0002,
        IMAGE_REL_ARM_BRANCH24 = 0x0003,
        IMAGE_REL_ARM_BRANCH11 = 0x0004,
        IMAGE_REL_ARM_TOKEN = 0x0005,
        IMAGE_REL_ARM_BLX24 = 0x0008,
        IMAGE_REL_ARM_BLX11 = 0x0009,
        IMAGE_REL_ARM_SECTION = 0x000E,
        IMAGE_REL_ARM_SECREL = 0x000F,
        IMAGE_REL_ARM_MOV32A = 0x0010,
        IMAGE_REL_ARM_MOV32T = 0x0011,
        IMAGE_REL_ARM_BRANCH20T = 0x0012,
        IMAGE_REL_ARM_BRANCH24T = 0x0014,
        IMAGE_REL_ARM_BLX23T = 0x0015
    }
}
