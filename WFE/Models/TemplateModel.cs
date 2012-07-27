using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using WFE.Lib;

namespace WFE.Models
{
    [XmlRoot("templates")]
    [Serializable]
    public class TemplateModel
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("type")]
        public List<TemplateRowModel> Data { get; set; }

        public TemplateModel()
        {
            Data = new List<TemplateRowModel>();
        }

        public TemplateRowModel GetTemplateFor(string activityType)
        {
            //TODO: Optimize with a cache
            activityType = activityType.ToLower();
            if (Data.Count(x => x.ActivityType.ToLower() == activityType) > 0)
            {
                return Data.FirstOrDefault(x => x.ActivityType.ToLower() == activityType);
            }
            else
            {
                Log.Debug("Template not found for " + activityType);
                throw new KeyNotFoundException("Description not found in template for activity :" + activityType);
            }
        }
    }
}
