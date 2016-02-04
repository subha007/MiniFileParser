namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The following relocation type indicators are defined for 
    /// Intel 386 and compatible processors
    /// </summary>
    public enum EnumRelocationTypeI386
    {
        IMAGE_REL_I386_ABSOLUTE = 0x0000,
        IMAGE_REL_I386_DIR16 = 0x0001,
        IMAGE_REL_I386_REL16 = 0x0002,
        IMAGE_REL_I386_DIR32 = 0x0006,
        IMAGE_REL_I386_DIR32NB = 0x0007,
        IMAGE_REL_I386_SEG12 = 0x0009,
        IMAGE_REL_I386_SECTION = 0x000A,
        IMAGE_REL_I386_SECREL = 0x000B,
        IMAGE_REL_I386_TOKEN = 0x000C,
        IMAGE_REL_I386_SECREL7 = 0x000D,
        IMAGE_REL_I386_REL32 = 0x0014
    }
}
