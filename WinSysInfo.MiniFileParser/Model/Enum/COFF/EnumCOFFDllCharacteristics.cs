using System;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The following values are defined for the DllCharacteristics field of the 
    /// optional header
    /// </summary>
    [Flags]
    public enum EnumCOFFDllCharacteristics : ushort
    {
        /// <summary>
        /// ASLR with 64 bit address space.
        /// </summary>
        IMAGE_DLL_CHARACTERISTICS_HIGH_ENTROPY_VA = 0x0020,

        /// <summary>
        /// DLL can be relocated at load time.
        /// </summary>
        IMAGE_DLL_CHARACTERISTICS_DYNAMIC_BASE = 0x0040,

        /// <summary>
        /// Code integrity checks are enforced.
        /// </summary>
        IMAGE_DLL_CHARACTERISTICS_FORCE_INTEGRITY = 0x0080,

        /// <summary>
        /// Image is NX compatible.
        /// </summary>
        IMAGE_DLL_CHARACTERISTICS_NX_COMPAT = 0x0100,

        /// <summary>
        /// Isolation aware, but do not isolate the image.
        /// </summary>
        IMAGE_DLL_CHARACTERISTICS_NO_ISOLATION = 0x0200,

        /// <summary>
        /// Does not use structured exception handling (SEH). No SEH handler may 
        /// be called in this image.
        /// </summary>
        IMAGE_DLL_CHARACTERISTICS_NO_SEH = 0x0400,

        /// <summary>
        /// Do not bind the image.
        /// </summary>
        IMAGE_DLL_CHARACTERISTICS_NO_BIND = 0x0800,

        /// <summary>
        /// Image should execute in an AppContainer.
        /// </summary>
        IMAGE_DLL_CHARACTERISTICS_APPCONTAINER = 0x1000,

        /// <summary>
        /// A WDM driver.
        /// </summary>
        IMAGE_DLL_CHARACTERISTICS_WDM_DRIVER = 0x2000,

        /// <summary>
        /// Image supports Control Flow Guard.
        /// </summary>
        IMAGE_DLL_CHARACTERISTICS_GUARD_CF = 0x4000,

        /// <summary>
        /// Terminal Server aware.
        /// </summary>
        IMAGE_DLL_CHARACTERISTICS_TERMINAL_SERVER_AWARE = 0x8000
    }
}
