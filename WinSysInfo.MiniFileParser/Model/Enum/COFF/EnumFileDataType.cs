namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// Defines a set of file types to preset while parsing a file.
    /// This identifies the type of file reader to choose and used to
    /// preset file parsing. E.g., specifying 'TEXT' will use a Text Reader
    /// and not a Binary reader or a Xml Reader
    /// </summary>
    public enum EnumFileDataType
    {
        /// <summary>
        /// Using this option might make the parsing a bit slow.
        /// As the parser will try to recognise the type of file
        /// using its own inteligence.
        /// </summary>
        NONE = 0,

        /// <summary>
        /// Using this option will use a Binary Reader
        /// </summary>
        BINARY = 1,

        /// <summary>
        /// Using this option will use a Text Reader
        /// </summary>
        TEXT = 2,

        /// <summary>
        /// Using this option will use a Xml raeder
        /// </summary>
        XML = 3
    }
}
