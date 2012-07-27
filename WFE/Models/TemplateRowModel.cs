using System.Xml.Serialization;

namespace WFE.Models
{
    public class TemplateRowModel
    {
        [XmlAttribute("activityType")]
        public string ActivityType { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }
    }
}