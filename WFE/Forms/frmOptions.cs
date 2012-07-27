using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WFE.Models.Output;
using WFE.Lib;
using System.IO;

namespace WFE.Forms
{
    public partial class frmOptions : Form
    {
        private List<Settings> _settings;
        public frmOptions()
        {
            InitializeComponent();

            LoadSettingsControls();
        }

        private void LoadSettingsControls()
        {
            _settings = new List<Settings> { new CodeSettings(), new DocumentSettings() };

            for (int i=0; i < _settings.Count; i++)
            {
                var page = new TabPage();
                page.Name = "page" + i.ToString();
                page.Text = _settings[i].GetName();

                var control = new SettingsControl(_settings[i]);
                page.Controls.Add(control);

                tabSettings.TabPages.Add(page);
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string output = Serializer.Serialize<List<Settings>>(_settings, Encoding.UTF8);
                File.WriteAllText(@"C:\\settings.xml", output);
            }
            catch (Exception ex)
            {
                
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
