using System;
using System.Collections.Generic;
using WFE.Extractors;
using WFE.Models;
using WFE.Lib;
using WFE.Models.Output;
using WFE.Ooxml;
using Novacode;
using WFE.Translators;
using WFE.Base;

namespace WFE.Generators.Workflow
{
    public class WfDocGenerator : WorkflowOutputGenerator
    {
        private const int LevelToBeCaptured = -1;

        public WfDocGenerator(List<ActivityChainModel> activities, RulesExtractor rulesExtractor, Settings Settings)
            : base(activities, rulesExtractor, Settings)
        {
            if (Settings.GetLanguage() < OutputFormat.Docx)
                throw new NotSupportedException("Output Language not supported:" + Settings.GetLanguage());

            Name = "Workflow to Code Generator";
        }

        public override bool Generate()
        {
            CreateDocument();
            return true;
        }

        public void CreateDocument()
        {
            DocX doc = DocX.Create(_settings.GetOutputFile("wfdoc"));

            CreateWorkflow(doc);

            doc.Save();
        }

        private void CreateWorkflow(DocX body)
        {
            for (int i = 0; i < _activities.Count; i++)
            {
                ActivityChainModel classData = _activities[i];
                body.DocumentWorkflow(classData.WorkflowType.Name, null);

                BuildActivities(body, classData.ActivityTree);
            }
        }

        private void BuildActivities(DocX body, List<ActivityWrapper> rootActivities)
        {
            for (int i = 0; i < rootActivities.Count; i++)
            {
                ActivityWrapper wrapper = rootActivities[i];
                if (wrapper.IsRoot)
                    body.DocumentRootActivity(wrapper.Name);
                else
                    body.DocumentCompositeActivity(wrapper.Name);
                var activityDetails = wrapper.GetActivitySpecificDetails(_rulesExtractor.RuleDefinitions);

                body.DocumentDescription(wrapper.GetDescription());
                body.DocumentPlaceholders("Placeholder for Workflow Snapshot");
                body.DocumentDependencies(wrapper.GetDependentList().ToCsv());
                body.DocumentPlaceholders("Placeholder for Code/UI Snapshot");
                body.DocumentDescription(activityDetails);

                if (wrapper.Children.Count > 0) // has some child activities
                {
                    // composite activity with children
                    for (int childCount = 0; childCount < wrapper.Children.Count; childCount++)
                    {
                        ActivityWrapper childActivity = wrapper.Children[childCount];
                        if (LevelToBeCaptured == -1)
                        {
                            DocumentActivity(body, childActivity);
                            BuildActivities(body, childActivity.Children);
                        }
                        else // check for level 
                        {
                            if (childActivity.Children.Count > 0 && childActivity.Path.Split('.').Length <= LevelToBeCaptured)
                            {
                                DocumentActivity(body, childActivity);
                                BuildActivities(body, childActivity.Children); // composite activity with children
                            }
                            else if (childActivity.Children.Count > 0 && childActivity.Path.Split('.').Length > LevelToBeCaptured) // gone till 3rd level, so just document this
                            {
                                DocumentActivity(body, childActivity);
                            }
                        }
                    }
                }
            }
        }

        private void DocumentActivity(DocX body, ActivityWrapper childActivity)
        {
            body.DocumentLeafActivity(childActivity.Name);
            body.DocumentDescription(childActivity.GetDescription());
            body.DocumentPlaceholders("Placeholder for Workflow Snapshot");
            body.DocumentDependencies(childActivity.GetDependentList().ToCsv());
            body.DocumentPlaceholders("Placeholder for Code/UI Snapshot");
            body.DocumentDescription(childActivity.GetActivitySpecificDetails(_rulesExtractor.RuleDefinitions));
        }
    }
}