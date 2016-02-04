namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// Sizes in bytes of various things in the COFF format.
    /// </summary>
    public enum EnumCOFFSizes
    {
        Header16Size = 20,
        Header32Size = 56,
        NameSize = 8,
        Symbol16Size = 18,
        Symbol32Size = 20,
        SectionSize = 40,
        RelocationSize = 10
    }
}
