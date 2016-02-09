using System;
namespace WinSysInfo.MiniFileParser.Model
{
    public class COFFFileHeaderLayoutModel : LayoutModel<COFFFileHeader>
    {
        public DateTime TimeDateStamp { get; set; }

        public COFFFileHeaderLayoutModel(LayoutModel<COFFFileHeader> baseObj)
            :base(baseObj)
        {
            this.TimeDateStamp = new System.DateTime(1970, 1, 1).AddSeconds(base.Data.TimeDateStamp);
        }

        //public bool IsImportLibrary()
        //{
        //    return this.actualData.NumberOfSections == 0XFFFF;
        //}

        //public uint GetNumberOfSections()
        //{
        //    return this.IsImportLibrary() ? 0 : (uint)this.actualData.NumberOfSections;
        //}

        //public uint GetPointerToSymbolTable()
        //{
        //    return this.IsImportLibrary() ? 0 : (uint)this.actualData.PointerToSymbolTable;
        //}

        //public uint GetNumberOfSymbols()
        //{
        //    return this.IsImportLibrary() ? 0 : (uint)this.actualData.NumberOfSymbols;
        //}
    }
}
