using System.Xml.Serialization;

namespace WFE.Models.Output
{
    public class Replacement
    {
        [XmlAttribute("find")]
        public string Find { get; set; }

        [XmlAttribute("replaceBy")]
        public string ReplaceBy { get; set; }

        [XmlAttribute("isRegEx")]
        public bool IsRegex { get; set; }
    }
}
