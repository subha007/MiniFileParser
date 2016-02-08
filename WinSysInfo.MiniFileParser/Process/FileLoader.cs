using System;
using WinSysInfo.MiniFileParser.Factory;
using WinSysInfo.MiniFileParser.Interface;

namespace WinSysInfo.MiniFileParser.Process
{
    /// <summary>
    /// Use this class to determine the type of file and the Reader to use in
    /// case you are not sure and want the application to determine by itself.
    /// It is adviced to use this class as much as possible than using the File
    /// parser class directly.
    /// At present this class determines the type of file from the file 
    /// extension.
    /// </summary>
    public class FileLoader
    {
        #region Properties

        /// <summary>
        /// Get or set the file parsing property
        /// </summary>
        public IFileReaderProperty Property { get; set; }

        /// <summary>
        /// The File browser
        /// </summary>
        protected IFileBrowser Browser { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor. Doesnot do anything
        /// </summary>
        public FileLoader() { }

        /// <summary>
        /// Constructor to initialize the Loader type using ful lfile path
        /// </summary>
        /// <param name="fullFilePath">The full file path to parse</param>
        public FileLoader(string fullFilePath) : this(fullFilePath, false) { }

        /// <summary>
        /// Constructor to initialize the Loader type using ful lfile path
        /// </summary>
        /// <param name="fullFilePath">The full file path to parse</param>
        /// <param name="useTempLocation">If true then copy it in temporary file location.</param>
        public FileLoader(string fullFilePath, bool useTempLocation)
        {
            this.Property = new FileReaderProperty(fullFilePath);
            this.Browser = FileBrowserFactory.GetFileBrowser(this.Property);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Read the file
        /// </summary>
        public void Read()
        {
            if (this.Browser == null)
                throw new ArgumentNullException("Browser", "No proper file browser is defined");

            this.Browser.Read();
        }

        #endregion Methods
    }
}
