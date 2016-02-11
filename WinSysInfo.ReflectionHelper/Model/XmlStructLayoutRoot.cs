using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WinSysInfo.ReflectionHelper.Model
{
    [Serializable]
    [XmlRoot("Struct")]
    public class XmlStructLayoutRoot
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("kind")]
        public LayoutKind Kind { get; set; }

        public uint Pack { get; set; }
    }
}
