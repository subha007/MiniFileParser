namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// Object files contain COFF relocations, which specify how the section data should be 
    /// modified when placed in the image file and subsequently loaded into memory. Image files 
    /// do not contain COFF relocations because all referenced symbols have already been assigned 
    /// addresses in a flat address space. An image contains relocation information in the form 
    /// of base relocations in the .reloc section (unless the image has the 
    /// IMAGE_FILE_RELOCS_STRIPPED attribute).
    /// </summary>
    public struct COFFRelocationsObjectOnly
    {
        /// <summary>
        /// The address of the item to which relocation is applied. This is the offset from the 
        /// beginning of the section, plus the value of the section’s RVA/Offset field
        /// </summary>
        public uint VirtualAddress { get; set; }

        /// <summary>
        /// A zero-based index into the symbol table. This symbol gives the address that is to be 
        /// used for the relocation. If the specified symbol has section storage class, then the 
        /// symbol’s address is the address with the first section of the same name.
        /// </summary>
        public uint SymbolTableIndex { get; set; }

        /// <summary>
        /// A value that indicates the kind of relocation that should be performed. Valid relocation
        /// types depend on machine type.
        /// </summary>
        public ushort Type { get; set; }
    }
}
