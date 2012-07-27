using System;
using System.IO;
using System.Windows.Forms;
using WFE.Extractors;
using WFE.Lib;
using WFE.Models;
using System.Workflow.ComponentModel;
using WFE.Generators.Rules;
using System.ComponentModel;
using System.Workflow.Activities.Rules.Design;
using System.Workflow.Activities.Rules;
using WFE.Models.Output;

namespace WFE.Forms
{
    public partial class frmMain : Form
    {
        private AssemblyResolver _assemblyResolver;
        private bool? _processAssembly;
        private bool _runAsync;
        private bool _generateWordDocumentAutomatically;

        public frmMain()
        {
            InitializeComponent();
            _runAsync = runInAsyncModeToolStripMenuItem.Checked;
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_assemblyResolver != null)
                _assemblyResolver.Dispose();

            Application.Exit();
        }

        private void LoadAssemblyToolStripMenuItemClick(object sender, EventArgs e)
        {
            var result = openAssemblyDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _processAssembly = true;

                UpdateUI(openAssemblyDialog.FileName);
            }
        }

        private void LoadRulesFileToolStripMenuItemClick(object sender, EventArgs e)
        {
            var result = openRulesDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _processAssembly = false;

                UpdateUI(openRulesDialog.FileName);
            }
        }

        private void LstResourcesSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstResources.SelectedIndex == -1 || lstResources.Items.Count <= 0 || _processAssembly == null)
                return;

            string itemSelected = lstResources.SelectedItem.ToString();

            txtResourceData.Text = _processAssembly.Value == true
                                    ? _assemblyResolver.GetResourceData(itemSelected)
                                    : File.ReadAllText(itemSelected);
        }

        private void ExtractCSharpRulesToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (ValidateBeforeExtractingRules() == false)
                return;

            var inputs = new object[] { OutputFormat.CSharp, lstResources.SelectedItem.ToString() };

            if (_runAsync)
            {
                var backgroundRulesExtract = new BackgroundWorker();
                backgroundRulesExtract.DoWork += BackgroundRulesExtractDoWork;
                backgroundRulesExtract.RunWorkerAsync(inputs);
            }
            else
            {
                BackgroundRulesExtractDoWork(this, new DoWorkEventArgs(inputs));
            }

            UpdateOutput(this, new MessageEventArgs("Rules extraction started for " + lstResources.SelectedItem,
                Base.EventType.Started));
        }

        private void BackgroundRulesExtractDoWork(object sender, DoWorkEventArgs e)
        {
            //if (this.InvokeRequired)
            //    this.Invoke(new EventHandler<DoWorkEventArgs>(backgroundRulesExtract_DoWork), new object[] { sender, e });

            var inputs = (object[])e.Argument;

            ProcessRules((OutputFormat)inputs[0], inputs[1].ToString());
        }

        private void ExtractVbNetRulesToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (ValidateBeforeExtractingRules() == false)
                return;

            if (_runAsync)
            {
                var backgroundRulesExtract = new BackgroundWorker();
                backgroundRulesExtract.DoWork += BackgroundRulesExtractDoWork;
                backgroundRulesExtract.RunWorkerAsync(OutputFormat.VBNet);
            }
            else
            {
                BackgroundRulesExtractDoWork(this, new DoWorkEventArgs(OutputFormat.VBNet));
            }

            if (lstResources.SelectedItem != null)
            {
                UpdateOutput(this, new MessageEventArgs("Workflow extraction started for " + lstResources.SelectedItem,
                                                        Base.EventType.Started));
            }
        }

        private void BackgroundWfExtractDoWork(object sender, DoWorkEventArgs e)
        {
            //if (this.InvokeRequired)
            //    this.Invoke(new EventHandler<DoWorkEventArgs>(backgroundRulesExtract_DoWork), new object[] { sender, e });

            ProcessWorkflow();
        }

        private void ExtractWorkflowToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_processAssembly.HasValue == false || (_processAssembly.Value == false))
            {
                MessageBox.Show(@"Please load an assembly to process workflow");
                return;
            }

            if (_runAsync)
            {
                var backgroundWfExtract = new BackgroundWorker();
                backgroundWfExtract.DoWork += BackgroundWfExtractDoWork;
                backgroundWfExtract.RunWorkerAsync();
            }
            else
            {
                BackgroundWfExtractDoWork(this, null);
            }
        }

        private void LstRuleSetsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRuleSets.SelectedIndex != -1)
            {
                var item = (BindItem)lstRuleSets.SelectedItem;
                var model = (RuleSetDomModel)item.Value;
                lstRules.Items.Clear();
                model.MethodsRuleDom.ForEach(x => lstRules.Items.Add(new BindItem(x.Name, x)));
            }
        }

        private void ConverterOnMessageReceived(object sender, MessageEventArgs e)
        {
            ReceivedMessage(e);
        }

        private void ReceivedMessage(MessageEventArgs e)
        {
            if (e.Type == Base.EventType.Error)
            {
                UpdateError(this, e);
            }
            else if (e.Type == Base.EventType.Output || e.Type == Base.EventType.Complete || e.Type == Base.EventType.Started)
            {
                UpdateOutput(this, e);
            }
        }

        private void RulesConverterOnMessageReceived(object sender, MessageEventArgs e)
        {
            ReceivedMessage(e);
        }

        private void RulesExtractorOnMessageReceived(object sender, MessageEventArgs e)
        {
            ReceivedMessage(e);
        }

        private void WorkflowExtractorOnMessageReceived(object sender, MessageEventArgs e)
        {
            ReceivedMessage(e);
        }

        private void UpdateError(object sender, MessageEventArgs e)
        {

            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new EventHandler<MessageEventArgs>(this.UpdateError), new object[] { sender, e });
                else
                {
                    if (e.CallStack.Count > 2)
                        lstErrors.Items.Insert(0, "[" + e.CallStack[2] + "] " + e.Message);
                    else
                        lstErrors.Items.Insert(0, "[unknown] " + e.Message);
                }

            }
            catch
            {

            }
        }

        private void UpdateOutput(object sender, MessageEventArgs e)
        {
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new EventHandler<MessageEventArgs>(this.UpdateOutput), new object[] { sender, e });
                else
                {
                    //if (e.Type == Base.EventType.Complete)
                    lstOutput.Items.Insert(0, "[" + sender + "] " + e.Message);
                }
            }
            catch
            {

            }
        }

        private bool ValidateBeforeExtractingRules()
        {
            if (_processAssembly.HasValue == false)
            {
                MessageBox.Show(@"Please load an assembly or a rule file");
                return false;
            }

            if (lstResources.Items.Count == 0 && _processAssembly.Value) // file has been loaded, but nothing embedded in it?
            {
                MessageBox.Show(@"The loaded assembly has no rule file embedded in it. Please try again with another assembly!");
                return false;
            }

            if (lstResources.SelectedIndex == -1)
            {
                MessageBox.Show(@"Please select a rule file from the list of displayed files and retry!");
                return false;
            }

            return true;
        }

        private void ProcessRules(OutputFormat language, string ruleInput)
        {
            try
            {
                var outputCode = Program.Settings.Find<CodeSettings>();
                outputCode.SetInstance(FileUtils.GetFileNameOnly(ruleInput), Environment.CurrentDirectory, language);
                var outputDocument = Program.Settings.Find<DocumentSettings>();
                outputDocument.SetInstance(FileUtils.GetFileNameOnly(ruleInput), Environment.CurrentDirectory, OutputFormat.Docx);

                if (_processAssembly != null)
                {
                    RulesExtractor rulesExtractor;

                    if (_processAssembly.Value) // assembly loading
                    {
                        rulesExtractor = RulesExtractor.Create(_assemblyResolver.GetResourceData(ruleInput));
                        rulesExtractor.OnMessageReceived += RulesExtractorOnMessageReceived;
                    }
                    else // rule file loading
                    {
                        rulesExtractor = RulesExtractor.CreateFromFile(ruleInput);
                        rulesExtractor.OnMessageReceived += RulesExtractorOnMessageReceived;
                    }

                    using (var rulesConverter = new RulesConverter(rulesExtractor, _runAsync))
                    {
                        rulesConverter.AddOutput<RulesCodeGenerator>(outputCode);

                        if (_generateWordDocumentAutomatically)
                            rulesConverter.AddOutput<RulesDocGenerator>(outputDocument);

                        rulesConverter.OnMessageReceived += RulesConverterOnMessageReceived;
                        rulesConverter.OnProcessingComplete += RulesConverterOnProcessingComplete;

                        rulesConverter.Generate();
                    }
                }
            }
            catch
            {

            }
        }

        private void ProcessWorkflow()
        {
            //TODO: Find a better way of doing this...
            if (treeLoaded == null) return;

            var inputFile = (BindItem)treeLoaded.Nodes[0].Tag;

            var outputDocument = Program.Settings.Find<DocumentSettings>();
            outputDocument.SetInstance(FileUtils.GetFileNameOnly(inputFile.Name), Environment.CurrentDirectory, OutputFormat.Docx);

            if (_assemblyResolver == null) return;

            var workflowExtractor = new WorkflowExtractor(_assemblyResolver);
            workflowExtractor.OnMessageReceived += WorkflowExtractorOnMessageReceived;
            workflowExtractor.GetActivityTree();

            RulesExtractor rulesExtractor = RulesExtractor.Create(_assemblyResolver);
            rulesExtractor.OnMessageReceived += RulesExtractorOnMessageReceived;

            using (var rulesConverter = new RulesConverter(rulesExtractor, _runAsync))
            {
                rulesConverter.OnMessageReceived += RulesConverterOnMessageReceived;
                rulesConverter.OnProcessingComplete += RulesConverterOnProcessingComplete;
            }

            using (var converter = new WorkflowConverter(workflowExtractor, rulesExtractor))
            {
                converter.OnMessageReceived += ConverterOnMessageReceived;
                converter.OnProcessingComplete += ConverterOnProcessingComplete;

                converter.AddOutput<Generators.Workflow.WfDocGenerator>(outputDocument);
                converter.Generate();
            }
        }

        private void RulesConverterOnProcessingComplete(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new EventHandler(RulesConverterOnProcessingComplete), new object[] { sender, e });

            var converter = (RulesConverter)sender;

            lstRuleSets.Items.Clear();
            converter.MainClassModelList.ForEach(x => lstRuleSets.Items.Add(new BindItem(x.Name, x)));
        }

        private void ConverterOnProcessingComplete(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new EventHandler(ConverterOnProcessingComplete), new object[] { sender, e });
        }

        private void UpdateUI(string inputFile)
        {
            treeLoaded.Nodes.Clear();
            var item = new BindItem(FileUtils.GetFileNameOnly(inputFile), inputFile);
            treeLoaded.Nodes.Add(new TreeNode { Text = item.Name, Tag = item });

            if (_processAssembly == null) return;

            if (_processAssembly.Value == false) // process rules
            {
                inputFile = FileUtils.Copy(inputFile);
                lstResources.Items.Add(inputFile);
            }
            else // process assembly
            {
                try
                {
                    string assemblyPath = FileUtils.Copy(inputFile);

                    // load assembly in memory
                    _assemblyResolver = new AssemblyResolver(assemblyPath);
                    _assemblyResolver.OnAssemblyLoaded += AssemblyResolverOnAssemblyLoaded;
                    _assemblyResolver.Load();
                }
                catch (ArgumentNullException argumentException)
                {
                    MessageBox.Show(@"Some technical error occured : " + argumentException.Message);
                }
                catch (InvalidOperationException loadException)
                {
                    MessageBox.Show(loadException.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Unknown error occured : " + ex.Message);
                }
            }
        }

        private void AssemblyResolverOnAssemblyLoaded(object sender, EventArgs e)
        {
            var types = _assemblyResolver.GetResources();

            if (lstResources.InvokeRequired)
                lstResources.Invoke(new EventHandler(AssemblyResolverOnAssemblyLoaded), new[] { sender, e });

            // load types in the list box
            types.ForEach(x => lstResources.Items.Add(x));
        }

        private void LstRulesSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRules.SelectedIndex != -1)
            {
                var item = (BindItem)lstRules.SelectedItem;

                if (item == null)
                    return;

                var model = (RulesDomModel)item.Value;
                var set = (RuleSet)model.ParentRuleSet;

                using (var dialog = new RuleSetDialog(typeof(Activity), null, set))
                {
                    dialog.ShowDialog();
                }
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOptions options = new frmOptions();
            options.ShowDialog();
        }

        private void runInAsyncModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _runAsync = runInAsyncModeToolStripMenuItem.Checked;
        }

        private void autogenerateWordDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _generateWordDocumentAutomatically = autogenerateWordDocumentToolStripMenuItem.Checked;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog();
        }
    }
}