namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The layout of a COFF file.
    /// </summary>
    public class COFFFileDataStore : FileDataStore<EnumPEStructureId>
    {
        #region Properties

        /// <summary>
        /// Check if it is PE Header
        /// </summary>
        public bool HasPEHeader { get; set; }

        /// <summary>
        /// Checks if the COFF file header is present or not
        /// </summary>
        public bool IsCOFFFileHeader { get; set; }

        /// <summary>
        /// The PE DOS Header.
        /// DOS header starts with the first 64 bytes of every PE file. It’s there 
        /// because DOS can recognize it as a valid executable and can run it in the 
        /// DOS stub mode
        /// </summary>
        internal LayoutModel<MSDOSHeaderLayout> MsDosHeader
        {
            get
            {
                return this.GetData(EnumPEStructureId.MSDOS_HEADER) as LayoutModel<MSDOSHeaderLayout>;
            }
            set
            {
                this.SetData(EnumPEStructureId.MSDOS_HEADER, value);
            }
        }

        /// <summary>
        /// The DOS stub usually just prints a string, something like the message.
        /// It can be a full-blown DOS program. When building applications on Windows,
        /// the linker sends instruction to a binary called winstub.exe to the executable
        /// file. This file is kept in the address 0x3c, which is offset to the next PE 
        /// header section.
        /// </summary>
        internal MSDOSStubLayoutModel MsDosStub
        {
            get
            {
                return this.GetData(EnumPEStructureId.MSDOS_STUB) as MSDOSStubLayoutModel;
            }
            set
            {
                this.SetData(EnumPEStructureId.MSDOS_STUB, value);
            }
        }

        /// <summary>
        /// NT Header
        /// </summary>
        internal LayoutModel<NTHeaderLayout> NTHeader
        {
            get
            {
                return this.GetData(EnumPEStructureId.NT_HEADER) as LayoutModel<NTHeaderLayout>;
            }
            set
            {
                this.SetData(EnumPEStructureId.NT_HEADER, value);
            }
        }

        /// <summary>
        /// Like other executable files, a PE file has a collection of fields that defines 
        /// what the rest of file looks like. The header contains info such as the location 
        /// and size of code
        /// </summary>
        internal COFFFileHeaderLayoutModel CoffFileHeader
        {
            get
            {
                return this.GetData(EnumPEStructureId.COFF_FILE_HEADER) as COFFFileHeaderLayoutModel;
            }
            set
            {
                this.SetData(EnumPEStructureId.COFF_FILE_HEADER, value);
            }
        }

        /// <summary>
        /// Only for Big Object files. The COFF Header.
        /// </summary>
        internal LayoutModel<COFFBigObjHeader> CoffFileBigHeader
        {
            get
            {
                return this.GetData(EnumPEStructureId.COFF_BIGOBJ_FILE_HEADER) as LayoutModel<COFFBigObjHeader>;
            }
            set
            {
                this.SetData(EnumPEStructureId.COFF_BIGOBJ_FILE_HEADER, value);
            }
        }

        /// <summary>
        /// Optional standard fields
        /// </summary>
        internal LayoutModel<OptionalHeaderStandardFields> OptHStdFields
        {
            get
            {
                return this.GetData(EnumPEStructureId.OPT_HEADER_STD_FIELDS) as LayoutModel<OptionalHeaderStandardFields>;
            }
            set
            {
                this.SetData(EnumPEStructureId.OPT_HEADER_STD_FIELDS, value);
            }
        }

        /// <summary>
        /// Optional standard fields PE32
        /// </summary>
        internal LayoutModel<OptionalHeaderStandardFields32> OptHStdFields32
        {
            get
            {
                return this.GetData(EnumPEStructureId.OPT_HEADER_STD_FIELDS32) as LayoutModel<OptionalHeaderStandardFields32>;
            }
            set
            {
                this.SetData(EnumPEStructureId.OPT_HEADER_STD_FIELDS32, value);
            }
        }

        /// <summary>
        /// If it is PE32 then
        /// </summary>
        internal LayoutModel<OptionalHeaderWindowsSpecificFields32> OptHWinSpFields32
        {
            get
            {
                return this.GetData(EnumPEStructureId.OPT_HEADER_STD_FIELDS32) as LayoutModel<OptionalHeaderWindowsSpecificFields32>;
            }
            set
            {
                this.SetData(EnumPEStructureId.OPT_HEADER_STD_FIELDS32, value);
            }
        }

        /// <summary>
        /// If it is PE32+ then
        /// </summary>
        internal LayoutModel<OptionalHeaderWindowsSpecificFields32Plus> OptHWinSpFields32Plus
        {
            get
            {
                return this.GetData(EnumPEStructureId.OPT_HEADER_SPEC_FLD32PLUS) as LayoutModel<OptionalHeaderWindowsSpecificFields32Plus>;
            }
            set
            {
                this.SetData(EnumPEStructureId.OPT_HEADER_SPEC_FLD32PLUS, value);
            }
        }

        internal uint NumberOfDataDirImageOnly { get; set; }

        /// <summary>
        /// Optional directory image
        /// </summary>
        internal OptHeaderDataDirectoriesImageOnly OptHDataDirImages
        {
            get
            {
                return this.GetData(EnumPEStructureId.OPT_HEADER_DATADIR_IMAGE_ONLY) as OptHeaderDataDirectoriesImageOnly;
            }
            set
            {
                this.SetData(EnumPEStructureId.OPT_HEADER_DATADIR_IMAGE_ONLY, value);
            }
        }

        internal uint NumberOfSections { get; set; }

        internal COFFSectionTableList SectionTables
        {
            get
            {
                return this.GetData(EnumPEStructureId.COFF_SECTION_TABLE) as COFFSectionTableList;
            }
            set
            {
                this.SetData(EnumPEStructureId.COFF_SECTION_TABLE, value);
            }
        }

        internal uint PointerToSymbolTable { get; set; }

        internal uint NumberOfSymbols { get; set; }

        internal SymbolTableList SymbolTables
        {
            get
            {
                return this.GetData(EnumPEStructureId.COFF_SYM_TABLE) as SymbolTableList;
            }
            set
            {
                this.SetData(EnumPEStructureId.COFF_SYM_TABLE, value);
            }
        }

        internal LayoutModel<ImportDirectoryTableEntry> ImportDataDir
        {
            get
            {
                return this.GetData(EnumPEStructureId.IMPORT_DIR_TABLE_ENTRY) as LayoutModel<ImportDirectoryTableEntry>;
            }
            set
            {
                this.SetData(EnumPEStructureId.IMPORT_DIR_TABLE_ENTRY, value);
            }
        }

        internal uint NumberOfImportDirectory = 0;
        internal uint NumberOfDelayImportDirectory = 0;

        #endregion Properties
    }
}
