using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSysInfo.MiniFileParser.Helper
{
    /// <summary>
    /// This class defines all Constants used in PE image structures
    /// </summary>
    public static class ConstantWinCOFFImage
    {
        public const uint NoOfDirEntries = 16;

        /// <summary>
        /// The maximum number of sections that a COFF object can have (inclusive).
        /// </summary>
        public const uint MaxNoOfSections = 65279;

        /// <summary>
        /// The PE signature bytes that follows the DOS stub header.
        /// </summary>
        public static readonly char[] PEMagic = { 'P', 'E', '\0', '\0' };

        /// <summary>
        /// The COFF MS DOS signature bytes. Hex Value is 0x5a4d
        /// </summary>
        public static readonly char[] MSDOSMagic = { 'M', 'Z' };

        /// <summary>
        /// The Big object magic bytes
        /// </summary>
        public static readonly char[] BigObjMagic = {
          '\xc7', '\xa1', '\xba', '\xd1', '\xee', '\xba', '\xa9', '\x4b',
          '\xaf', '\x20', '\xfa', '\xf6', '\x6a', '\xa4', '\xdc', '\xb8',
        };

        /// <summary>
        /// Minimum version for BigObj
        /// </summary>
        public static readonly uint MinBigObjectVersion = 2;
    }
}
