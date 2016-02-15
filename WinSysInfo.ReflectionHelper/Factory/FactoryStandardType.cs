using System;
using WinSysInfo.ReflectionHelper.Model;

namespace WinSysInfo.ReflectionHelper.Factory
{
    /// <summary>
    /// The factory class to get the data type
    /// </summary>
    public static class FactoryStandardType
    {
        /// <summary>
        /// Get the data type
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static Type Get(EnumNETDataType dataType)
        {
            switch(dataType)
            {
                case EnumNETDataType.BOOL:
                    return typeof(bool);
                case EnumNETDataType.BYTE:
                    return typeof(byte);
                case EnumNETDataType.CHAR:
                    return typeof(char);
                case EnumNETDataType.DECIMAL:
                    return typeof(decimal);
                case EnumNETDataType.DOUBLE:
                    return typeof(double);
                case EnumNETDataType.FLOAT:
                    return typeof(float);
                case EnumNETDataType.INT:
                    return typeof(int);
                case EnumNETDataType.LONG:
                    return typeof(long);
                case EnumNETDataType.SBYTE:
                    return typeof(sbyte);
                case EnumNETDataType.SHORT:
                    return typeof(short);
                case EnumNETDataType.STRING:
                    return typeof(string);
                case EnumNETDataType.UINT:
                    return typeof(uint);
                case EnumNETDataType.ULONG:
                    return typeof(ulong);
                case EnumNETDataType.USHORT:
                    return typeof(ushort);
                default:
                    return null;
            }
        }
    }
}
