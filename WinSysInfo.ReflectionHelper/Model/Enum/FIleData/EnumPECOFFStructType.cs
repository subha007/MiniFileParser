namespace WinSysInfo.ReflectionHelper.Model
{
    /// <summary>
    /// The list of enumeration structure for PE COFF File model
    /// </summary>
    public enum EnumPECOFFStructType
    {
        /// <summary>
        /// Unknwon model
        /// </summary>
        UNKNOWN = 0x00000000,

        /// <summary>
        /// MS-DOS header that has been around since version 2 of the 
        /// MS-DOS operating system. The MS-DOS header occupies the first 
        /// 64 bytes of the PE file.
        /// </summary>
        MSDOS_HEADER = 0x00000001,

        /// <summary>
        /// The real-mode stub program is an actual program run by MS-DOS 
        /// when the executable is loaded. For an actual MS-DOS executable 
        /// image file, the application begins executing here. For successive 
        /// operating systems, including Windows, OS/2®, and Windows NT, an MS-DOS 
        /// stub program is placed here that runs instead of the actual application
        /// </summary>
        MSDOS_STUB = MSDOS_HEADER + 1,

        /// <summary>
        /// Contains the signature that identifies the file as PE
        /// </summary>
        NT_HEADER = MSDOS_STUB + 1,

        /// <summary>
        /// COFF File header
        /// </summary>
        COFF_FILE_HEADER = NT_HEADER + 1,

        /// <summary>
        /// COFF Bigobj file header
        /// </summary>
        COFF_BIGOBJ_FILE_HEADER = COFF_FILE_HEADER + 1,
        OPTIONAL_HEADER_FIELDS = COFF_BIGOBJ_FILE_HEADER + 1,
        OPT_HEADER_STD_FIELDS = OPTIONAL_HEADER_FIELDS + 1,
        OPT_HEADER_STD_FIELDS32 = OPT_HEADER_STD_FIELDS + 1,
        OPT_HEADER_SPEC_FLD32 = OPT_HEADER_STD_FIELDS32 + 1,
        OPT_HEADER_SPEC_FLD32PLUS = OPT_HEADER_SPEC_FLD32 + 1,
        OPT_HEADER_DATADIR_IMAGE_ONLY = OPT_HEADER_SPEC_FLD32PLUS + 1,
        OPT_HEADER_DATADIR_EXPORT_TABLE = OPT_HEADER_DATADIR_IMAGE_ONLY + 1,
        OPT_HEADER_DATADIR_IMPORT_TABLE = OPT_HEADER_DATADIR_EXPORT_TABLE + 1,
        OPT_HEADER_DATADIR_RESOURCE_TABLE = OPT_HEADER_DATADIR_IMPORT_TABLE + 1,
        OPT_HEADER_DATADIR_EXCEPTION_TABLE = OPT_HEADER_DATADIR_RESOURCE_TABLE + 1,
        OPT_HEADER_DATADIR_CERTIFICATE_TABLE = OPT_HEADER_DATADIR_EXCEPTION_TABLE + 1,
        OPT_HEADER_DATADIR_BASE_RELOCATION_TABLE = OPT_HEADER_DATADIR_CERTIFICATE_TABLE + 1,
        OPT_HEADER_DATADIR_DEBUG = OPT_HEADER_DATADIR_BASE_RELOCATION_TABLE + 1,
        OPT_HEADER_DATADIR_ARCHITECTURE = OPT_HEADER_DATADIR_DEBUG + 1,
        OPT_HEADER_DATADIR_GLOBAL_PTR = OPT_HEADER_DATADIR_ARCHITECTURE + 1,
        OPT_HEADER_DATADIR_TLS_TABLE = OPT_HEADER_DATADIR_GLOBAL_PTR + 1,
        OPT_HEADER_DATADIR_LOAD_CONFIG_TABLE = OPT_HEADER_DATADIR_TLS_TABLE + 1,
        OPT_HEADER_DATADIR_BOUND_IMPORT = OPT_HEADER_DATADIR_LOAD_CONFIG_TABLE + 1,
        OPT_HEADER_DATADIR_IAT = OPT_HEADER_DATADIR_BOUND_IMPORT + 1,
        OPT_HEADER_DATADIR_DELAY_IMPORT_DESCRIPTOR = OPT_HEADER_DATADIR_IAT + 1,
        OPT_HEADER_DATADIR_CLR_RUNTIME_HEADER = OPT_HEADER_DATADIR_DELAY_IMPORT_DESCRIPTOR + 1,
        OPT_HEADER_DATADIR_RESERVED = OPT_HEADER_DATADIR_CLR_RUNTIME_HEADER + 1,
        COFF_SECTION_TABLE = OPT_HEADER_DATADIR_RESERVED + 1,
        COFF_SYM_TABLE = COFF_SECTION_TABLE + 1024,
        COFF_SYM_TABLE_BIGOBJ = COFF_SYM_TABLE + 1024,
        DATA_DIR_TABLE_ENTRY = COFF_SYM_TABLE_BIGOBJ + 1,
        IMPORT_DIR_TABLE_ENTRY = COFF_SYM_TABLE_BIGOBJ + 1024,
        DELAY_IMPORT_DIR_TABLE_ENTRY = IMPORT_DIR_TABLE_ENTRY + 1,
        EXPORT_DIR_TABLE_ENTRY = DELAY_IMPORT_DIR_TABLE_ENTRY + 1,
        BASE_RELOC_ENTRY = EXPORT_DIR_TABLE_ENTRY + 1
    }
}
