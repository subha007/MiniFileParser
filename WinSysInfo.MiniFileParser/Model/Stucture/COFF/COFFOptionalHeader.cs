namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// Every image file has an optional header that provides information 
    /// to the loader. This header is optional in the sense that some files
    /// (specifically, object files) do not have it. For image files, this
    /// header is required. An object file can have an optional header, but
    /// generally this header has no function in an object file except to 
    /// increase its size. 
    /// </summary>
    public class COFFOptionalHeader
    {
    }
}
