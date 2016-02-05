namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// Specifies the type of DATA_DIRECTORY entry
    /// </summary>
    public enum EnumDataDirectoryEntryType
    {
        /// <summary>
        /// Export Directory
        /// </summary>
        EXPORT_TABLE = 0,

        /// <summary>
        /// Import Directory
        /// </summary>
        IMPORT_TABLE = 1,

        /// <summary>
        /// Resource Directory
        /// </summary>
        RESOURCE_TABLE = 2,

        /// <summary>
        /// Exception Directory
        /// </summary>
        EXCEPTION_TABLE = 3,

        /// <summary>
        /// Security Directory
        /// </summary>
        CERTIFICATE_TABLE = 4,

        /// <summary>
        /// Base Relocation Table
        /// </summary>
        BASE_RELOCATION_TABLE = 5,

        /// <summary>
        /// Debug Directory
        /// </summary>
        DEBUG = 6,

        /// <summary>
        /// Architecture Specific Data
        /// </summary>
        ARCHITECTURE = 7,

        /// <summary>
        /// RVA of GP
        /// </summary>
        GLOBAL_PTR = 8,

        /// <summary>
        /// TLS Directory
        /// </summary>
        TLS_TABLE = 9,

        /// <summary>
        /// Load Configuration Directory
        /// </summary>
        LOAD_CONFIG_TABLE = 10,

        /// <summary>
        /// Bound Import Directory in headers
        /// </summary>
        BOUND_IMPORT = 11,

        /// <summary>
        /// Import Address Table
        /// </summary>
        IAT = 12,

        /// <summary>
        /// Delay Load Import Descriptors
        /// </summary>
        DELAY_IMPORT_DESCRIPTOR = 13,

        /// <summary>
        /// COM Runtime descriptor
        /// </summary>
        CLR_RUNTIME_HEADER = 14
    }
}
