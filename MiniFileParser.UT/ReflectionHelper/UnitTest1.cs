using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinSysInfo.ReflectionHelper.Process;
using WinSysInfo.ReflectionHelper.Model;

namespace MiniFileParser.UT.ReflectionHelper
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            RuntimeAssemblyProcess assemblyProc = new RuntimeAssemblyProcess("PECOFFDataStructures.xml", EnumXmlFilePathType.FILE_PARSE);
        }
    }
}
