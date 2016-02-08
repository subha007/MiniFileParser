using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using WinSysInfo.MiniFileParser.Interface;
using WinSysInfo.MiniFileParser.Model;

namespace WinSysInfo.MiniFileParser.Process
{
    /// <summary>
    /// Represents a randomly accessed view of a memory-mapped file
    /// </summary>
    public class MemoryRandomAccess : IFileReadStrategy, IDisposable
    {
        #region Main Properties

        /// <summary>
        /// In .NET 4 + this is the best method
        /// </summary>
        private MemoryMappedFile MemoryFile { get; set; }

        /// <summary>
        /// Represents a randomly accessed view of a memory-mapped file.
        /// </summary>
        protected MemoryMappedViewAccessor Accessor { get; set; }

        /// <summary>
        /// Reference to the Reader property passed from the file parser object
        /// </summary>
        public IFileReaderProperty ReaderProperty { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="readerProperty"></param>
        public MemoryRandomAccess(IFileReaderProperty readerProperty)
        {
            if (readerProperty == null) throw new ArgumentNullException("readerProperty");
            if (readerProperty.TryValidate() == false) throw new InvalidOperationException("readerProperty has invalid data");
            this.ReaderProperty = readerProperty;

            this.MemoryFile = MemoryMappedFile.CreateFromFile(readerProperty.FilePath.FileInUse, FileMode.Open, readerProperty.FilePath.UniqueName);
            this.fileOffset = 0;
        }

        #endregion

        #region OpenClose

        /// <summary>
        /// Get or set random access is open
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// Open the random accessor
        /// </summary>
        public void Open()
        {
            if (this.IsOpen == false)
                this.Accessor = this.MemoryFile.CreateViewAccessor(this.ReaderProperty.OffsetOfFile, this.ReaderProperty.SizeOfReader);

            this.IsOpen = true;
        }

        /// <summary>
        /// Close random access
        /// </summary>
        public void Close()
        {
            if (this.Accessor != null)
            {
                this.Accessor.Dispose();
                this.Accessor = null;
            }
        }

        /// <summary>
        /// Get or set the file offset for which this intermediate reader is created
        /// </summary>
        private long fileOffset;
        public long FileOffset
        {
            get { return fileOffset; }
        }

        #endregion

        #region Peek

        /// <summary>
        /// Peek ahead bytes but do not chnage the seek pointer in sequential access
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <param name="count">The number of bytes to read. Default is 1.</param>
        /// <returns>A byte array</returns>
        public byte[] PeekBytes(int count = 1, long position = 0)
        {
            using (MemoryMappedViewStream tempPeek =
                this.MemoryFile.CreateViewStream(position, count, MemoryMappedFileAccess.Read))
            {
                if (tempPeek != null && count > 0)
                {
                    byte[] iodata = new byte[count];
                    tempPeek.Read(iodata, (int)0, count);
                    return iodata;
                }
            }

            return null;
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

            using (MemoryMappedViewAccessor tempPeek = this.MemoryFile.CreateViewAccessor(position,
                count, MemoryMappedFileAccess.Read))
            {
                TLayoutType fileData;
                tempPeek.Read(position, out fileData);
                model.SetData(fileData);
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

        }

        /// <summary>
        /// Seek file pointer to position
        /// </summary>
        /// <param name="position"></param>
        public void SeekOriginal(long position)
        {
            // Seek Position from origin

        }

        #endregion Seek

        #region Reader

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
        /// <typeparam name="T">The Layout Model value Type</typeparam>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <param name="model">The structure to contain the read data</param>
        public void ReadLayout<TLayoutType>(LayoutModel<TLayoutType> model, long position)
            where TLayoutType : struct
        {
            if (this.Accessor != null)
            {
                TLayoutType fileData;
                this.Accessor.Read<TLayoutType>(position, out fileData);
                model.SetData(fileData);
            }
        }

        /// <summary>
        /// Read bytes
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0.</param>
        /// <param name="count">The number of bytes to read. Default is 1.</param>
        /// <returns>A byte array</returns>
        public byte[] ReadBytes(int count = 1, long position = 0)
        {
            if (this.Accessor != null && count > 0)
            {
                byte[] iodata = new byte[count];
                this.Accessor.ReadArray(position, iodata, (int)0, count);
                return iodata;
            }

            return null;
        }

        /// <summary>
        /// Read boolean data
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0.</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public bool? ReadBoolean(long position = 0)
        {
            if (this.Accessor != null)
                return this.Accessor.ReadBoolean(position);
            return null;
        }

        /// <summary>
        /// Reads a byte value from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0.</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public byte? ReadByte(long position = 0)
        {
            if (this.Accessor != null)
                return this.Accessor.ReadByte(position);
            return null;
        }

        /// <summary>
        /// Reads a character from the accessor.
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0.</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public char? ReadChar(long position = 0)
        {
            if (this.Accessor != null)
                return this.Accessor.ReadChar(position);
            return null;
        }

        /// <summary>
        /// Reads a decimal value from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0.</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public decimal? ReadDecimal(long position = 0)
        {
            if (this.Accessor != null)
                return this.Accessor.ReadDecimal(position);
            return null;
        }

        /// <summary>
        /// Reads a double-precision floating-point value from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0.</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public double? ReadDouble(long position = 0)
        {
            if (this.Accessor != null)
                return this.Accessor.ReadDouble(position);
            return null;
        }

        /// <summary>
        /// Reads a 16-bit integer from the accessor.
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0.</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public short? ReadInt16(long position = 0)
        {
            if (this.Accessor != null)
                return this.Accessor.ReadInt16(position);
            return null;
        }

        /// <summary>
        /// Reads a 32-bit integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0.</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public int? ReadInt32(long position = 0)
        {
            if (this.Accessor != null)
                return this.Accessor.ReadInt32(position);
            return null;
        }

        /// <summary>
        /// Reads a 64-bit integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0.</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public long? ReadInt64(long position = 0)
        {
            if (this.Accessor != null)
                return this.Accessor.ReadInt64(position);
            return null;
        }

        /// <summary>
        /// Reads an 8-bit signed integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0.</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public sbyte? ReadSByte(long position = 0)
        {
            if (this.Accessor != null)
                return this.Accessor.ReadSByte(position);
            return null;
        }

        /// <summary>
        /// Reads a single-precision floating-point value from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0.</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public float? ReadSingle(long position = 0)
        {
            if (this.Accessor != null)
                return this.Accessor.ReadSingle(position);
            return null;
        }

        /// <summary>
        /// Reads an unsigned 16-bit integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0.</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public ushort? ReadUInt16(long position = 0)
        {
            if (this.Accessor != null)
                return this.Accessor.ReadUInt16(position);
            return null;
        }

        /// <summary>
        /// Reads an unsigned 32-bit integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0.</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public uint? ReadUInt32(long position = 0)
        {
            if (this.Accessor != null)
                return this.Accessor.ReadUInt32(position);
            return null;
        }

        /// <summary>
        /// Reads an unsigned 64-bit integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0.</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        public ulong? ReadUInt64(long position = 0)
        {
            if (this.Accessor != null)
                return this.Accessor.ReadUInt64(position);
            return null;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Destructor
        /// </summary>
        ~MemoryRandomAccess()
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
