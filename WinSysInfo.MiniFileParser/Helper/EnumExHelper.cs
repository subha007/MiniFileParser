using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSysInfo.MiniFileParser.Helper
{
    /// <summary>
    /// Contains extended helper methods can be used on <see cref="Enum"/> types
    /// </summary>
    public static class EnumExHelper
    {
        /// <summary>
        /// Get enum value from the enum name
        /// </summary>
        /// <param name="enumName">The Enum class name</param>
        /// <param name="enumConst">The enum value name</param>
        /// <returns></returns>
        public static int GetValueOf(string enumName, string enumConst)
        {
            Type enumType = Type.GetType(enumName);
            if (enumType == null)
            {
                throw new ArgumentException("Specified enum type could not be found", "enumName");
            }

            object value = Enum.Parse(enumType, enumConst);
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Get enum value from the enum name
        /// </summary>
        /// <param name="enumType">The Enum class type</param>
        /// <param name="enumConst">The enum value name</param>
        /// <returns></returns>
        public static object GetValueOf(Type enumType, string enumConst)
        {
            return Enum.Parse(enumType, enumConst);
        }

        /// <summary>
        /// A templated function to return exact Enum type
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumConst"></param>
        /// <returns></returns>
        public static TEnum GetValueOf<TEnum>(string enumConst) where TEnum : struct
        {
            return (TEnum) Enum.Parse(typeof(TEnum), enumConst);
        }
    }
}
