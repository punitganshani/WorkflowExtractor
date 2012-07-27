using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFE.Models.Output
{
    public class DocumentSettings : Settings
    {
        internal DocumentSettings()
            : base("Document")
        {

        }

        internal DocumentSettings(string className, string outputDirectory)
        {
            base.SetInstance(className, outputDirectory, OutputFormat.Docx);
        }
    }
}
