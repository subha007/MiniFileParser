using System.IO;
using System.Xml.Serialization;

namespace WinSysInfo.ReflectionHelper.Process
{
    /// <summary>
    /// The class is used to read an xml file into a dataset
    /// </summary>
    public class XmlSerializerProcess
    {
        #region Properties

        /// <summary>
        /// The full Xml file path
        /// </summary>
        public string FullFilePath { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public XmlSerializerProcess(string xmlPath)
        {
            this.FullFilePath = xmlPath;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Reads the xml file into the structure
        /// </summary>
        /// <typeparam name="TXmlRootLayout"></typeparam>
        /// <returns>TXmlRootLayout type data object</returns>
        public TXmlRootLayout ReadDataStructure<TXmlRootLayout>()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TXmlRootLayout));
            using (StreamReader reader = new StreamReader(this.FullFilePath))
            {
                return (TXmlRootLayout)serializer.Deserialize(reader);
            }
        }

        #region Methods
    }
}
