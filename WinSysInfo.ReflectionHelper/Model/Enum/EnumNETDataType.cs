using System;
using System.Runtime.InteropServices;

namespace WinSysInfo.ReflectionHelper.Model
{
    [Serializable]
    [ComVisible(true)]
    public enum EnumNETDataType
    {
        BYTE = 1,
        SBYTE = 2,
        SHORT = 3,
        USHORT = 4,
        INT = 5,
        UINT = 6,
        LONG = 7,
        ULONG = 8,
        FLOAT = 9,
        DOUBLE = 10,
        DECIMAL = 11,
        CHAR = 12,
        BOOL = 13,
        STRING = 14,
        OBJECT = 15
    }
}
