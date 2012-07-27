using System;
using System.Collections.Generic;
using System.Text;
using System.Workflow.ComponentModel;
using WFE.Lib;
using WFE.Models;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Text.RegularExpressions;

namespace WFE.Translators
{
    internal static class ActivityTranslators
    {
        private static readonly TemplateModel _descriptionTemplate;

        static ActivityTranslators()
        {
            _descriptionTemplate = TemplateReader.ReadTemplate("template-description.xml");
        }

        public static string GetTypeInformation(this ActivityWrapper activityWrapper)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Type :{0}", activityWrapper.Current.GetType().FullName).AppendLine();
            builder.AppendFormat("Depth:{0}", activityWrapper.Depth).AppendLine();
            builder.AppendFormat("Root :{0}", activityWrapper.IsRoot).AppendLine();
            builder.AppendFormat("Child:{0}", activityWrapper.Children.Count).AppendLine();
            return builder.ToString();
        }

        public static string GetDescription(this ActivityWrapper activityWrapper)
        {
            Activity activity = activityWrapper.Current;
            string key = activity.GetType().ToString();
            try
            {
                TemplateRowModel data = _descriptionTemplate.GetTemplateFor(key);
                return Translate(activityWrapper, data.Description);
            }
            catch (KeyNotFoundException)
            {
                return String.Format("TBD <<Please add a description here, or alternatively, provide a template for {0} >>", key);
            }
        }

        public static string GetActivitySpecificDetails(this ActivityWrapper activityWrapper, RuleDefinitions ruleDefinitions)
        {
            var builder = new StringBuilder();
            builder.Append(ConvertActivity(activityWrapper.Current, ruleDefinitions));
            return builder.ToString();
        }

        private static string ConvertActivity(Activity current, RuleDefinitions ruleDefinitions)
        {
            var builder = new StringBuilder();
            if (current is IfElseActivity)
            {
                //var activity = current as IfElseActivity;
                //if (activity.Activities.Count > 0) // there are branches if..else..activity
                //{
                //    string[] dependents = activity.Activities.Select(x => x.Name).ToArray();

                //    builder.AppendFormat("There are {0} conditions executed in this scenario namely -", activity.Activities.Count)
                //        .AppendLine(dependents.ToCSV())
                //        .AppendLine();
                //}
            }
            else if (current is IfElseBranchActivity)
            {
                var activity = current as IfElseBranchActivity;
                builder.AppendLine(ConvertActivityCondition(activity.Condition, ruleDefinitions));
            }
            else if (current is WhileActivity)
            {
                var activity = current as WhileActivity;
                builder.AppendLine(ConvertActivityCondition(activity.Condition, ruleDefinitions));
            }
            //else if (current is SequenceActivity)
            //{

            //}
            //else if (current is ListenActivity)
            //{

            //}
            //else if (current is StateActivity)
            //{

            //}
            //else if (current is StateInitializationActivity)
            //{

            //}
            else if (current is SetStateActivity)
            {
                var activity = current as SetStateActivity;
                builder.AppendFormat("This activity sets the next state of the workflow to {0}", activity.TargetStateName).AppendLine();
            }
            //else if (current is EventDrivenActivity)
            //{

            //}
            else if (current is PolicyActivity)
            {
                var activity = current as PolicyActivity;
                builder.AppendLine(RuleTranslator.ConvertRuleSetReference(activity.RuleSetReference, ruleDefinitions));
            }
            else
            {
                Log.Debug("Not supported: " + current.GetType());
            }

            return builder.ToString().TrimEnd(Environment.NewLine.ToCharArray());
        }

        private static string ConvertActivityCondition(ActivityCondition condition, RuleDefinitions ruleDefinitions)
        {
            var builder = new StringBuilder();
            if (condition != null)
            {
                if (condition is RuleConditionReference)
                {
                    var ruleRef = (RuleConditionReference)condition;
                    builder.AppendLine(RuleTranslator.ConvertRuleConditionReference(ruleDefinitions, ruleRef));
                }
                else
                {
                    throw new NotSupportedException("Type not found:" + condition.GetType());
                }
            }
            else
            {
                builder.AppendLine("This activity gets executed if the other conditions fail in the evaluation process");
            }
            return builder.ToString();
        }

/*
        private static string FormatUserData(System.Collections.IDictionary iDictionary)
        {
            var builder = new StringBuilder();
            foreach (var key in iDictionary.Keys)
            {
                builder.AppendFormat("Key: {0}    Value: {1}", key, iDictionary[key] ?? "-").AppendLine();
            }
            return builder.ToString();
        }
*/

        private static string Translate(ActivityWrapper activityWrapper, string inputFromTemplate)
        {
            var regEx = new Regex("[$](?<props>[a-zA-Z0-9]*)");
            MatchCollection matchCollection = regEx.Matches(inputFromTemplate);
            string output = inputFromTemplate;

            for (int matchCounter = 0; matchCounter < matchCollection.Count; matchCounter++)
            {
                Match match = matchCollection[matchCounter];
                if (match.Success) // has some keywords
                {
                    Group group = match.Groups["props"];
                    CaptureCollection captures = group.Captures;

                    for (int i = 0; i < captures.Count; i++)
                    {
                        string key = captures[i].Value;
                        switch (key)
                        {
                            case "name":
                                output = output.Replace("$name", activityWrapper.Name);
                                break;
                            case "dependents":
                                output = output.Replace("$dependents", activityWrapper.GetDependentList().ToCsv());
                                break;
                            default:
                                try
                                {
                                    var value = activityWrapper.Current.GetPublicPropertValue<string>(key);
                                    output = output.Replace("$" + key, key + ":" + value);
                                }
                                catch
                                {
                                    try
                                    {
                                        var value = activityWrapper.GetPublicPropertValue<string>(key);
                                        output = output.Replace("$" + key, key + ":" + value);
                                    }
                                    catch
                                    {
                                        output = output.Replace("$" + key, key + ":" + "TBD");
                                    }
                                }
                                break;
                        }
                    }
                }
            }

            return output;
        }
    }
}
