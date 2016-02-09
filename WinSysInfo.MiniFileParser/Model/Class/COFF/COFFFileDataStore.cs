using System.Collections.Generic;
using System.Linq;
using WinSysInfo.MiniFileParser.Helper;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The layout of a COFF file.
    /// </summary>
    public class COFFFileDataStore : FileDataStore<EnumPEStructureId>
    {
        #region Constructors

        public COFFFileDataStore()
        {
            this.SetRelation(EnumPEStructureId.OPTIONAL_HEADER_FIELDS, EnumPEStructureId.OS_HEADER);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Get the list of data
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public List<LayoutModel<TStruct>> GetListData<TStruct>(EnumPEStructureId enumVal)
            where TStruct : struct
        {
            return base.GetListData<EnumPEStructureId, TStruct>(enumVal);
        }

        /// <summary>
        /// Get the data
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public LayoutModel<TStruct> GetData<TStruct>(EnumPEStructureId enumVal)
            where TStruct : struct
        {
            return base.GetData<EnumPEStructureId, TStruct>(enumVal);
        }

        /// <summary>
        /// Set the data list
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public void SetData<TStruct>(EnumPEStructureId enumVal, LayoutModel<TStruct> modelobj,
            EnumPEStructureId? enumParent = null, int position = -1)
            where TStruct : struct
        {
            base.SetData<EnumPEStructureId, TStruct>(enumVal, modelobj, enumParent, position);
        }

        #endregion Methods

        #region Properties

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
                return this.GetData<MSDOSHeaderLayout>(EnumPEStructureId.MSDOS_HEADER);
            }
            set
            {
                this.SetData(EnumPEStructureId.MSDOS_HEADER, value, EnumPEStructureId.OS_HEADER);
            }
        }

        /// <summary>
        /// Check if the MSDOS header Magic data is the PE Header
        /// This check can only be performed after the <see cref="MsDosHeader"/> data object is
        /// filled. Else it will always return false.
        /// </summary>
        public bool HasPEHeader
        {
            get
            {
                return this.MsDosHeader.Data.Magic.SequenceEqual(ConstantWinCOFFImage.MSDOSMagic);
            }
        }

        /// <summary>
        /// Checks if the COFF file header is present or not
        /// </summary>
        public bool IsCOFFFileHeader { get; set; }

        /// <summary>
        /// The DOS stub usually just prints a string, something like the message.
        /// It can be a full-blown DOS program. When building applications on Windows,
        /// the linker sends instruction to a binary called winstub.exe to the executable
        /// file. This file is kept in the address 0x3c, which is offset to the next PE 
        /// header section.
        /// </summary>
        internal LayoutModel<MSDOSStubLayout> MsDosStub
        {
            get
            {
                return this.GetData<MSDOSStubLayout>(EnumPEStructureId.MSDOS_STUB);
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
                return this.GetData<NTHeaderLayout>(EnumPEStructureId.NT_HEADER);
            }
            set
            {
                this.SetData(EnumPEStructureId.NT_HEADER, value, EnumPEStructureId.OS_HEADER);
            }
        }

        /// <summary>
        /// Like other executable files, a PE file has a collection of fields that defines 
        /// what the rest of file looks like. The header contains info such as the location 
        /// and size of code
        /// </summary>
        internal LayoutModel<COFFFileHeader> CoffFileHeader
        {
            get
            {
                return this.GetData<COFFFileHeader>(EnumPEStructureId.COFF_FILE_HEADER);
            }
            set
            {
                this.SetData(EnumPEStructureId.COFF_FILE_HEADER, value, EnumPEStructureId.OS_HEADER);
            }
        }

        /// <summary>
        /// Only for Big Object files. The COFF Header.
        /// </summary>
        internal LayoutModel<COFFBigObjHeader> CoffFileBigHeader
        {
            get
            {
                return this.GetData<COFFBigObjHeader>(EnumPEStructureId.COFF_BIGOBJ_FILE_HEADER);
            }
            set
            {
                this.SetData(EnumPEStructureId.COFF_BIGOBJ_FILE_HEADER, value, EnumPEStructureId.OS_HEADER);
            }
        }

        /// <summary>
        /// Check if this is an import library
        /// </summary>
        /// <returns></returns>
        public bool IsImportLibrary()
        {
            return this.CoffFileBigHeader.Data.NumberOfSections == 0XFFFF;
        }

        /// <summary>
        /// Optional standard fields
        /// </summary>
        internal LayoutModel<OptionalHeaderStandardFields> OptHStdFields
        {
            get
            {
                return this.GetData<OptionalHeaderStandardFields>(EnumPEStructureId.OPT_HEADER_STD_FIELDS);
            }
            set
            {
                this.SetData(EnumPEStructureId.OPT_HEADER_STD_FIELDS, value, EnumPEStructureId.OPTIONAL_HEADER_FIELDS);
            }
        }

        /// <summary>
        /// Optional standard fields PE32
        /// </summary>
        internal LayoutModel<OptionalHeaderStandardFields32> OptHStdFields32
        {
            get
            {
                return this.GetData<OptionalHeaderStandardFields32>(EnumPEStructureId.OPT_HEADER_STD_FIELDS32);
            }
            set
            {
                this.SetData(EnumPEStructureId.OPT_HEADER_STD_FIELDS32, value, EnumPEStructureId.OPTIONAL_HEADER_FIELDS);
            }
        }

        /// <summary>
        /// If it is PE32 then
        /// </summary>
        internal LayoutModel<OptionalHeaderWindowsSpecificFields32> OptHWinSpFields32
        {
            get
            {
                return this.GetData<OptionalHeaderWindowsSpecificFields32>(EnumPEStructureId.OPT_HEADER_STD_FIELDS32);
            }
            set
            {
                this.SetData(EnumPEStructureId.OPT_HEADER_STD_FIELDS32, value, EnumPEStructureId.OPTIONAL_HEADER_FIELDS);
            }
        }

        /// <summary>
        /// If it is PE32+ then
        /// </summary>
        internal LayoutModel<OptionalHeaderWindowsSpecificFields32Plus> OptHWinSpFields32Plus
        {
            get
            {
                return this.GetData<OptionalHeaderWindowsSpecificFields32Plus>(EnumPEStructureId.OPT_HEADER_SPEC_FLD32PLUS);
            }
            set
            {
                this.SetData(EnumPEStructureId.OPT_HEADER_SPEC_FLD32PLUS, value, EnumPEStructureId.OPTIONAL_HEADER_FIELDS);
            }
        }

        internal uint NumberOfDataDirImageOnly { get; set; }

        /// <summary>
        /// Set Optional directory image
        /// </summary>
        internal void SetOptHDataDirImages(EnumPEStructureId structId, LayoutModel<OptionalHeaderDataDirImageOnly> value)
        {
            this.SetData(structId, value, EnumPEStructureId.OPT_HEADER_DATADIR_IMAGE_ONLY);
        }

        /// <summary>
        /// Get the optional directory image
        /// </summary>
        /// <param name="structId"></param>
        /// <returns></returns>
        internal LayoutModel<OptionalHeaderDataDirImageOnly> GetOptHDataDirImages(EnumPEStructureId structId)
        {
            return this.GetData<OptionalHeaderDataDirImageOnly>(structId);
        }

        internal uint NumberOfSections { get; set; }

        /// <summary>
        /// Section tables
        /// </summary>
        internal List<LayoutModel<COFFSectionTableLayout>> SectionTables
        {
            get
            {
                return this.GetListData<COFFSectionTableLayout>(EnumPEStructureId.COFF_SECTION_TABLE);
            }
            set
            {
                this.SetListData(EnumPEStructureId.COFF_SECTION_TABLE, value);
            }
        }

        internal uint PointerToSymbolTable { get; set; }

        internal uint NumberOfSymbols { get; set; }

        internal List<LayoutModel<COFFSymbolTableLayout>> SymbolTables
        {
            get
            {
                return this.GetListData<COFFSymbolTableLayout>(EnumPEStructureId.COFF_SYM_TABLE);
            }
            set
            {
                this.SetListData(EnumPEStructureId.COFF_SYM_TABLE, value);
            }
        }

        internal List<LayoutModel<COFFSymbolTableBigObjLayout>> SymbolTablesBigObj
        {
            get
            {
                return this.GetListData<COFFSymbolTableBigObjLayout>(EnumPEStructureId.COFF_SYM_TABLE_BIGOBJ);
            }
            set
            {
                this.SetListData(EnumPEStructureId.COFF_SYM_TABLE_BIGOBJ, value);
            }
        }

        internal void SetDirTableEntry<TDataDirStruct>(EnumPEStructureId dataDirEnum, List<LayoutModel<TDataDirStruct>> listDataDir)
            where TDataDirStruct : struct
        {
            this.SetListData(dataDirEnum, listDataDir, EnumPEStructureId.DATA_DIR_TABLE_ENTRY);
        }

        #endregion Properties
    }
}
