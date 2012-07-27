using System;
using System.Collections.Generic;
using System.Text;
using WFE.Models;
using System.IO;
using System.Reflection;

namespace WFE.Lib
{
    public class TemplateReader
    {
        private static readonly string _templateDirectory;
        private static readonly string _defaultTemplateDirectory;
        private static readonly Dictionary<string, TemplateModel> _templates;

        static TemplateReader()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);

            _defaultTemplateDirectory = Path.Combine(Path.GetDirectoryName(path), "Templates");
            _templateDirectory = Environment.CurrentDirectory;
            _templates = new Dictionary<string, TemplateModel>();
        }

        public static TemplateModel ReadTemplate(string fileName)
        {
            string templatePath = Path.Combine(_templateDirectory, fileName);
            if (File.Exists(templatePath) == false) // file not in the user selected directory?
            {
                templatePath = Path.Combine(_defaultTemplateDirectory, fileName);

                if (File.Exists(templatePath) == false) // file not in the program directory?
                    throw new FileNotFoundException("Template not found: " + fileName);
            }

            if (_templates.ContainsKey(fileName))
                return _templates[fileName];

            string content = File.ReadAllText(templatePath);
            var template = Serializer.Deserialize<TemplateModel>(content, Encoding.UTF8);
            _templates[fileName] = template;

            return template;
        }
    }
}
