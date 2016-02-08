using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using WinSysInfo.MiniFileParser.Helper;
using WinSysInfo.MiniFileParser.Interface;
using WinSysInfo.MiniFileParser.Model;

namespace WinSysInfo.MiniFileParser.Process
{
    public class MemorySequentialAccess : IFileReadStrategy, IDisposable
    {
        #region Main Properties

        /// <summary>
        /// In .NET 4 + this is the best method
        /// </summary>
        private MemoryMappedFile MemoryFile { get; set; }

        /// <summary>
        /// Use the stream returned by this method for sequential access to a memory-mapped file, 
        /// such as for inter-process communications.
        /// </summary>
        protected MemoryMappedViewStream IoAccess { get; set; }

        /// <summary>
        /// Reference to the Reader property passed from the file parser object
        /// </summary>
        public IFileReaderProperty ReaderProperty { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="memoryFile"></param>
        public MemorySequentialAccess(IFileReaderProperty readerProperty)
        {
            if (readerProperty == null) throw new ArgumentNullException("readerProperty");
            if (readerProperty.TryValidate() == false) throw new InvalidOperationException("readerProperty has invalid data");
            this.ReaderProperty = readerProperty;

            this.MemoryFile = MemoryMappedFile.CreateFromFile(readerProperty.FilePath.FileInUse, FileMode.Open, readerProperty.FilePath.UniqueName);
        }

        #endregion

        #region Private

        /// <summary>
        /// Set file offset
        /// </summary>
        /// <param name="offset"></param>
        private void SetFileOffset(long offset)
        {
            this.IoAccess.Seek(offset, System.IO.SeekOrigin.Begin);
        }

        #endregion

        #region OpenClose

        /// <summary>
        /// Open the random accessor
        /// </summary>
        public void Open()
        {
            if (this.ReaderProperty == null) throw new ArgumentNullException("The Reader property is null and must be initialized");

            if (this.IsOpen == false)
                this.IoAccess = this.MemoryFile.CreateViewStream(
                    ReaderProperty.OffsetOfFile
                    , ReaderProperty.SizeOfReader
                    , MemoryMappedFileAccess.Read);

            this.IsOpen = true;
        }

        /// <summary>
        /// Close random access
        /// </summary>
        public void Close()
        {
            if (this.IoAccess != null)
            {
                this.IoAccess.Close();
                this.IoAccess.Dispose();
                this.IoAccess = null;
            }
        }

        /// <summary>
        /// Get or set random access is open
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// Get or set the file offset for which this intermediate reader is created
        /// </summary>
        public long FileOffset
        {
            get { return (this.IoAccess != null) ? this.IoAccess.Position : 0; }
        }

        #endregion

        #region Peek

        /// <summary>
        /// Peek ahead bytes but do not chnage the seek pointer in sequential access and read
        /// independently of current file position
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <param name="count">The number of bytes to read. Default 1.</param>
        /// <returns>A byte array</returns>
        public byte[] PeekBytes(int count = 1, long position = 0)
        {
            if (position < 0) position = this.FileOffset;
            using (MemoryMappedViewStream tempPeek =
                this.MemoryFile.CreateViewStream(position, count, MemoryMappedFileAccess.Read))
            {
                byte[] iodata = new byte[count];
                tempPeek.Read(iodata, (int)0, count);
                return iodata;
            }
        }

        /// <summary>
        /// Peek ahead bytes but do not chnage the seek pointer in sequential access
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <param name="count">The number of bytes to read. Default 1.</param>
        /// <returns>A byte array</returns>
        public LayoutModel<TLayoutType> PeekStructure<TLayoutType>(int count = 1, long position = 0)
            where TLayoutType : struct
        {
            LayoutModel<TLayoutType> model = new LayoutModel<TLayoutType>();

            using (MemoryMappedViewStream tempPeek = this.MemoryFile.CreateViewStream(position,
                count, MemoryMappedFileAccess.Read))
            {
                MemorySequentialAccess.ReadLayoutHelper(tempPeek, model, position);
            }

            return model;
        }

        /// <summary>
        /// Peek ahead ushort but do not chnage the seek pointer in sequential access
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        public ushort PeekUShort(long position = 0)
        {
            byte[] bData = PeekBytes(2, position);
            return BitConverter.ToUInt16(bData, 0);
        }

        /// <summary>
        /// Peek ahead uint but do not chnage the seek pointer in sequential access
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        public uint PeekUInt(long position = 0)
        {
            byte[] bData = PeekBytes(4, position);
            return BitConverter.ToUInt32(bData, 0);
        }

        #endregion

        #region Seek

        /// <summary>
        /// Seek file pointer to position
        /// </summary>
        /// <param name="position"></param>
        public void SeekForward(long position)
        {
            // Seek Position from current
            this.IoAccess.Seek(position, System.IO.SeekOrigin.Current);
        }

        /// <summary>
        /// Seek file pointer to position
        /// </summary>
        /// <param name="position"></param>
        public void SeekOriginal(long position)
        {
            // Seek Position from origin
            this.IoAccess.Seek(position, System.IO.SeekOrigin.Begin);
        }

        #endregion Seek

        #region Reader With Position

        /// <summary>
        /// Read a layout model
        /// </summary>
        /// <typeparam name="T">The Layout Model value Type</typeparam>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The structure to contain the read data</returns>
        public LayoutModel<TLayoutType> ReadLayout<TLayoutType>(long position = 0)
            where TLayoutType : struct
        {
            LayoutModel<TLayoutType> model = new LayoutModel<TLayoutType>();
            this.ReadLayout<TLayoutType>(model, position);

            return model;
        }

        /// <summary>
        /// Read a layout model
        /// </summary>
        /// <typeparam name="TLayoutType">The Layout Model value Type</typeparam>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <param name="model">The structure to contain the read data</param>
        public void ReadLayout<TLayoutType>(LayoutModel<TLayoutType> model, long position = 0)
            where TLayoutType : struct
        {
            MemorySequentialAccess.ReadLayoutHelper(this.IoAccess, model, position);
        }

        private static void ReadLayoutHelper<TLayoutType>(MemoryMappedViewStream viewStream
            , LayoutModel<TLayoutType> model
            , long position = 0)
            where TLayoutType : struct
        {
            if (viewStream != null)
            {
                // Seek Position from current
                viewStream.Seek(position, System.IO.SeekOrigin.Current);

                byte[] bytes = ReadBytes(viewStream, (int)LayoutModel<TLayoutType>.DataSize, position);

                GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                model.SetData(Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(TLayoutType)));
                handle.Free();
            }
        }

        /// <summary>
        /// Read bytes
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <param name="count">The number of bytes to read. Default is 1.</param>
        /// <returns>A byte array</returns>
        public byte[] ReadBytes(int count = 1, long position = 0)
        {
            if (this.IoAccess != null && count > 0)
            {
                // Seek Position from current
                this.IoAccess.Seek(position, System.IO.SeekOrigin.Current);

                byte[] iodata = new byte[count];
                this.IoAccess.Read(iodata, (int)0, count);
                return iodata;
            }

            return null;
        }

        /// <summary>
        /// Read bytes
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <param name="viewStream"></param>
        /// <param name="count">The number of bytes to read. Default is 1.</param>
        /// <returns>A byte array</returns>
        private static byte[] ReadBytes(MemoryMappedViewStream viewStream,
            int count = 1, long position = 0)
        {
            if (viewStream != null && count > 0)
            {
                // Seek Position from current
                viewStream.Seek(position, System.IO.SeekOrigin.Current);

                byte[] iodata = new byte[count];
                viewStream.Read(iodata, (int)0, count);
                return iodata;
            }

            return null;
        }

        /// <summary>
        /// Read boolean data
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public bool? ReadBoolean(long position = 0)
        {
            // Seek Position from current
            this.IoAccess.Seek(position, System.IO.SeekOrigin.Current);

            byte[] iodata = ReadBytes(1, position);
            if (iodata != null)
                return BitConverter.ToBoolean(iodata, 0);

            return null;
        }

        /// <summary>
        /// Reads a byte value from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public byte? ReadByte(long position = 0)
        {
            // Seek Position from current
            this.IoAccess.Seek(position, System.IO.SeekOrigin.Current);

            byte[] iodata = ReadBytes(1, position);
            if (iodata != null)
                return iodata[0];

            return null;
        }

        /// <summary>
        /// Reads a character from the accessor.
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public char? ReadChar(long position = 0)
        {
            // Seek Position from current
            this.IoAccess.Seek(position, System.IO.SeekOrigin.Current);

            byte[] iodata = ReadBytes(1, position);
            if (iodata != null)
                return BitConverter.ToChar(iodata, 0);

            return null;
        }

        /// <summary>
        /// Reads a decimal value from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public decimal? ReadDecimal(long position = 0)
        {
            // Seek Position from current
            this.IoAccess.Seek(position, System.IO.SeekOrigin.Current);

            byte[] iodata = ReadBytes(16, position);
            if (iodata != null)
                return BitConverterExtended.ToDecimal(iodata);

            return null;
        }

        /// <summary>
        /// Reads a double-precision floating-point value from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public double? ReadDouble(long position = 0)
        {
            // Seek Position from current
            this.IoAccess.Seek(position, System.IO.SeekOrigin.Current);

            byte[] iodata = ReadBytes(8, position);
            if (iodata != null)
                return BitConverter.ToDouble(iodata, 0);

            return null;
        }

        /// <summary>
        /// Reads a 16-bit integer from the accessor.
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public short? ReadInt16(long position = 0)
        {
            // Seek Position from current
            this.IoAccess.Seek(position, System.IO.SeekOrigin.Current);

            byte[] iodata = ReadBytes(2, position);
            if (iodata != null)
                return BitConverter.ToInt16(iodata, 0);

            return null;
        }

        /// <summary>
        /// Reads a 32-bit integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public int? ReadInt32(long position = 0)
        {
            // Seek Position from current
            this.IoAccess.Seek(position, System.IO.SeekOrigin.Current);

            byte[] iodata = ReadBytes(4, position);
            if (iodata != null)
                return BitConverter.ToInt32(iodata, 0);

            return null;
        }

        /// <summary>
        /// Reads a 64-bit integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public long? ReadInt64(long position = 0)
        {
            // Seek Position from current
            this.IoAccess.Seek(position, System.IO.SeekOrigin.Current);

            byte[] iodata = ReadBytes(8, position);
            if (iodata != null)
                return BitConverter.ToInt64(iodata, 0);

            return null;
        }

        /// <summary>
        /// Reads an 8-bit signed integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public sbyte? ReadSByte(long position = 0)
        {
            // Seek Position from current
            this.IoAccess.Seek(position, System.IO.SeekOrigin.Current);

            byte[] iodata = ReadBytes(1, position);
            if (iodata != null)
                return Convert.ToSByte(iodata[0]);

            return null;
        }

        /// <summary>
        /// Reads a single-precision floating-point value from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public float? ReadSingle(long position = 0)
        {
            // Seek Position from current
            this.IoAccess.Seek(position, System.IO.SeekOrigin.Current);

            byte[] iodata = ReadBytes(4, position);
            if (iodata != null)
                return BitConverter.ToSingle(iodata, 0);

            return null;
        }

        /// <summary>
        /// Reads an unsigned 16-bit integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public ushort? ReadUInt16(long position = 0)
        {
            // Seek Position from current
            this.IoAccess.Seek(position, System.IO.SeekOrigin.Current);

            byte[] iodata = ReadBytes(2, position);
            if (iodata != null)
                return BitConverter.ToUInt16(iodata, 0);

            return null;
        }

        /// <summary>
        /// Reads an unsigned 32-bit integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public uint? ReadUInt32(long position = 0)
        {
            // Seek Position from current
            this.IoAccess.Seek(position, System.IO.SeekOrigin.Current);

            byte[] iodata = ReadBytes(4, position);
            if (iodata != null)
                return BitConverter.ToUInt32(iodata, 0);

            return null;
        }

        /// <summary>
        /// Reads an unsigned 64-bit integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public ulong? ReadUInt64(long position = 0)
        {
            // Seek Position from current
            this.IoAccess.Seek(position, System.IO.SeekOrigin.Current);

            byte[] iodata = ReadBytes(4, position);
            if (iodata != null)
                return BitConverter.ToUInt64(iodata, 0);

            return null;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Destructor
        /// </summary>
        ~MemorySequentialAccess()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose wrapper. Actual dispose method.
        /// </summary>
        /// <param name="itIsSafeToAlsoFreeManagedObjects"></param>
        protected virtual void Dispose(Boolean itIsSafeToAlsoFreeManagedObjects)
        {
            // Free unmanaged resources

            // Free managed resources if applicable
            if (itIsSafeToAlsoFreeManagedObjects == true)
            {
                this.Close();

                if (this.MemoryFile != null)
                    this.MemoryFile.Dispose();

                if (this.ReaderProperty != null)
                {
                    this.ReaderProperty.Dispose();
                }
            }
        }

        /// <summary>
        /// Interface implementation
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
