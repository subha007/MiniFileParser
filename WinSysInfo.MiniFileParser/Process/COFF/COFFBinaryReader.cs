using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSysInfo.MiniFileParser.Helper;
using WinSysInfo.MiniFileParser.Interface;

namespace WinSysInfo.MiniFileParser.Process
{
    internal class COFFBinaryReader : IPECOFFFileReader
    {
        #region Properties

        /// <summary>
        /// Get or set the full file path to parse
        /// </summary>
        internal IFileReaderProperty FilePath { get; set; }

        /// <summary>
        /// Get or set the data store
        /// </summary>
        internal IFileDataStore Store { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        internal COFFBinaryReader(IFileReaderProperty property, IFileDataStore store)
        {
            this.FilePath = property;
            this.Store = store;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Check if the file has PE signature
        /// </summary>
        /// <returns></returns>
        bool HasPEHeader();

        /// <summary>
        /// Read MS DOS Header
        /// </summary>
        void ReadMSDOSHeader();

        /// <summary>
        /// Read MS DOS stub
        /// </summary>
        void ReadMSDOSStub();

        /// <summary>
        /// Check the PE magic bytes. ("PE\0\0") 
        /// </summary>
        /// <returns></returns>
        bool ContainsPEMagicBytes();

        /// <summary>
        /// Initialize NT Header
        /// </summary>
        void ReadNTHeader();

        /// <summary>
        /// Seek through COFF Header
        /// </summary>
        void ReadCOFFHeader();

        /// <summary>
        /// Seek through COFF Header
        /// </summary>
        void ReadCOFFBigObjHeader();

        /// <summary>
        /// It might be a bigobj file, let's check.  Note that COFF bigobj and COFF
        /// </summary>
        void CheckAndMaintainBigObjHeader();

        /// <summary>
        /// Initialize optional header standard fields
        /// </summary>
        void ReadOptHeaderStdFields();

        /// <summary>
        /// Initialize optional header standard fields
        /// </summary>
        void ReadOptHeaderStdFields32();

        /// <summary>
        /// Initialize optional header standard fields
        /// </summary>
        void ReadOptHeaderSpecificFields32();

        /// <summary>
        /// Initialize optional header standard fields
        /// </summary>
        void ReadOptHeaderSpecificFields32Plus();

        /// <summary>
        /// Get Data Directory count
        /// </summary>
        /// <returns></returns>
        void CalculateDataDirectoryCount();

        /// <summary>
        /// Each data directory gives the address and size of a table or string that Windows uses.
        /// These data directory entries are all loaded into memory so that the system can use them
        /// at run time. A data directory is an 8byte field.
        /// </summary>
        void ReadOptHeaderDataDirectoriesImageOnly();

        #endregion Methods
    }
}
