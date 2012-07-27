using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WFE.Models.Output
{
    [XmlInclude(typeof(CodeSettings))]
    [XmlInclude(typeof(DocumentSettings))]
    public abstract class Settings
    {
        [XmlElement("replacements")]
        public List<Replacement> Replacements {get; set;}
        
        [XmlElement("instance")]
        public InstanceSettings Instance { get; set; }

        [XmlAttribute("name")]
        protected string Name { get; set; }

        protected Settings(string name = "NA")
        {
            Replacements = new List<Replacement>();
            Name = name;
        }

        internal string GetName()
        {
            return Name;
        }

        internal string GetOutputFile(string suffix)
        {
            if (Instance == null)
                throw new ApplicationException("Instance Settings are not initialized.");

            return Instance.GetOutputFile(suffix);
        }

        internal string GetOutputDirectory()
        {
            if (Instance == null)
                throw new ApplicationException("Instance Settings are not initialized.");

            return Instance.OutputDirectory;
        }

        internal string GetInputFile()
        {
            if (Instance == null)
                throw new ApplicationException("Instance Settings are not initialized.");

            return Instance.InputFile;
        }

        internal OutputFormat GetLanguage()
        {
            if (Instance == null)
                throw new ApplicationException("Instance Settings are not initialized.");

            return Instance.Language;
        }

        public void SetInstance(string className, string outputDirectory, OutputFormat language)
        {
            Instance = new InstanceSettings(className, outputDirectory, language);
        }

        protected void AddReplacement(string find, string replaceBy, bool isRegex)
        {
            Replacements.Add(new Replacement {Find = find, ReplaceBy = replaceBy, IsRegex = isRegex});
        }
    }
}