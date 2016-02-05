namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// Specifies the type of Reader
    /// </summary>
    public enum EnumFileReaderType
    {
        /// <summary>
        /// No reader or unknown reader
        /// </summary>
        NONE = 0,

        /// <summary>
        /// Uses custom class <see cref="MemorySequentialAccess"/> which reads memory buffer sequentially
        /// Useful for reading large files
        /// </summary>
        MEMORY_SEQ_READ = 1,

        /// <summary>
        /// uses custom class <see cref="MemoryRandomAccess"/> which reads memory buffer randomly
        /// Useful for reading large files
        /// </summary>
        MEMORY_ACCESSOR_READ = 2,

        /// <summary>
        /// Using raw binary reader <see cref="System.IO.BinaryReader"/>
        /// </summary>
        BINARY_READ = 3,

        /// <summary>
        /// Using raw character by character reader <see cref="System.IO.TextReader"/>
        /// </summary>
        TEXT_READ = 4,

        /// <summary>
        /// Text Reader using buffer. Uses <see cref="System.IO.StreamReader"/>
        /// </summary>
        STREAM_READ = 5,

        /// <summary>
        /// Used for reading all the data from file <see cref="System.IO.File.ReadAllText"/>
        /// </summary>
        READ_ALL_TEXT = 6,

        /// <summary>
        /// Represents a reader that provides fast, non-cached, forward-only access to XML data.
        /// <see cref="System.Xml.XmlReader"/>
        /// </summary>
        XML_READ = 7,

        /// <summary>
        /// You can use this class to load, validate, edit, add, and position XML in a document. 
        /// <see cref="System.Xml.XmlDocument"/>
        /// </summary>
        XML_DOM_READ = 8,
    }
}
