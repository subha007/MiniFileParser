namespace WinSysInfo.MiniFileParser.Model
{
    public enum EnumBaseRelocationType
    {
        IMAGE_REL_BASED_ABSOLUTE = 0,
        IMAGE_REL_BASED_HIGH = 1,
        IMAGE_REL_BASED_LOW = 2,
        IMAGE_REL_BASED_HIGHLOW = 3,
        IMAGE_REL_BASED_HIGHADJ = 4,
        IMAGE_REL_BASED_MIPS_JMPADDR = 5,
        IMAGE_REL_BASED_ARM_MOV32A = 5,
        IMAGE_REL_BASED_ARM_MOV32T = 7,
        IMAGE_REL_BASED_MIPS_JMPADDR16 = 9,
        IMAGE_REL_BASED_DIR64 = 10
    }
}
