namespace WFE.Forms
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.openAssemblyDialog = new System.Windows.Forms.OpenFileDialog();
            this.openRulesDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableAssembly = new System.Windows.Forms.TableLayoutPanel();
            this.txtResourceData = new System.Windows.Forms.TextBox();
            this.lstResources = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableRules = new System.Windows.Forms.TableLayoutPanel();
            this.lstRuleSets = new System.Windows.Forms.ListBox();
            this.lstRules = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadAssemblyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRulesFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractCSharpRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractVBNetRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.extractWorkflowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runInAsyncModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autogenerateWordDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeLoaded = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableMain = new System.Windows.Forms.TableLayoutPanel();
            this.tabErrorsOutput = new System.Windows.Forms.TabControl();
            this.tabErrors = new System.Windows.Forms.TabPage();
            this.lstErrors = new System.Windows.Forms.ListBox();
            this.tabOutput = new System.Windows.Forms.TabPage();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableAssembly.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableRules.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableMain.SuspendLayout();
            this.tabErrorsOutput.SuspendLayout();
            this.tabErrors.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // openAssemblyDialog
            // 
            this.openAssemblyDialog.FileName = "openFileDialog1";
            this.openAssemblyDialog.Filter = "Assemblies |*.dll";
            // 
            // openRulesDialog
            // 
            this.openRulesDialog.FileName = "openFileDialog1";
            this.openRulesDialog.Filter = "Workflow Rules | *.rules";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPage1);
            this.tabMain.Controls.Add(this.tabPage2);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Margin = new System.Windows.Forms.Padding(0);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Drawing.Point(0, 0);
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(747, 530);
            this.tabMain.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableAssembly);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(739, 504);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Assembly Rules";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableAssembly
            // 
            this.tableAssembly.ColumnCount = 1;
            this.tableAssembly.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableAssembly.Controls.Add(this.txtResourceData, 0, 1);
            this.tableAssembly.Controls.Add(this.lstResources, 0, 0);
            this.tableAssembly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableAssembly.Location = new System.Drawing.Point(3, 3);
            this.tableAssembly.Name = "tableAssembly";
            this.tableAssembly.RowCount = 2;
            this.tableAssembly.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableAssembly.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 352F));
            this.tableAssembly.Size = new System.Drawing.Size(733, 498);
            this.tableAssembly.TabIndex = 6;
            // 
            // txtResourceData
            // 
            this.txtResourceData.BackColor = System.Drawing.Color.White;
            this.txtResourceData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResourceData.Location = new System.Drawing.Point(0, 146);
            this.txtResourceData.Margin = new System.Windows.Forms.Padding(0);
            this.txtResourceData.Multiline = true;
            this.txtResourceData.Name = "txtResourceData";
            this.txtResourceData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResourceData.Size = new System.Drawing.Size(733, 352);
            this.txtResourceData.TabIndex = 5;
            // 
            // lstResources
            // 
            this.lstResources.BackColor = System.Drawing.SystemColors.Info;
            this.lstResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstResources.FormattingEnabled = true;
            this.lstResources.Location = new System.Drawing.Point(0, 0);
            this.lstResources.Margin = new System.Windows.Forms.Padding(0);
            this.lstResources.Name = "lstResources";
            this.lstResources.Size = new System.Drawing.Size(733, 146);
            this.lstResources.TabIndex = 4;
            this.lstResources.SelectedIndexChanged += new System.EventHandler(this.LstResourcesSelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableRules);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(739, 504);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Rules";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableRules
            // 
            this.tableRules.ColumnCount = 2;
            this.tableRules.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableRules.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableRules.Controls.Add(this.lstRuleSets, 0, 0);
            this.tableRules.Controls.Add(this.lstRules, 1, 0);
            this.tableRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableRules.Location = new System.Drawing.Point(0, 0);
            this.tableRules.Name = "tableRules";
            this.tableRules.RowCount = 1;
            this.tableRules.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableRules.Size = new System.Drawing.Size(739, 504);
            this.tableRules.TabIndex = 7;
            // 
            // lstRuleSets
            // 
            this.lstRuleSets.BackColor = System.Drawing.SystemColors.Info;
            this.lstRuleSets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRuleSets.FormattingEnabled = true;
            this.lstRuleSets.Location = new System.Drawing.Point(0, 0);
            this.lstRuleSets.Margin = new System.Windows.Forms.Padding(0);
            this.lstRuleSets.Name = "lstRuleSets";
            this.lstRuleSets.Size = new System.Drawing.Size(369, 504);
            this.lstRuleSets.TabIndex = 5;
            this.lstRuleSets.SelectedIndexChanged += new System.EventHandler(this.LstRuleSetsSelectedIndexChanged);
            // 
            // lstRules
            // 
            this.lstRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRules.FormattingEnabled = true;
            this.lstRules.Location = new System.Drawing.Point(369, 0);
            this.lstRules.Margin = new System.Windows.Forms.Padding(0);
            this.lstRules.Name = "lstRules";
            this.lstRules.Size = new System.Drawing.Size(370, 504);
            this.lstRules.TabIndex = 6;
            this.lstRules.SelectedIndexChanged += new System.EventHandler(this.LstRulesSelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.runToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadAssemblyToolStripMenuItem,
            this.loadRulesFileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadAssemblyToolStripMenuItem
            // 
            this.loadAssemblyToolStripMenuItem.Name = "loadAssemblyToolStripMenuItem";
            this.loadAssemblyToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.loadAssemblyToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.loadAssemblyToolStripMenuItem.Text = "Load &Assembly";
            this.loadAssemblyToolStripMenuItem.Click += new System.EventHandler(this.LoadAssemblyToolStripMenuItemClick);
            // 
            // loadRulesFileToolStripMenuItem
            // 
            this.loadRulesFileToolStripMenuItem.Name = "loadRulesFileToolStripMenuItem";
            this.loadRulesFileToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.loadRulesFileToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.loadRulesFileToolStripMenuItem.Text = "Load &Rules File";
            this.loadRulesFileToolStripMenuItem.Click += new System.EventHandler(this.LoadRulesFileToolStripMenuItemClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(170, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractCSharpRulesToolStripMenuItem,
            this.extractVBNetRulesToolStripMenuItem,
            this.toolStripMenuItem2,
            this.extractWorkflowToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.runToolStripMenuItem.Text = "&Build";
            // 
            // extractCSharpRulesToolStripMenuItem
            // 
            this.extractCSharpRulesToolStripMenuItem.Name = "extractCSharpRulesToolStripMenuItem";
            this.extractCSharpRulesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.extractCSharpRulesToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.extractCSharpRulesToolStripMenuItem.Text = "Extract &C# Rules";
            this.extractCSharpRulesToolStripMenuItem.Click += new System.EventHandler(this.ExtractCSharpRulesToolStripMenuItemClick);
            // 
            // extractVBNetRulesToolStripMenuItem
            // 
            this.extractVBNetRulesToolStripMenuItem.Name = "extractVBNetRulesToolStripMenuItem";
            this.extractVBNetRulesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.extractVBNetRulesToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.extractVBNetRulesToolStripMenuItem.Text = "Extract &VB.Net Rules";
            this.extractVBNetRulesToolStripMenuItem.Click += new System.EventHandler(this.ExtractVbNetRulesToolStripMenuItemClick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(195, 6);
            // 
            // extractWorkflowToolStripMenuItem
            // 
            this.extractWorkflowToolStripMenuItem.Name = "extractWorkflowToolStripMenuItem";
            this.extractWorkflowToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.extractWorkflowToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.extractWorkflowToolStripMenuItem.Text = "Extract &Workflow";
            this.extractWorkflowToolStripMenuItem.Click += new System.EventHandler(this.ExtractWorkflowToolStripMenuItemClick);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runInAsyncModeToolStripMenuItem,
            this.autogenerateWordDocumentToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // runInAsyncModeToolStripMenuItem
            // 
            this.runInAsyncModeToolStripMenuItem.Checked = true;
            this.runInAsyncModeToolStripMenuItem.CheckOnClick = true;
            this.runInAsyncModeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.runInAsyncModeToolStripMenuItem.Name = "runInAsyncModeToolStripMenuItem";
            this.runInAsyncModeToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.runInAsyncModeToolStripMenuItem.Text = "Run in Async mode";
            this.runInAsyncModeToolStripMenuItem.Click += new System.EventHandler(this.runInAsyncModeToolStripMenuItem_Click);
            // 
            // autogenerateWordDocumentToolStripMenuItem
            // 
            this.autogenerateWordDocumentToolStripMenuItem.CheckOnClick = true;
            this.autogenerateWordDocumentToolStripMenuItem.Name = "autogenerateWordDocumentToolStripMenuItem";
            this.autogenerateWordDocumentToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.autogenerateWordDocumentToolStripMenuItem.Text = "Auto-generate Word document";
            this.autogenerateWordDocumentToolStripMenuItem.Click += new System.EventHandler(this.autogenerateWordDocumentToolStripMenuItem_Click);
            // 
            // treeLoaded
            // 
            this.treeLoaded.BackColor = System.Drawing.SystemColors.Info;
            this.treeLoaded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeLoaded.Location = new System.Drawing.Point(0, 0);
            this.treeLoaded.Margin = new System.Windows.Forms.Padding(0);
            this.treeLoaded.Name = "treeLoaded";
            this.treeLoaded.Size = new System.Drawing.Size(233, 666);
            this.treeLoaded.TabIndex = 9;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeLoaded);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableMain);
            this.splitContainer1.Size = new System.Drawing.Size(984, 666);
            this.splitContainer1.SplitterDistance = 233;
            this.splitContainer1.TabIndex = 10;
            // 
            // tableMain
            // 
            this.tableMain.ColumnCount = 1;
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableMain.Controls.Add(this.tabMain, 0, 0);
            this.tableMain.Controls.Add(this.tabErrorsOutput, 0, 1);
            this.tableMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableMain.Location = new System.Drawing.Point(0, 0);
            this.tableMain.Name = "tableMain";
            this.tableMain.RowCount = 2;
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.72973F));
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.27027F));
            this.tableMain.Size = new System.Drawing.Size(747, 666);
            this.tableMain.TabIndex = 8;
            // 
            // tabErrorsOutput
            // 
            this.tabErrorsOutput.Controls.Add(this.tabErrors);
            this.tabErrorsOutput.Controls.Add(this.tabOutput);
            this.tabErrorsOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabErrorsOutput.Location = new System.Drawing.Point(0, 530);
            this.tabErrorsOutput.Margin = new System.Windows.Forms.Padding(0);
            this.tabErrorsOutput.Name = "tabErrorsOutput";
            this.tabErrorsOutput.SelectedIndex = 0;
            this.tabErrorsOutput.Size = new System.Drawing.Size(747, 136);
            this.tabErrorsOutput.TabIndex = 8;
            // 
            // tabErrors
            // 
            this.tabErrors.Controls.Add(this.lstErrors);
            this.tabErrors.Location = new System.Drawing.Point(4, 22);
            this.tabErrors.Name = "tabErrors";
            this.tabErrors.Padding = new System.Windows.Forms.Padding(3);
            this.tabErrors.Size = new System.Drawing.Size(739, 110);
            this.tabErrors.TabIndex = 0;
            this.tabErrors.Text = "Errors";
            this.tabErrors.UseVisualStyleBackColor = true;
            // 
            // lstErrors
            // 
            this.lstErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstErrors.FormattingEnabled = true;
            this.lstErrors.Location = new System.Drawing.Point(3, 3);
            this.lstErrors.Margin = new System.Windows.Forms.Padding(0);
            this.lstErrors.Name = "lstErrors";
            this.lstErrors.Size = new System.Drawing.Size(733, 104);
            this.lstErrors.TabIndex = 0;
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.lstOutput);
            this.tabOutput.Location = new System.Drawing.Point(4, 22);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabOutput.Size = new System.Drawing.Size(739, 110);
            this.tabOutput.TabIndex = 1;
            this.tabOutput.Text = "Output";
            this.tabOutput.UseVisualStyleBackColor = true;
            // 
            // lstOutput
            // 
            this.lstOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.Location = new System.Drawing.Point(3, 3);
            this.lstOutput.Margin = new System.Windows.Forms.Padding(0);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new System.Drawing.Size(733, 104);
            this.lstOutput.TabIndex = 1;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 690);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "WFE - Workflow Extractor by Punit Ganshani";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableAssembly.ResumeLayout(false);
            this.tableAssembly.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableRules.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableMain.ResumeLayout(false);
            this.tabErrorsOutput.ResumeLayout(false);
            this.tabErrors.ResumeLayout(false);
            this.tabOutput.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openAssemblyDialog;
        private System.Windows.Forms.OpenFileDialog openRulesDialog;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox lstResources;
        private System.Windows.Forms.ListBox lstRuleSets;
        private System.Windows.Forms.ListBox lstRules;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadAssemblyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadRulesFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractCSharpRulesToolStripMenuItem;
        private System.Windows.Forms.TextBox txtResourceData;
        private System.Windows.Forms.ToolStripMenuItem extractVBNetRulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem extractWorkflowToolStripMenuItem;
        private System.Windows.Forms.TreeView treeLoaded;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableAssembly;
        private System.Windows.Forms.TableLayoutPanel tableRules;
        private System.Windows.Forms.TableLayoutPanel tableMain;
        private System.Windows.Forms.TabControl tabErrorsOutput;
        private System.Windows.Forms.TabPage tabErrors;
        private System.Windows.Forms.TabPage tabOutput;
        private System.Windows.Forms.ListBox lstErrors;
        private System.Windows.Forms.ListBox lstOutput;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runInAsyncModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autogenerateWordDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;

    }
}

