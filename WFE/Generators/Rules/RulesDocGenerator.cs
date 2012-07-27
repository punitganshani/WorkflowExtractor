using System;
using System.Collections.Generic;
using WFE.Models;
using WFE.Base;
using WFE.Models.Output;
using WFE.Translators;
using WFE.Ooxml;
using Novacode;

namespace WFE.Generators.Rules
{
    public class RulesDocGenerator : RulesOutputGenerator
    {
        public RulesDocGenerator(List<RuleSetDomModel> model, Settings Settings, bool runAsync)
            : base(model, Settings, runAsync)
        {
            if (Settings.GetLanguage() < OutputFormat.Docx)
                throw new NotSupportedException("Output Language not supported:" + Settings.GetLanguage());

            Name = "Rules to Document Generator";
        }

        public override bool Generate()
        {
            CreateDocument();
            return true;
        }

        public void CreateDocument()
        {
            DocX doc = DocX.Create(Settings.GetOutputFile("ruledoc"));

            // will generate sync even if runAsync = true
            for (int i = 0; i < Model.Count; i++)
            {
                if (Model[i] == null)
                    continue;

                CreateRuleSetDocument(doc, Model[i].RuleSet);
            }
            
            doc.Save();
        }

        private static void CreateRuleSetDocument(DocX body, System.Workflow.Activities.Rules.RuleSet ruleSet)
        {
            string ruleSetString = RuleTranslator.ConvertRuleSet(ruleSet, false);

            body.DocumentWorkflow(ruleSet.Name);
            body.DocumentDescription(ruleSetString);
        }
    }
}