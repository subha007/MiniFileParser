namespace WinSysInfo.MiniFileParser.Model
{
    public class OptionalHeaderDataDirImageOnlyLayoutModel : LayoutModel<OptionalHeaderDataDirImageOnly>
    {
        public OptionalHeaderDataDirImageOnlyLayoutModel(LayoutModel<OptionalHeaderDataDirImageOnly> obj)
            : base(obj)
        { }

        public uint NumberOfDirectory { get; set; }
    }
}
