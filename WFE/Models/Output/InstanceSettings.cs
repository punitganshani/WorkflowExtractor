using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WFE.Models.Output
{
    public class InstanceSettings
    {
        [XmlAttribute("input")]
        internal string InputFile { get; set; }
        [XmlAttribute("language")]
        internal OutputFormat Language { get; set; }
        [XmlAttribute("outputDir")]
        internal string OutputDirectory { get; set; }

        internal string GetOutputFile(string suffix)
        {
            if (string.IsNullOrEmpty(OutputDirectory) || string.IsNullOrEmpty(InputFile))
                throw new NullReferenceException("Either output directory, or input is not set");

            return Path.Combine(OutputDirectory, InputFile + "-" + suffix + GetPrefix());
        }

        private string GetPrefix()
        {
            switch (Language)
            {
                case OutputFormat.CSharp:
                    return ".cs";
                case OutputFormat.VBNet:
                    return ".vb";
                case OutputFormat.Docx:
                    return ".docx";
                default:
                    return ".txt";
            }
        }

        internal InstanceSettings()
        {
            
        }

        internal InstanceSettings(string className, string outputDirectory, OutputFormat language)
        {
            InputFile = className;
            OutputDirectory = outputDirectory;
            Language = language;
        }
    }
}
