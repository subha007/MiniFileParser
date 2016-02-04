using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSysInfo.MiniFileParser.Factory;
using WinSysInfo.MiniFileParser.Helper;
using WinSysInfo.MiniFileParser.Interface;
using WinSysInfo.MiniFileParser.Model;

namespace WinSysInfo.MiniFileParser.Process
{
    /// <summary>
    /// This class parses and browses the PE File format
    /// </summary>
    public class COFFFileBrowser : IFileBrowser
    {
        #region Properties

        /// <summary>
        /// The reader used to read the PE File 
        /// </summary>
        protected IFileReadStrategy ReaderStrategy { get; set; }

        /// <summary>
        /// Get or set the file parsing property
        /// </summary>
        public IFileReaderProperty Property { get; set; }

        /// <summary>
        /// Get or set the data store
        /// </summary>
        public IFileDataStore Store { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public COFFFileBrowser() { }

        /// <summary>
        /// Constructor with file path
        /// </summary>
        /// <param name="fileFullPath"></param>
        public COFFFileBrowser(string fileFullPath) : this(fileFullPath, FileDataStoreFactory.COFFStore()) { }

        /// <summary>
        /// Default constructor
        /// </summary>
        public COFFFileBrowser(FileReaderProperty fileProperty) : this(fileProperty, FileDataStoreFactory.COFFStore()) { }

        /// <summary>
        /// Constructor with fil path and data store
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <param name="store"></param>
        public COFFFileBrowser(string fullFilePath, COFFFileDataStore store) 
            : this(new FileReaderProperty(fullFilePath), store)
        {
        }

        /// <summary>
        /// Constructor using Direct file path data and store
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="store"></param>
        public COFFFileBrowser(FileReaderProperty fileProperty, COFFFileDataStore store) 
        {
            this.Property = fileProperty;
            this.Store = store;
            this.ReaderStrategy = FactoryFileReadStrategy.Instance(this.Property);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Read the file
        /// </summary>
        /// <returns>In case during file reading it is found that a finer class exists 
        /// then create and return that file browser class or else return itself</returns>
        public void Read()
        {
            if (this.Property == null) throw new ArgumentNullException();
            if (this.Store == null) throw new ArgumentNullException();

            if (this.Property.FilePath == null ||
               StringExHelper.IsNullOrEmptyOrWhiteSpace(this.Property.FilePath.FileInUse) == true)
                throw new ArgumentNullException("File Path", "File Path must be valid");

            if (File.Exists(this.Property.FilePath.FileInUse) == false)
                throw new FileNotFoundException(this.Property.FilePath.FileInUse);

            IBinaryFileReader reader = new COFFBinaryReader(this.Property, this.Store);

            if (reader == null)
                throw new InvalidOperationException("Reader is not initialized");

            Parse(reader);
        }

        /// <summary>
        /// Start parsing of file
        /// </summary>
        /// <param name="reader"></param>
        protected void Parse(IBinaryFileReader reader)
        {
            // Validate
        }

        #endregion Methods
    }
}
