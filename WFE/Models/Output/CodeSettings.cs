using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace WFE.Models.Output
{
    public class CodeSettings : Settings
    {
        [XmlElement("ruleDefinition")]
        public MethodSettings MethodSettings { get; set; }

        [XmlAttribute("ruleSetClasses")]
        public bool CreateSubClass { get; set; }

        [XmlAttribute("comments")]
        public bool IncludeComments { get; set; }

        [XmlAttribute("namespace")]
        public string Namespace { get; set; }

        [XmlElement("inheritance")]
        public List<string> InheritedClasses { get; set; }

        [XmlElement("references")]
        public List<string> References { get; set; }

        [XmlElement("modelType")]
        public string ModelType { get; set; }

        internal CodeSettings()
            : base("Code")
        {
            MethodSettings = new Output.MethodSettings();
            CreateSubClass = false;
            InheritedClasses = new List<string>();
            References = new List<string> {"System"};
            IncludeComments = true;
            ModelType = "System.Object";
        }

        internal CodeSettings(string className, string outputDirectory, OutputFormat language = OutputFormat.CSharp)
        {
            base.SetInstance(className, outputDirectory, language);
        }
    }
}