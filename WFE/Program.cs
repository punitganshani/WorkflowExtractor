using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WFE.Forms;
using WFE.Models.Output;
using WFE.Lib;
using System.Text;
using System.IO;

namespace WFE
{
    static class Program
    {
        public static List<Settings> Settings { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string path = Path.Combine(Application.StartupPath, "Settings", "Settings.xml");
#if generateSettingsFile
            Settings = new List<Models.Output.Settings>();

            CodeSettings codeSettings = new CodeSettings();
            codeSettings.CreateSubClass = false;
            codeSettings.IncludeComments = true;
            codeSettings.InheritedClasses = new List<string>(new [] {"System.Object"});
            codeSettings.MethodSettings = new MethodSettings();
            codeSettings.MethodSettings.NameFormat = "[workflow]_[ruleSet]_[ruleName]";
            codeSettings.MethodSettings.ParametersFormat = "object model, string expression";
            codeSettings.MethodSettings.Returns = typeof(bool).FullName;
            codeSettings.ModelType = "System.Object";
            codeSettings.Namespace = "WFE.ExtractedCode";
            codeSettings.References = new List<string>(new[] { "System", "System.Collections.Generic", "System.Linq"});

            Settings.Add(codeSettings);
            Settings.Add(new DocumentSettings());

            
            string content = Serializer.Serialize<List<Settings>>(Settings, Encoding.UTF8);
            File.WriteAllText(path , content);
#else
            string content = File.ReadAllText(path);
            Settings = Serializer.Deserialize<List<Settings>>(content, Encoding.UTF8);
#endif

            Application.Run(new frmMain());
        }
    }
}