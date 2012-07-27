using System;
using System.Collections.Generic;
using System.Text;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using WFE.Base;
using WFE.Lib;

namespace WFE.Extractors
{
    public sealed class RulesExtractor : Events, IDisposable
    {
        private List<Stream> _rulesStream;
        private RuleDefinitions _ruleDefinitions;
        private readonly AssemblyResolver _assemblyResolver;

        public RuleDefinitions RuleDefinitions
        {
            get
            {
                if (_ruleDefinitions == null)
                    GetRuleDefinitions();

                return _ruleDefinitions;
            }
        }

        private RulesExtractor(string rulesData)
        {
            _rulesStream = new List<Stream> {new MemoryStream(Encoding.Default.GetBytes(rulesData))};
        }

        private RulesExtractor(Stream stream)
        {
            _rulesStream = new List<Stream> {stream};
        }

        private RulesExtractor(AssemblyResolver assemblyResolver)
        {
            _assemblyResolver = assemblyResolver;
            _rulesStream = new List<Stream>();

            List<string> ruleResources = _assemblyResolver.GetResources();

            for (int i = 0; i < ruleResources.Count; i++)
            {
                string rulesData = _assemblyResolver.GetResourceData(ruleResources[i]).Replace("???<Rule", "<Rule");
                _rulesStream.Add(new MemoryStream(Encoding.Default.GetBytes(rulesData)));
            }
        }

        public static RulesExtractor CreateFromFile(string rulesFile)
        {
            if (string.IsNullOrEmpty(rulesFile))
                throw new ArgumentNullException("rulesFile");

            if (File.Exists(rulesFile) == false)
                throw new FileNotFoundException("Rules file not found: " + rulesFile);

            return new RulesExtractor(File.ReadAllText(rulesFile));
        }

        public static RulesExtractor Create(AssemblyResolver assemblyResolver)
        {
            if (assemblyResolver == null)
                throw new ArgumentNullException("assemblyResolver");

            return new RulesExtractor(assemblyResolver);
        }

        public static RulesExtractor Create(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            return new RulesExtractor(stream);
        }

        public static RulesExtractor Create(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            text = text.Replace("???<Rule", "<Rule");

            return new RulesExtractor(text);
        }

        public void GetRuleDefinitions()
        {
            var serializer = new WorkflowMarkupSerializer();
            _ruleDefinitions = new RuleDefinitions();
            
            Parallel.For(0, _rulesStream.Count, streamCounter =>
                {
                    using (XmlReader reader = new XmlTextReader(_rulesStream[streamCounter]))
                    {
                        var temp = serializer.Deserialize(reader) as RuleDefinitions;

                        if (temp == null) return;

                        if (temp.RuleSets != null)
                        {
                            for (int i=0; i < temp.RuleSets.Count; i++)
                            {
                                _ruleDefinitions.RuleSets.Add(temp.RuleSets[i]);
                            }
                        }
                        if (temp.Conditions != null)
                        {
                            for (int i = 0; i < temp.Conditions.Count; i++)
                            {
                                _ruleDefinitions.Conditions.Add(temp.Conditions[i]);
                            }
                        }
                    }
                });
        }

        #region IDisposable Members

        public void Dispose()
        {
            _ruleDefinitions = null;
            _rulesStream = null;
        }

        #endregion
    }
}
