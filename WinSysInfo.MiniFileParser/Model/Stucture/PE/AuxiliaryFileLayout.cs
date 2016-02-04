namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// This format follows a symbol-table record with storage class FILE (103). The symbol name
    /// itself should be .file, and the auxiliary record that follows it gives the name of a 
    /// source-code file.
    /// </summary>
    public class AuxiliaryFileLayout
    {
        /// <summary>
        /// An ANSI string that gives the name of the source file. This is padded with nulls if
        /// it is less than the maximum length
        /// </summary>
        public string FileName { get; set; }
    }
}
