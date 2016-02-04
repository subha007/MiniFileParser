using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        PE = 1,

        /// <summary>
        /// Windows executable 32 bits
        /// </summary>
        PE32 = PE + 1,

        /// <summary>
        /// Windows executable 64 bits
        /// </summary>
        PE32PLUS = PE + 2,

        /// <summary>
        /// A Dll file type
        /// </summary>
        DLL = 10,

        /// <summary>
        /// An Obj file type
        /// </summary>
        OBJ = 20,

        /// <summary>
        /// An xml file
        /// </summary>
        XML = 40,

        /// <summary>
        /// Normal text file
        /// </summary>
        TXT = 80,

        /// <summary>
        /// Windows Screensaver file
        /// </summary>
        SCR = 100,
    }
}
