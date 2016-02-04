﻿using System;
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
        public static IFileBrowser GetFileBrowser(EnumFileType fileType, FileReaderProperty fileProperty)
        {
            switch(fileType)
            {
                case EnumFileType.PE:
                    return new COFFFileBrowser(fileProperty);

                default:
                    return null;
            }
        }
    }
}
