using System;
using System.Reflection;
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
        public static Type Get(TypeCode dataType)
        {
            switch(dataType)
            {
                case TypeCode.Object:
                    return typeof(object);
                case TypeCode.DBNull:
                    return typeof(DBNull);
                case TypeCode.Boolean:
                    return typeof(bool);
                case TypeCode.Char:
                    return typeof(char);
                case TypeCode.SByte:
                    return typeof(sbyte);
                case TypeCode.Byte:
                    return typeof(byte);
                case TypeCode.Int16:
                    return typeof(short);
                case TypeCode.UInt16:
                    return typeof(ushort);
                case TypeCode.Int32:
                    return typeof(int);
                case TypeCode.UInt32:
                    return typeof(uint);
                case TypeCode.Int64:
                    return typeof(long);
                case TypeCode.UInt64:
                    return typeof(ulong);
                case TypeCode.Single:
                    return typeof(Single);
                case TypeCode.Double:
                    return typeof(double);
                case TypeCode.Decimal:
                    return typeof(decimal);
                case TypeCode.DateTime:
                    return typeof(DateTime);
                case TypeCode.String:
                    return typeof(string);
                default:
                    return null;
            }
        }

        /// <summary>
        /// is this a numeric type
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static bool IsNumericTypes(TypeCode dataType)
        {
            switch (dataType)
            {
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Convert from value
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="p"></param>
        /// <param name="numberStyles"></param>
        public static object ConvertToNumeric(TypeCode dataTypeCode, string value, int numberBase)
        {
            Type dataType = FactoryStandardType.Get(dataTypeCode);
            MethodInfo mi = typeof(Convert).GetMethod(NameOfConvertToMethodNumeric(dataTypeCode), new Type[] { typeof(string), typeof(int) });

            return mi.Invoke(dataType, new object[] { value, numberBase });
        }

        /// <summary>
        /// return the conver tspecific method
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private static string NameOfConvertToMethodNumeric(TypeCode dataType)
        {
            switch (dataType)
            {
                case TypeCode.SByte:
                    return "ToSByte";
                case TypeCode.Byte:
                    return "ToByte";
                case TypeCode.Int16:
                    return "ToInt16";
                case TypeCode.UInt16:
                    return "ToUInt16";
                case TypeCode.Int32:
                    return "ToInt32";
                case TypeCode.UInt32:
                    return "ToUInt32";
                case TypeCode.Int64:
                    return "ToInt64";
                case TypeCode.UInt64:
                    return "ToUInt64";
                default:
                    return string.Empty;
            }
        }
    }
}
