namespace WinSysInfo.MiniFileParser.Model.Enum
{
    /// <summary>
    /// Microsoft tools use this field only to indicate whether the symbol is a function, so that 
    /// the only two resulting values are 0x0 and 0x20 for the Type field. However, other tools 
    /// can use this field to communicate more information.
    /// </summary>
    public enum EnumSymbolTypeMicrosoftTool
    {
        NOT_FUNCTION = 0x0,
        FUNCTION = 0x20
    }
}
