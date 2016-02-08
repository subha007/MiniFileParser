namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// Defines the different file types mostly by parsing the file extension
    /// or set explicitly
    /// </summary>
    public enum EnumFileType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        NONE = 0,

        /// <summary>
        /// Windows Executable format
        /// </summary>
        /// <remarks>File extesnion .exe</remarks>
        EXE = 1,

        /// <summary>
        /// A Dll file type
        /// </summary>
        /// <remarks>File extension .dll</remarks>
        DLL = 10,

        /// <summary>
        /// An Obj file type
        /// </summary>
        /// <remarks>File extesnion .obj</remarks>
        OBJ = 20,

        /// <summary>
        /// An xml file
        /// </summary>
        /// <remarks>File extension .xml</remarks>
        XML = 40,

        /// <summary>
        /// Normal text file
        /// </summary>
        /// <remarks>File extension .txt</remarks>
        TXT = 80,

        /// <summary>
        /// Windows Screensaver file
        /// </summary>
        /// <remarks>File extension .scr</remarks>
        SCR = 100,
    }
}
