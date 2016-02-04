using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSysInfo.MiniFileParser.Helper;

namespace WinSysInfo.MiniFileParser.Interface
{
    /// <summary>
    /// Common interface for all file browser classes
    /// </summary>
    public interface IFileBrowser
    {
        #region Properties

        /// <summary>
        /// The reader used to read the PE File 
        /// </summary>
        IFileReadStrategy ReaderStrategy { get; set; }

        /// <summary>
        /// Get or set the file parsing property
        /// </summary>
        IFileReaderProperty Property { get; set; }

        /// <summary>
        /// Get or set the data store
        /// </summary>
        IFileDataStore Store { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Read the file
        /// </summary>
        /// <returns>In case during file reading it is found that a finer class exists 
        /// then create and return that file browser class or else return itself</returns>
        void Read();
        
        #endregion Methods
    }
}
