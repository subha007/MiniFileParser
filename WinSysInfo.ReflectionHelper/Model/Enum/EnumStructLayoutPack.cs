using System;
using System.Runtime.InteropServices;

namespace WinSysInfo.ReflectionHelper.Model
{
    /// <summary>
    /// Enumeration of Pack values
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    public enum EnumStructLayoutPack
    {
        PackDefault = 0,
        Pack1 = 1,
        Pack2 = 2,
        Pack4 = 4,
        Pack8 = 8,
        Pack16 = 16,
        Pack32 = 32,
        Pack64 = 64,
        Pack128 = 128
    }
}
