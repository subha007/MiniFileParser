using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSysInfo.MiniFileParser.Model;

namespace WinSysInfo.MiniFileParser.Interface
{
    /// <summary>
    /// This interface defines a strategy to read a file.
    /// </summary>
    public interface IFileReadStrategy
    {
        #region OpenClose

        /// <summary>
        /// Open a partial section of the file for read from the offset and size
        /// </summary>
        void Open();

        /// <summary>
        /// Close file
        /// </summary>
        void Close();

        /// <summary>
        /// Get or set if file is open
        /// </summary>
        bool IsOpen { get; set; }

        /// <summary>
        /// Get or set the file offset for which this intermediate reader is created
        /// </summary>
        long FileOffset { get; }

        #endregion

        #region Peek

        /// <summary>
        /// Peek ahead bytes but do not chnage the seek pointer in sequential access
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>A byte array</returns>
        byte[] PeekBytes(int count, long position = 0);

        /// <summary>
        /// Peek ahead bytes but do not chnage the seek pointer in sequential access
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <param name="count">The number of bytes to read. Default 1.</param>
        /// <returns>A byte array</returns>
        LayoutModel<TLayoutType> PeekStructure<TLayoutType>(int count = 1, long position = 0)
            where TLayoutType : struct;

        /// <summary>
        /// Peek ahead ushort but do not chnage the seek pointer in sequential access
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        ushort PeekUShort(long position = 0);

        /// <summary>
        /// Peek ahead uint but do not chnage the seek pointer in sequential access
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        uint PeekUInt(long position = 0);

        #endregion

        #region Seek

        /// <summary>
        /// Seek file pointer to position
        /// </summary>
        /// <param name="position"></param>
        void SeekForward(long position);

        /// <summary>
        /// Seek file pointer to position
        /// </summary>
        /// <param name="position"></param>
        void SeekOriginal(long position);

        #endregion Seek

        #region Reader With Position

        /// <summary>
        /// Read a layout model
        /// </summary>
        /// <typeparam name="TLayoutType">The Layout Model value Type</typeparam>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The structure to contain the read data</returns>
        LayoutModel<TLayoutType> ReadLayout<TLayoutType>(long position = 0)
            where TLayoutType : struct;

        /// <summary>
        /// Read a layout model
        /// </summary>
        /// <typeparam name="T">The Layout Model value Type</typeparam>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <param name="model">The structure to contain the read data</param>
        void ReadLayout<TLayoutType>(LayoutModel<TLayoutType> model, long position = 0)
            where TLayoutType : struct;

        /// <summary>
        /// Read bytes
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>A byte array</returns>
        byte[] ReadBytes(int count = 1, long position = 0);

        /// <summary>
        /// Read boolean data
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        bool? ReadBoolean(long position = 0);

        /// <summary>
        /// Reads a byte value from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        byte? ReadByte(long position = 0);

        /// <summary>
        /// Reads a character from the accessor.
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        char? ReadChar(long position = 0);

        /// <summary>
        /// Reads a decimal value from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        decimal? ReadDecimal(long position = 0);

        /// <summary>
        /// Reads a double-precision floating-point value from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        double? ReadDouble(long position = 0);

        /// <summary>
        /// Reads a 16-bit integer from the accessor.
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        short? ReadInt16(long position = 0);

        /// <summary>
        /// Reads a 32-bit integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        int? ReadInt32(long position = 0);

        /// <summary>
        /// Reads a 64-bit integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        long? ReadInt64(long position = 0);

        /// <summary>
        /// Reads an 8-bit signed integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        sbyte? ReadSByte(long position = 0);

        /// <summary>
        /// Reads a single-precision floating-point value from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        float? ReadSingle(long position = 0);

        /// <summary>
        /// Reads an unsigned 16-bit integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        ushort? ReadUInt16(long position = 0);

        /// <summary>
        /// Reads an unsigned 32-bit integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        uint? ReadUInt32(long position = 0);

        /// <summary>
        /// Reads an unsigned 64-bit integer from the accessor
        /// </summary>
        /// <param name="position">The position in the file at which to begin reading
        /// relative to the current position in the file. Default is 0</param>
        /// <returns>The value that was read or null if cannot read.</returns>
        ulong? ReadUInt64(long position = 0);

        #endregion
    }
}
