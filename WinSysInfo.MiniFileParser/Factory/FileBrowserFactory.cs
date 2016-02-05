using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSysInfo.MiniFileParser.Interface;
using WinSysInfo.MiniFileParser.Model;
using WinSysInfo.MiniFileParser.Process;

namespace WinSysInfo.MiniFileParser.Factory
{
    /// <summary>
    /// The class which dteremines file browser class type
    /// </summary>
    public class FileBrowserFactory
    {
        public static IFileBrowser GetFileBrowser(IFileReaderProperty fileProperty)
        {
            switch (fileProperty.FileType)
            {
                case EnumFileType.EXE:
                    return new COFFFileBrowser(fileProperty);

                default:
                    return null;
            }
        }
    }
}
