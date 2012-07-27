using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WFE.Models.Output;

namespace WFE.Forms
{
    public partial class SettingsControl : UserControl
    {
        private readonly Settings _settings;
        
        public SettingsControl(Settings settings)
        {
            InitializeComponent();
            this._settings = settings;
        }

        private void btnAddReplacement_Click(object sender, EventArgs e)
        {
            _settings.Replacements.Add(new Replacement
                                  {
                                      Find = txtFind.Text.Trim(),
                                      ReplaceBy = txtReplaceBy.Text.Trim(),
                                      IsRegex = chkIsRegEx.Checked
                                  });


            gridReplacements.DataSource = null;
            gridReplacements.DataSource = _settings.Replacements;
        }
    }
}