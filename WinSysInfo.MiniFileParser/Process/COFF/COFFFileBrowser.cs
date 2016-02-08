using System;
using System.IO;
using WinSysInfo.MiniFileParser.Factory;
using WinSysInfo.MiniFileParser.Helper;
using WinSysInfo.MiniFileParser.Interface;

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
        public IFileReadStrategy ReaderStrategy { get; set; }

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
        public COFFFileBrowser(IFileReaderProperty fileProperty) : this(fileProperty, FileDataStoreFactory.COFFStore()) { }

        /// <summary>
        /// Constructor with fil path and data store
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <param name="store"></param>
        public COFFFileBrowser(string fullFilePath, IFileDataStore store) 
            : this(new FileReaderProperty(fullFilePath), store)
        {
        }

        /// <summary>
        /// Constructor using Direct file path data and store
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="store"></param>
        public COFFFileBrowser(IFileReaderProperty fileProperty, IFileDataStore store) 
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

            IFileReader reader = new COFFBinaryReaderInternal(this.Property, this.Store, this.ReaderStrategy);

            if (reader == null)
                throw new InvalidOperationException("Reader is not initialized");

            reader.Read();
        }

        #endregion Methods
    }
}
