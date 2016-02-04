using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSysInfo.MiniFileParser.Helper;
using WinSysInfo.MiniFileParser.Model;

namespace WinSysInfo.MiniFileParser.Factory
{
    /// <summary>
    /// A single helper class to determine the type of file
    /// </summary>
    public class FileTypeFactory
    {
        /// <summary>
        /// Get the type of file from the file extension
        /// </summary>
        /// <param name="extension">The file extension</param>
        /// <returns></returns>
        public static EnumFileType GetFileType(string extension)
        {
            if (StringExHelper.IsNullOrEmptyOrWhiteSpace(extension) == true)
                throw new ArgumentNullException("Extesnion", "File extesnion cannot be null.");

            switch(extension.ToLower())
            {
                case "txt":
                    return EnumFileType.TXT;
                case "exe":
                    return EnumFileType.PE;
                case "dll":
                    return EnumFileType.DLL;
                case "obj":
                    return EnumFileType.OBJ;
                case "scr":
                    return EnumFileType.SCR;
                default:
                    return EnumFileType.NONE;
            }
        }
    }
}
