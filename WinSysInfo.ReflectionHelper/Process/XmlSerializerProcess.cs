using System;
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
            where TXmlRootLayout : class
        {
            TXmlRootLayout objData = null;
            XmlSerializer serializer = new XmlSerializer(typeof(TXmlRootLayout));
            serializer.UnknownAttribute += serializer_UnknownAttribute;
            serializer.UnknownElement += serializer_UnknownElement;
            serializer.UnknownNode += serializer_UnknownNode;

            using (StreamReader reader = new StreamReader(this.FullFilePath))
            {
                objData = (TXmlRootLayout)serializer.Deserialize(reader);
            }

            return objData;
        }

        /// <summary>
        /// Unknown node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            throw new FileLoadException(
                string.Format("Unable to load Xml. Error Level NodeName:{0}, Type:{1}, LineNo:{2}, LinePos:{3}, Text:{4}",
                e.Name, e.NodeType, e.LineNumber, e.LinePosition, e.Text));
        }

        /// <summary>
        /// Unknown element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void serializer_UnknownElement(object sender, XmlElementEventArgs e)
        {
            throw new FileLoadException(
                string.Format("Unable to load Xml. Error Level ElementName:{0}, Value:{1}, LineNo:{2}, LinePos:{3}, Expected:{4}",
                e.Element.Name, e.Element.Value, e.LineNumber, e.LinePosition, e.ExpectedElements));
        }

        /// <summary>
        /// Unknown Attribute exception handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            throw new FileLoadException(
                string.Format("Unable to load Xml. Error Level AttributeName:{0}, Value:{1}, LineNo:{2}, LinePos:{3}, Object:{4}",
                e.Attr.Name, e.Attr.Value, e.LineNumber, e.LinePosition, e.ObjectBeingDeserialized));
        }

        #endregion Methods
    }
}
