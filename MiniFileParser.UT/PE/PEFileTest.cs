using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinSysInfo.MiniFileParser.Process;

namespace MiniFileParser.UT
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BasicFileLoader()
        {
            // Call File Loader
            // Argument : File Path
            // Method : Read
            new FileLoader(@"C:\TEMP\ConsoleApplication9.exe").Read();
        }
    }
}
