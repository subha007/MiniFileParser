using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSysInfo.MiniFileParser.Helper;
using WinSysInfo.MiniFileParser.Model;

namespace WinSysInfo.MiniFileParser.Interface
{
    /// <summary>
    /// An interface used to define the set of properties used to parse a file
    /// </summary>
    public interface IFileReaderProperty : IDisposable
    {
        #region Properties

        /// <summary>
        /// Get or set the binary reader type
        /// </summary>
        EnumReaderType ReaderType { get; set; }

        /// <summary>
        /// Get or set the file path
        /// </summary>
        FilePathHelper FilePath { get; set; }

        /// <summary>
        /// The offset in the file from which to create file reader
        /// </summary>
        long OffsetOfFile { get; }

        /// <summary>
        /// The size of file from offset to create a reader
        /// </summary>
        long SizeOfReader { get; }

        #endregion Properties

        #region Disposable

        /// <summary>
        /// Validate the data
        /// </summary>
        /// <returns></returns>
        bool TryValidate();

        #endregion Disposable
    }
}
