using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The PE32 Import Lookup Table.
    /// There is an array of these for each imported DLL. It represents either
    /// the ordinal to import from the target DLL, or a name to lookup and import
    /// from the target DLL.
    /// This also happens to be the same format used by the Import Address Table
    /// when it is initially written out to the image.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ImportLookupTableEntry32
    {
        public uint Data { get; set; }

        /// <summary>
        /// Is this entry specified by ordinal, or name?
        /// </summary>
        public bool IsOrdinal { get { return Convert.ToBoolean(Data & 0x80000000); } }

        /// <summary>
        /// Get the ordinal value of this entry. isOrdinal must be true.
        /// </summary>
        /// <returns></returns>
        public ushort Ordinal
        {
            get
            {
                Debug.Assert(this.IsOrdinal == true);
                return Convert.ToUInt16(this.Data & 0xFFFF);
            }
            set
            {
                this.Data = value | 0x80000000;
            }
        }

    }
}
