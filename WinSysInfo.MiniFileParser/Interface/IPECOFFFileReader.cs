namespace WinSysInfo.MiniFileParser.Interface
{
    internal interface IPECOFFFileReader : IFileReader
    {
        #region Methods

        /// <summary>
        /// Read MS DOS Header
        /// </summary>
        void ReadMSDOSHeader();

        /// <summary>
        /// Read MS DOS stub
        /// </summary>
        void ReadMSDOSStub();

        /// <summary>
        /// Check the PE magic bytes. ("PE\0\0") 
        /// </summary>
        /// <returns></returns>
        bool ContainsPEMagicBytes();

        /// <summary>
        /// Initialize NT Header
        /// </summary>
        void ReadNTHeader();

        /// <summary>
        /// Seek through COFF Header
        /// </summary>
        void ReadCOFFHeader();

        /// <summary>
        /// Seek through COFF Header
        /// </summary>
        void ReadCOFFBigObjHeader();

        /// <summary>
        /// It might be a bigobj file, let's check.  Note that COFF bigobj and COFF
        /// </summary>
        void CheckBigObjHeader();

        /// <summary>
        /// Initialize optional header standard fields
        /// </summary>
        void ReadOptHeaderStdFields();

        /// <summary>
        /// Initialize optional header standard fields
        /// </summary>
        void ReadOptHeaderStdFields32();

        /// <summary>
        /// Initialize optional header standard fields
        /// </summary>
        void ReadOptHeaderSpecificFields32();

        /// <summary>
        /// Initialize optional header standard fields
        /// </summary>
        void ReadOptHeaderSpecificFields32Plus();

        /// <summary>
        /// Get Data Directory count
        /// </summary>
        /// <returns></returns>
        void CalculateDataDirectoryCount();

        /// <summary>
        /// Each data directory gives the address and size of a table or string that Windows uses.
        /// These data directory entries are all loaded into memory so that the system can use them
        /// at run time. A data directory is an 8byte field.
        /// </summary>
        void ReadOptHeaderDataDirectoriesImageOnly();

        void CalculateNumberOfSections();
        void ReadSectionTable();
        void CalculatePointerToSymbolTable();
        void CalculateNumberOfSymbols();
        void ReadSymbolTablePointer();

        uint GetSectionTableRvaPointer(uint Addr);

        #endregion Methods
    }
}
