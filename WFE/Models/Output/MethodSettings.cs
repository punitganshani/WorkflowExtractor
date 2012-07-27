using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WFE.Models.Output
{
    public class MethodSettings
    {
        [XmlAttribute("nameFormat")]
        public string NameFormat { get; set; }

        [XmlAttribute("returns")]
        public string Returns { get; set; }

        [XmlAttribute("paramFormat")]
        public string ParametersFormat { get; set; }

        public MethodSettings()
        {
            NameFormat = "[workflow]_[ruleSet]_[ruleName]";
            ParametersFormat = "object model, string expression";
            Returns = typeof(bool).FullName;
        }
    }
}
