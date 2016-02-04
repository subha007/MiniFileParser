
namespace WinSysInfo.MiniFileParser.Helper
{
    /// <summary>
    /// Extended or better string methods and properties
    /// </summary>
    public static class StringExHelper
    {
        /// <summary>
        /// Compile time constant.
        /// Helpful to use in parameter default
        /// </summary>
        public const string Empty = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrWhiteSpace(string data)
        {
            return (string.IsNullOrWhiteSpace(data) == true || data == string.Empty);
        }
    }
}
