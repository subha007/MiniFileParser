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
            new FileLoader(@"C:\Code\ConsoleApplication1\ConsoleApplication1\bin\Debug\ConsoleApplication1.exe").Read();
        }
    }
}
