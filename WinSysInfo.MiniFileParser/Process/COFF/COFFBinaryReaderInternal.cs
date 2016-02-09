using System;
using System.Collections.Generic;
using System.Linq;
using WinSysInfo.MiniFileParser.Helper;
using WinSysInfo.MiniFileParser.Interface;
using WinSysInfo.MiniFileParser.Model;

namespace WinSysInfo.MiniFileParser.Process
{
    internal class COFFBinaryReaderInternal : IPECOFFFileReader
    {
        #region Properties

        /// <summary>
        /// Get or set the full file path to parse
        /// </summary>
        internal IFileReaderProperty FileProperty { get; set; }

        /// <summary>
        /// Get or set the data store
        /// </summary>
        internal COFFFileDataStore DataStore { get; set; }

        /// <summary>
        /// The reader used to read the PE File 
        /// </summary>
        public IFileReadStrategy ReaderStrategy { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        internal COFFBinaryReaderInternal(IFileReaderProperty property, IFileDataStore store, IFileReadStrategy reader)
        {
            this.FileProperty = property;
            this.DataStore = store as COFFFileDataStore;
            this.ReaderStrategy = reader;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Contains the logic to parse the COFF file
        /// </summary>
        /// <returns></returns>
        public bool Read() 
        {
            bool bSuccess = false;
            try
            {
                // Open the file for reading
                this.ReaderStrategy.Open();

                // Read the MS DOS Header
                ReadMSDOSHeader();

                if (this.DataStore.HasPEHeader == false)
                    throw new FormatException("PE Magic Header MZ is not present.");

                // Read the MS DOS Stub
                ReadMSDOSStub();

                // Read NT header
                ReadNTHeader();

                // Check the PE magic bytes. ("PE\0\0")
                if (ContainsPEMagicBytes() == false)
                    throw new NotImplementedException("This is not PE COFF file");

                // Check if this normal COFF file or big obj file
                CheckBigObjHeader();

                if (this.DataStore.IsCOFFFileHeader)
                    // Read COFF Header
                    ReadCOFFHeader();
                else
                    // ReadcoffReaderHelper Big Obj COFF at the same position
                    ReadCOFFBigObjHeader();

                // The prior checkSize call may have failed.  This isn't a hard error
                // because we were just trying to sniff out bigobj.
                if (this.DataStore.IsCOFFFileHeader && this.DataStore.IsImportLibrary())
                    throw new Exception("Unknown file");

                if (this.DataStore.HasPEHeader)
                {
                    ReadOptHeaderStdFields();

                    // Check PE32 or PE32+
                    if (this.DataStore.OptHStdFields.Data.Magic == EnumOptionalHeaderMagicNo.IMAGE_NT_OPTIONAL_HDR32_MAGIC)
                    {
                        ReadOptHeaderStdFields32();
                        ReadOptHeaderSpecificFields32();
                    }
                    else if (this.DataStore.OptHStdFields.Data.Magic == EnumOptionalHeaderMagicNo.IMAGE_NT_OPTIONAL_HDR64_MAGIC)
                    {
                        ReadOptHeaderSpecificFields32Plus();
                    }
                    else
                    {
                        // It's neither PE32 nor PE32+.
                        throw new Exception("It's neither PE32 nor PE32+");
                    }

                    CalculateDataDirectoryCount();

                    // Read the data directories
                    ReadOptHeaderDataDirectoriesImageOnly();
                }

                // Read the Section Table (Section Header)
                CalculateNumberOfSections();
                ReadSectionTable();

                // Initialize the pointer to the symbol table.
                CalculatePointerToSymbolTable();
                if (this.DataStore.PointerToSymbolTable != 0)
                {
                    ReadSymbolTablePointer();
                }
                else
                {
                    // We had better not have any symbols if we don't have a symbol table.
                    CalculateNumberOfSymbols();
                    if(this.DataStore.NumberOfSymbols != 0)
                        throw new Exception("We had better not have any symbols if we don't have a symbol table");
                }

                // Initialize the pointer to the beginning of the Data Dir table.
                InitDataDirTableEntryPointer<ImportDirectoryTableEntry>(EnumPEStructureId.OPT_HEADER_DATADIR_IMPORT_TABLE,
                    EnumPEStructureId.IMPORT_DIR_TABLE_ENTRY);
                InitDataDirTableEntryPointer<ExportDirectoryTableEntry>(EnumPEStructureId.OPT_HEADER_DATADIR_EXPORT_TABLE,
                    EnumPEStructureId.EXPORT_DIR_TABLE_ENTRY);
                InitDataDirTableEntryPointer<DelayImportDirectoryTableEntry>(EnumPEStructureId.OPT_HEADER_DATADIR_DELAY_IMPORT_DESCRIPTOR,
                    EnumPEStructureId.DELAY_IMPORT_DIR_TABLE_ENTRY);
                InitDataDirTableEntryPointer<COFFBaseRelocBlockHeader>(EnumPEStructureId.OPT_HEADER_DATADIR_BASE_RELOCATION_TABLE,
                    EnumPEStructureId.BASE_RELOC_ENTRY);

                // Find string table. The first four byte of the string table contains the
                // total size of the string table, including the size field itself. If the
                // string table is empty, the value of the first four byte would be 4.

                bSuccess = true;
            }
            finally
            {
                // Close
                this.ReaderStrategy.Close();
            }

            return bSuccess;
        }

        /// <summary>
        /// Read MS DOS Header
        /// </summary>
        public void ReadMSDOSHeader()
        {
            // PE/COFF, seek through MS-DOS compatibility stub and 4-byte
            // PE signature to find 'normal' COFF header.
            this.DataStore.MsDosHeader = this.ReaderStrategy.ReadLayout<MSDOSHeaderLayout>();

            //Check if PE header is DWORD-aligned
            if ((this.DataStore.MsDosHeader.Data.AddressOfNewExeHeader % sizeof(uint)) != 0)
                throw new System.DataMisalignedException("PE header is not DWORD-aligned");
        }

        /// <summary>
        /// Read MS DOS stub
        /// </summary>
        public void ReadMSDOSStub()
        {
            MSDOSStubLayout rawData;
            
            int size = (int)this.DataStore.MsDosHeader.Data.AddressOfNewExeHeader -
                            (int)this.ReaderStrategy.FileOffset;
            rawData.Stub = this.ReaderStrategy.ReadBytes(size);

            LayoutModel<MSDOSStubLayout> dosStubObj = new LayoutModel<MSDOSStubLayout>(rawData);

            this.DataStore.MsDosStub = dosStubObj;
        }

        /// <summary>
        /// Check the PE magic bytes. ("PE\0\0") 
        /// </summary>
        /// <returns></returns>
        public bool ContainsPEMagicBytes()
        {
            return this.DataStore.NTHeader.Data.Signature.SequenceEqual(ConstantWinCOFFImage.PEMagic);
        }

        /// <summary>
        /// Initialize NT Header
        /// </summary>
        public void ReadNTHeader()
        {
            this.DataStore.NTHeader = this.ReaderStrategy.ReadLayout<NTHeaderLayout>();
        }

        /// <summary>
        /// Seek through COFF Header
        /// </summary>
        public void ReadCOFFHeader() 
        {
            // PE/COFF, seek through MS-DOS compatibility stub and 4-byte
            // PE signature to find 'normal' COFF header.
            this.DataStore.CoffFileHeader =
                                new COFFFileHeaderLayoutModel(this.ReaderStrategy.ReadLayout<COFFFileHeader>());
        }

        /// <summary>
        /// Seek through COFF Header
        /// </summary>
        public void ReadCOFFBigObjHeader() 
        {
            // PE/COFF, seek through MS-DOS compatibility stub and 4-byte
            // PE signature to find 'normal' COFF header.
            this.DataStore.CoffFileBigHeader =
                   this.ReaderStrategy.ReadLayout<COFFBigObjHeader>((-1) * LayoutModel<COFFFileHeader>.DataSize);
        }

        /// <summary>
        /// It might be a bigobj file, let's check.  Note that COFF bigobj and COFF
        /// </summary>
        public void CheckBigObjHeader()
        {
            this.DataStore.IsCOFFFileHeader = !(!this.DataStore.HasPEHeader &&
                this.ReaderStrategy.PeekUShort(COFFFileHeaderLayoutModel.GetOffset("Machine")) == (ushort)0 &&
                this.ReaderStrategy.PeekUShort(COFFFileHeaderLayoutModel.GetOffset("NumberOfSections")) == (ushort)0xFFFF);

            if (this.DataStore.IsCOFFFileHeader == true) return;

            char[] sigchars = System.Text.Encoding.UTF8.GetString(
                this.ReaderStrategy.ReadBytes(16, LayoutModel<COFFBigObjHeader>.GetOffset("UUID")))
                .ToCharArray();

            if (this.ReaderStrategy.PeekUShort(LayoutModel<COFFBigObjHeader>.GetOffset("Version"))
                >= ConstantWinCOFFImage.MinBigObjectVersion &&
                    sigchars.SequenceEqual(ConstantWinCOFFImage.BigObjMagic) == true)
                this.DataStore.IsCOFFFileHeader = false;
            else
                this.DataStore.IsCOFFFileHeader = true;
        }

        /// <summary>
        /// Initialize optional header standard fields
        /// </summary>
        public void ReadOptHeaderStdFields()
        {
            this.DataStore.OptHStdFields =
                                this.ReaderStrategy.ReadLayout<OptionalHeaderStandardFields>();
        }

        /// <summary>
        /// Initialize optional header standard fields
        /// </summary>
        public void ReadOptHeaderStdFields32()
        {
            this.DataStore.OptHStdFields32 =
                                this.ReaderStrategy.ReadLayout<OptionalHeaderStandardFields32>();
        }

        /// <summary>
        /// Initialize optional header standard fields
        /// </summary>
        public void ReadOptHeaderSpecificFields32()
        {
            this.DataStore.OptHWinSpFields32 =
                                this.ReaderStrategy.ReadLayout<OptionalHeaderWindowsSpecificFields32>();
        }

        /// <summary>
        /// Initialize optional header standard fields
        /// </summary>
        public void ReadOptHeaderSpecificFields32Plus()
        {
            this.DataStore.OptHWinSpFields32Plus =
                                this.ReaderStrategy.ReadLayout<OptionalHeaderWindowsSpecificFields32Plus>();
        }

        /// <summary>
        /// Get Data Directory count
        /// </summary>
        /// <returns></returns>
        public void CalculateDataDirectoryCount()
        {
            if (this.DataStore.OptHStdFields.Data.Magic == EnumOptionalHeaderMagicNo.IMAGE_NT_OPTIONAL_HDR32_MAGIC)
                this.DataStore.NumberOfDataDirImageOnly =
                    this.DataStore.OptHWinSpFields32.Data.CommonData2.NumberOfRvaAndSizes;
            else if (this.DataStore.OptHStdFields.Data.Magic == EnumOptionalHeaderMagicNo.IMAGE_NT_OPTIONAL_HDR64_MAGIC)
                this.DataStore.NumberOfDataDirImageOnly =
                    this.DataStore.OptHWinSpFields32Plus.Data.CommonData2.NumberOfRvaAndSizes;
            else
                this.DataStore.NumberOfDataDirImageOnly = 0;
        }

        /// <summary>
        /// Each data directory gives the address and size of a table or string that Windows uses.
        /// These data directory entries are all loaded into memory so that the system can use them
        /// at run time. A data directory is an 8byte field.
        /// </summary>
        public void ReadOptHeaderDataDirectoriesImageOnly()
        {
            // Read the data directories
            OptHeaderDataDirectoriesImageOnly dirsImage = new OptHeaderDataDirectoriesImageOnly();

            for (int indx = 0; indx < this.DataStore.NumberOfDataDirImageOnly; ++indx)
            {
                EnumPEStructureId enumVal = (EnumPEStructureId)
                    ((int)EnumPEStructureId.OPT_HEADER_DATADIR_EXPORT_TABLE + indx);

                this.DataStore.SetOptHDataDirImages(enumVal, this.ReaderStrategy.ReadLayout<OptionalHeaderDataDirImageOnly>());
            }
        }

        /// <summary>
        /// Get the count of sections
        /// </summary>
        /// <returns></returns>
        public void CalculateNumberOfSections()
        {
            if (this.DataStore.IsCOFFFileHeader)
                this.DataStore.NumberOfSections =
                    (uint)(this.DataStore.IsImportLibrary() ?
                                                        0 : this.DataStore.CoffFileHeader.Data.NumberOfSections);
            else
                this.DataStore.NumberOfSections = this.DataStore.CoffFileBigHeader.Data.NumberOfSections;
        }

        /// <summary>
        /// Read the section tables list
        /// </summary>
        public void ReadSectionTable()
        {
            COFFSectionTableList listOfSectionTable = new COFFSectionTableList();
            for (int indx = 0; indx < this.DataStore.NumberOfSections; ++indx)
            {
                listOfSectionTable.Add(
                    this.ReaderStrategy.ReadLayout<COFFSectionTableLayout>());
            }
            this.DataStore.SectionTables = listOfSectionTable;
        }

        /// <summary>
        /// Calculate the position of the symbol table in the file
        /// </summary>
        public void CalculatePointerToSymbolTable()
        {
            if (this.DataStore.IsCOFFFileHeader)
                this.DataStore.PointerToSymbolTable =
                    this.DataStore.IsImportLibrary() ?
                            0 : this.DataStore.CoffFileHeader.Data.PointerToSymbolTable;
            else
                this.DataStore.PointerToSymbolTable =
                    this.DataStore.CoffFileBigHeader.Data.PointerToSymbolTable;
        }

        /// <summary>
        /// Calculate the number of symbols
        /// </summary>
        public void CalculateNumberOfSymbols()
        {
            if (this.DataStore.IsCOFFFileHeader)
                this.DataStore.NumberOfSymbols =
                    this.DataStore.IsImportLibrary() ?
                    0 : this.DataStore.CoffFileHeader.Data.NumberOfSymbols;
            else
                this.DataStore.NumberOfSymbols = this.DataStore.CoffFileBigHeader.Data.NumberOfSymbols;
        }

        /// <summary>
        /// Read the symbol table pointer
        /// </summary>
        public void ReadSymbolTablePointer()
        {
            if (this.DataStore.IsCOFFFileHeader)
            {
                List<LayoutModel<COFFSymbolTableLayout>> listOfSymbolTable = new List<LayoutModel<COFFSymbolTableLayout>>();
                for (int indx = 0; indx < (int)this.DataStore.NumberOfSymbols; ++indx)
                    listOfSymbolTable.Add(new COFFSymbolTableLayoutModel(
                                this.ReaderStrategy.ReadLayout<COFFSymbolTableLayout>()));
                this.DataStore.SymbolTables = listOfSymbolTable;
            }
            else
            {
                List<LayoutModel<COFFSymbolTableBigObjLayout>> listOfSymbolTable = new List<LayoutModel<COFFSymbolTableBigObjLayout>>();
                for (int indx = 0; indx < (int)this.DataStore.NumberOfSymbols; ++indx)
                    listOfSymbolTable.Add(
                                this.ReaderStrategy.ReadLayout<COFFSymbolTableBigObjLayout>());
                this.DataStore.SymbolTablesBigObj = listOfSymbolTable;
            }
        }

        public void InitDataDirTableEntryPointer<TDataDirStruct>(EnumPEStructureId dataDirType,
            EnumPEStructureId dataTableType)
            where TDataDirStruct : struct
        {
            // First, we get the RVA of the table. If the file lacks a pointer to
            // the import table, do nothing.
            LayoutModel<OptionalHeaderDataDirImageOnly> layoutOptHDir = this.DataStore.GetOptHDataDirImages(dataDirType);

            if (layoutOptHDir == null) return;

            // Do nothing if the pointer to table is NULL.
            if (layoutOptHDir.Data.RelativeVirtualAddress == 0) return;

            // -1 because the last entry is the null entry.
            uint noOfDirs = layoutOptHDir.Data.Size / 
                                    LayoutModel<TDataDirStruct>.DataSize - 1;

            // Find the section that contains the RVA. This is needed because the RVA is
            // the import table's memory address which is different from its file offset.
            uint intPtr = GetSectionTableRvaPointer(layoutOptHDir.Data.RelativeVirtualAddress);

            // Move to pointer
            this.ReaderStrategy.SeekOriginal(intPtr);

            List<LayoutModel<TDataDirStruct>> dataDirList = new List<LayoutModel<TDataDirStruct>>();
            for (int indx = 0; indx < (int)noOfDirs; ++indx)
                dataDirList.Add(this.ReaderStrategy.ReadLayout<TDataDirStruct>());

            this.DataStore.SetDirTableEntry(dataTableType, dataDirList);
        }

        public uint GetSectionTableRvaPointer(uint Addr)
        {
            foreach (LayoutModel<COFFSectionTableLayout> sectionTable in this.DataStore.SectionTables)
            {
                if(sectionTable.Data.RelativeVirtualAddress <= Addr && 
                   Addr < (sectionTable.Data.RelativeVirtualAddress + sectionTable.Data.VirtualSize))
                {
                    uint Offset = Addr - sectionTable.Data.RelativeVirtualAddress;
                    return sectionTable.Data.PointerToRawData + Offset;
                }
            }

            return 0;
        }

        #endregion Methods
    }
}
