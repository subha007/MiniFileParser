namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The optional header magic number determines whether an 
    /// image is a PE32 or PE32+ executable.
    /// </summary>
    public enum EnumOptionalHeaderMagicNo : ushort
    {
        IMAGE_NT_OPTIONAL_HDR32_MAGIC = 0x10b,
        IMAGE_NT_OPTIONAL_HDR64_MAGIC = 0x20b,
        IMAGE_ROM_OPTIONAL_HDR_MAGIC = 0x107
    }
}
