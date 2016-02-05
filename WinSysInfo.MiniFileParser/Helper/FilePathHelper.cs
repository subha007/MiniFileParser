using System;
using System.IO;
using System.Text.RegularExpressions;

namespace WinSysInfo.MiniFileParser.Helper
{
    /// <summary>
    /// A File IO class helper for extended use
    /// </summary>
    public class FilePathHelper : IDisposable
    {
        #region Constants

        /// <summary>
        /// On Windows-based platforms, paths must be less than 248 characters, and 
        /// file names must be less than 260 characters
        /// </summary>
        internal static readonly uint MaxPath = 259;

        #endregion Constants

        #region Fields

        /// <summary>
        /// In case the file path is more than 260 then we first copy the file as a
        /// temporary file
        /// </summary>
        public bool UsingTempFile { get; protected set; }

        /// <summary>
        /// The temporary file name
        /// </summary>
        public string TempFile { get; protected set; }

        /// <summary>
        /// The full path of the actual file
        /// </summary>
        public string ActualFile { get; protected set; }

        #endregion Fields

        #region Custom Fields

        /// <summary>
        /// The full path of the file in use
        /// </summary>
        public string FileInUse
        {
            get
            {
                if (this.UsingTempFile)
                    return this.TempFile;
                else
                    return this.ActualFile;
            }
        }

        /// <summary>
        /// The file name of the actual file
        /// </summary>
        public string FileName
        {
            get
            {
                FileInfo fi = new FileInfo(this.FileInUse);
                return fi.Name;
            }
        }

        /// <summary>
        /// The file extension of the actual file without the strating dot
        /// </summary>
        public string Extension
        {
            get
            {
                FileInfo fi = new FileInfo(this.ActualFile);
                return fi.Extension.Remove(0);
            }
        }

        /// <summary>
        /// The unique name for the file
        /// </summary>
        public string UniqueName
        {
            get
            {
                if (string.IsNullOrEmpty(this.FileInUse) == true)
                    return System.Guid.NewGuid().ToString();
                else
                    return Regex.Replace(this.FileInUse, @"\\|:|/|\.", string.Empty);
            }
        }

        #endregion Custom Fields

        #region Constructors

        /// <summary>
        /// Basic constructor
        /// </summary>
        public FilePathHelper() : this(string.Empty) { }

        /// <summary>
        /// Constructor with file path
        /// </summary>
        /// <param name="actualfile">The full file path</param>
        public FilePathHelper(string actualfile)
        {
            this.ActualFile = actualfile;
            this.TempFile = string.Empty;
            this.UsingTempFile = false;
        }

        #endregion Constructors

        #region User Public Methods

        /// <summary>
        /// If the file path is more than 260 copy into temp file. Also, if
        /// it is forced to use temp file.
        /// </summary>
        /// <param name="bForceTemp">Force use of temp file</param>
        public bool Rationalize(bool bForceTemp = false)
        {
            if ((bForceTemp == true || string.IsNullOrEmpty(this.ActualFile) == true) ||
                (this.ActualFile.Length > FilePathHelper.MaxPath))
            {
                // Create Temp file and use
                this.TempFile = System.IO.Path.GetTempFileName();
                System.IO.File.Copy(this.ActualFile, this.TempFile, true);
                this.UsingTempFile = true;

                return true;
            }

            if (string.IsNullOrEmpty(this.ActualFile) == true) return false;

            if (File.Exists(this.ActualFile) == false) return false;

            return true;
        }

        #endregion User Public Methods

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
                if (this.UsingTempFile)
                {
                    System.IO.File.Delete(this.TempFile);
                    this.TempFile = string.Empty;
                    this.UsingTempFile = false;
                }
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        /// <summary>
        /// Cleanup for unmanaged resources
        /// </summary>
        ~FilePathHelper()
        {
            Dispose(false);
        }

        #endregion Disposable
    }
}
