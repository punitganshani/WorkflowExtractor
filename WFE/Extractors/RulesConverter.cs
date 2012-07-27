using System;
using System.Collections.Generic;
using System.Workflow.Activities.Rules;
using System.CodeDom;
using WFE.Lib;
using WFE.Models;
using System.Threading.Tasks;
using WFE.Base;
using WFE.Models.Output;

namespace WFE.Extractors
{
    public sealed class RulesConverter : Events, IDisposable
    {
        private class GeneratorTypes
        {
            public Type Type { get; private set; }

            public Settings Output { get; private set; }

            public GeneratorTypes(Type type, Settings output)
            {
                Output = output;
                Type = type;
            }
        }
        private bool _runAsync;
        private RuleDefinitions _ruleDefinitions;

        private List<RuleSetDomModel> _mainClassModelList;

        private List<GeneratorTypes> _generatorsTypes;

        public List<RuleSetDomModel> MainClassModelList
        {
            get { return _mainClassModelList; }
        }

        public RulesConverter(RulesExtractor ruleExtractor, bool runAsync)
        {
            if (ruleExtractor == null)
                throw new ArgumentNullException("ruleExtractor");

            if (ruleExtractor.RuleDefinitions == null)
            {
                SendMessage("Rule definitions have not been extracted", EventType.Output);
                ruleExtractor.GetRuleDefinitions();
            }

            _ruleDefinitions = ruleExtractor.RuleDefinitions;
            _runAsync = runAsync;
        }

        internal void AddOutput<T>(Settings settings) 
        {
            if (_generatorsTypes == null)
                _generatorsTypes = new List<GeneratorTypes>();

            if (settings == null)
                throw new ArgumentNullException("settings");

            _generatorsTypes.Add(new GeneratorTypes(typeof(T), settings));
        }

        internal bool Generate()
        {
            if (_runAsync)
                return GenerateAsync();

            var conditionTypes = new List<Type>();
            _mainClassModelList = new List<RuleSetDomModel>();

            for (int x = 0; x < _ruleDefinitions.RuleSets.Count; x++)
            {
                RuleSet ruleSet = _ruleDefinitions.RuleSets[x];

                var mainClassModel = new RuleSetDomModel
                {
                    Name = TransformName(ruleSet.Name),
                    RuleSetName = ruleSet.Name,
                    RuleSet = ruleSet
                };

                _mainClassModelList.Add(mainClassModel);

                foreach (var rule in ruleSet.Rules)
                {
                    try
                    {
                        if (rule.Active)
                            SendMessage("Extracting rule " + ruleSet.Name + " >> " + rule.Name, EventType.Output);
                        else
                            SendMessage("Extracting INACTIVE rule " + ruleSet.Name + " >> " + rule.Name, EventType.Output);

                        List<CodeStatement> thenStatements = GetCodeStatements(rule.ThenActions);
                        List<CodeStatement> elseStatements = GetCodeStatements(rule.ElseActions);
                        CodeExpression condition = ((RuleExpressionCondition)(rule.Condition)).Expression;
                        Type expectedType = condition.GetType();

                        if (expectedType != typeof(CodeConditionStatement))
                        {
                            conditionTypes.Add(expectedType);

                            //if (conditionTypes.Contains(condition.GetType()) == false)
                            //{
                            //    Log.Info("Condition is " + condition.GetType());
                            //    conditionTypes.Add(condition.GetType());
                            //}
                            //else
                            //{
                            //    Type expectedType = condition.GetType();
                            //    conditionTypes.Add(condition.GetType());
                            //    //throw new NotImplementedException("No implementation found for condition type " + expectedType);
                            //}
                        }
                        else
                        {
                            throw new NotImplementedException("No implementation found for condition type " + condition.GetType());
                        }

                        mainClassModel.MethodsRuleDom.Add(new RulesDomModel(ruleSet)
                        {
                            Name = TransformName(rule.Name),
                            Else = elseStatements,
                            Then = thenStatements,
                            Condition = condition,
                            Comment = "",
                            Comments = FormatComments(rule, ruleSet)
                        });
                    }
                    catch (Exception exception)
                    {
                        Log.Error("Exception occured while extracting a rule", exception);
                        SendMessage(exception.Message, EventType.Error);
                    }
                };

            };

            bool success = true;

            for (int i = 0; i < _generatorsTypes.Count; i++)
            {
                using (var instance = (RulesOutputGenerator)Activator.CreateInstance(_generatorsTypes[i].Type,
                        MainClassModelList, _generatorsTypes[i].Output, _runAsync))
                {
                    SendMessage("Generating output " + instance.Name, EventType.Output);
                    success &= instance.Generate();
                }
            }

            SendMessage("Rules conversion completed", EventType.Complete);
            OnComplete();

            return success;
        }

        private bool GenerateAsync()
        {
            var conditionTypes = new List<Type>();
            _mainClassModelList = new List<RuleSetDomModel>();

            Parallel.For(0, _ruleDefinitions.RuleSets.Count, x =>
            {
                RuleSet ruleSet = _ruleDefinitions.RuleSets[x];

                var mainClassModel = new RuleSetDomModel
                {
                    Name = TransformName(ruleSet.Name),
                    RuleSetName = ruleSet.Name,
                    RuleSet = ruleSet
                };

                _mainClassModelList.Add(mainClassModel);

                Parallel.ForEach(ruleSet.Rules, rule =>
                {
                    try
                    {
                        if (rule.Active)
                            SendMessage("Extracting rule " + ruleSet.Name + " >> " + rule.Name, EventType.Output);
                        else
                            SendMessage("Extracting INACTIVE rule " + ruleSet.Name + " >> " + rule.Name, EventType.Output);

                        List<CodeStatement> thenStatements = GetCodeStatements(rule.ThenActions);
                        List<CodeStatement> elseStatements = GetCodeStatements(rule.ElseActions);
                        CodeExpression condition = ((RuleExpressionCondition)(rule.Condition)).Expression;
                        Type expectedType = condition.GetType();

                        if (expectedType != typeof(CodeConditionStatement))
                        {
                            conditionTypes.Add(expectedType);

                            //if (conditionTypes.Contains(condition.GetType()) == false)
                            //{
                            //    Log.Info("Condition is " + condition.GetType());
                            //    conditionTypes.Add(condition.GetType());
                            //}
                            //else
                            //{
                            //    Type expectedType = condition.GetType();
                            //    conditionTypes.Add(condition.GetType());
                            //    //throw new NotImplementedException("No implementation found for condition type " + expectedType);
                            //}
                        }
                        else
                        {
                            throw new NotImplementedException("No implementation found for condition type " + condition.GetType());
                        }

                        mainClassModel.MethodsRuleDom.Add(new RulesDomModel(ruleSet)
                        {
                            Name = TransformName(rule.Name),
                            Else = elseStatements,
                            Then = thenStatements,
                            Condition = condition,
                            Comment = "",
                            Comments = FormatComments(rule, ruleSet)
                        });
                    }
                    catch (Exception exception)
                    {
                        Log.Error("Exception occured while extracting a rule", exception);
                        SendMessage(exception.Message, EventType.Error);
                    }
                });

            });

            bool success = true;

            for (int i = 0; i < _generatorsTypes.Count; i++)
            {
                using (var instance = (RulesOutputGenerator)Activator.CreateInstance(_generatorsTypes[i].Type,
                        MainClassModelList, _generatorsTypes[i].Output, _runAsync))
                {
                    SendMessage("Generating output " + instance.Name, EventType.Output);
                    success &= instance.Generate();
                }
            }

            SendMessage("Rules conversion completed", EventType.Complete);
            OnComplete();

            return success;
        }

        private static List<CodeCommentStatement> FormatComments(Rule rule, RuleSet ruleSet)
        {
            var comments = new List<CodeCommentStatement>();
            try
            {
                comments.Add(new CodeCommentStatement("<summary>", true));
                comments.Add(new CodeCommentStatement("RuleSet                          :" + ruleSet.Name ?? string.Empty, true));
                comments.Add(new CodeCommentStatement("RuleSet Chaining                 :" + ruleSet.ChainingBehavior ?? string.Empty, true));
                comments.Add(new CodeCommentStatement("RuleSet Description              :" + ruleSet.Description ?? string.Empty, true));
                comments.Add(new CodeCommentStatement("", true));
                comments.Add(new CodeCommentStatement("Rule Name                        :" + rule.Name ?? string.Empty, true));
                comments.Add(new CodeCommentStatement("Rule Description                 :" + rule.Description ?? string.Empty, true));
                comments.Add(new CodeCommentStatement("Rule IsActive                    :" + rule.Active ?? string.Empty, true));
                comments.Add(new CodeCommentStatement("Rule ReevaluationBehavior        :" + rule.ReevaluationBehavior ?? string.Empty, true));
                comments.Add(new CodeCommentStatement("</summary>", true));
            }
            catch
            {

            }
            return comments;
        }

        private static string TransformName(string input)
        {
            return input.Replace(" ", "_").Replace("(", string.Empty).Replace(")", string.Empty);
        }

        private static List<CodeStatement> GetCodeStatements(IList<RuleAction> ruleActions)
        {
            var statements = new List<CodeStatement>();
            for (int i = 0; i < ruleActions.Count; i++)
            {
                if (ruleActions[i] is RuleHaltAction)
                {
                    Log.Info("RuleHaltAction... not implemented");
                }
                else if (ruleActions[i] is RuleStatementAction)
                {
                    var statement = (CodeStatement)((RuleStatementAction)ruleActions[i]).CodeDomStatement;
                    statements.Add(statement);
                }
            }

            return statements;
        }

        #region IDisposable Members

        public void Dispose()
        {
            _ruleDefinitions = null;
        }

        #endregion

        
    }
}