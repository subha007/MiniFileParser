using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSysInfo.MiniFileParser.Helper;
using WinSysInfo.MiniFileParser.Interface;
using WinSysInfo.MiniFileParser.Model;

namespace WinSysInfo.MiniFileParser.Process
{
    /// <summary>
    /// The Class which stores the file reader property data
    /// </summary>
    public class FileReaderProperty : IFileReaderProperty
    {
        #region Properties

        /// <summary>
        /// Get or set the binary reader type
        /// </summary>
        public EnumReaderType ReaderType { get; set; }

        /// <summary>
        /// Get or set the file path
        /// </summary>
        public FilePathHelper FilePath { get; set; }

        /// <summary>
        /// The offset in the file from which to create file reader
        /// </summary>
        public long OffsetOfFile { get; protected set; }

        /// <summary>
        /// The size of file from offset to create a reader
        /// </summary>
        public long SizeOfReader { get; protected set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public FileReaderProperty()
            : this(string.Empty
            , EnumReaderType.MEMORY_SEQ_ACCESS
            , 0
            , 0)
        { }

        /// <summary>
        /// Default constructor
        /// </summary>
        public FileReaderProperty(string fullFilePath)
            : this(fullFilePath
            , EnumReaderType.MEMORY_SEQ_ACCESS
            , 0
            , 0)
        { }

        /// <summary>
        /// Default constructor
        /// </summary>
        public FileReaderProperty(string fullFilePath
            , EnumReaderType readerType
            , long offset
            , long size)
        {
            this.FilePath = new FilePathHelper(fullFilePath);
            this.FilePath.Rationalize();

            this.ReaderType = readerType;
            this.OffsetOfFile = offset;
            this.SizeOfReader = size;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Validate the data
        /// </summary>
        /// <returns></returns>
        public bool TryValidate()
        {
            return this.FilePath.Rationalize();
        }

        #endregion Methods

        #region Disposable

        /// <summary>
        /// Flag to check if already disposed or not
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Disposable interface method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// All object references and unmanaged resources are released in this method.
        /// </summary>
        /// <param name="disposing">the argument indicates whether or not the Dispose
        /// method should be called on any managed object references.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // free other managed objects
                this.FilePath.Dispose();
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        /// <summary>
        /// Cleanup for unmanaged resources
        /// </summary>
        ~FileReaderProperty()
        {
            Dispose(false);
        }

        #endregion Disposable
    }
}
