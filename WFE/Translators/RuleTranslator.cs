using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.Activities.Rules;
using System.CodeDom;

namespace WFE.Translators
{
    internal static class RuleTranslator
    {
        internal static string ConvertRuleSet(RuleSet ruleSet, bool displayActivityDetails = false)
        {
            var builder = new StringBuilder();

            int counter = 0;
            foreach (var rule in ruleSet.Rules)
            {
                string condition = ConvertRuleCondition(rule.Condition);
                string then = ConvertRuleCondition(rule.ThenActions);
                string elseStatement = ConvertRuleCondition(rule.ElseActions);

                if (string.IsNullOrEmpty(condition)) then = "true";
                if (string.IsNullOrEmpty(then)) then = "NA";
                if (string.IsNullOrEmpty(elseStatement)) elseStatement = "NA";

                if (displayActivityDetails)
                    builder.AppendFormat("This activity executes {0} rules group by name '{1}' ", ruleSet.Rules.Count, ruleSet.Name).AppendLine();
                builder.AppendFormat("Rule : {0} Name : {1}", ++counter, rule.Name).AppendLine();
                builder.AppendLine("*******************************************************");
                builder.Append("Condition:").AppendLine(condition);
                builder.AppendLine("THEN (if true):");
                builder.AppendLine(then);
                builder.AppendLine("ELSE (if false):");
                builder.AppendLine(elseStatement);
                builder.AppendLine("*******************************************************");
            }
            return builder.ToString();
        }

        internal static string ConvertRuleSetReference(RuleSetReference ruleReference, RuleDefinitions ruleDefinitions, bool displayActivityDetails = true)
        {
            var builder = new StringBuilder();
            string ruleSetName = ruleReference.RuleSetName;
            try
            {
                RuleSet ruleSet = ruleDefinitions.RuleSets.First(x => x.Name == ruleSetName);
                if (ruleSet.Rules.Count > 0)
                    builder.Append(ConvertRuleSet(ruleSet, displayActivityDetails));
                else
                    builder.Append("No rules existing for this ruleset: " + ruleSetName);
            }
            catch
            {
                //builder.AppendFormat("This activity executes rules group by name '{0}' ", ruleSetName);
            }
            return builder.ToString();
        }

        internal static string ConvertRuleCondition(IList<RuleAction> actionList)
        {
            var builder = new StringBuilder();
            foreach (RuleAction current in actionList)
            {
                builder.AppendLine(current.ToString());
                //if (current is RuleStatementAction)
                //{
                //    RuleStatementAction action = current as RuleStatementAction;
                //    return action.CodeDomStatement.ToString();

                //}
                //else
                //{
                //    throw new NotSupportedException("RuleAction type: " + current.GetType());
                //}
            }

            return builder.ToString().Trim();
        }

        internal static string ConvertRuleConditionReference(RuleDefinitions ruleDefinitions, RuleConditionReference ruleRef)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("This activity gets executed only if condition '{0}' is true", ruleRef.ConditionName);
            try
            {
                RuleCondition condition = ruleDefinitions.Conditions.First(x => x.Name != null && x.Name == ruleRef.ConditionName);
                builder.AppendFormat("[Condition Code: {0}]", ConvertRuleCondition(condition));
            }
            catch
            {
                // no condition, so don't append anything
            }

            return builder.ToString();
        }

        internal static string ConvertRuleCondition(RuleCondition condition)
        {
            var builder = new StringBuilder();
            if (condition is RuleExpressionCondition)
            {
                builder.Append(ConvertRuleExpressionCondition((RuleExpressionCondition)condition));
            }
            else
            {
                throw new NotSupportedException("Unknown condition type :" + condition.GetType() + " for rule name " + condition.Name);
            }
            return builder.ToString();
        }

        internal static string ConvertRuleExpressionCondition(RuleExpressionCondition expression)
        {
            var builder = new StringBuilder();
            builder.Append(ConvertCodeExpression(expression.Expression));
            return builder.ToString();
        }

        internal static string ConvertCodeExpression(CodeExpression codeExpression)
        {
            if (codeExpression is CodePropertyReferenceExpression)
            {
                var property = codeExpression as CodePropertyReferenceExpression;

                if (property.TargetObject != null)
                {
                    string input = ConvertCodeExpression(property.TargetObject);
                    return String.Format("{0}.{1}", input == string.Empty ? "this" : input, property.PropertyName);
                }
                else
                    return property.PropertyName;
            }
            else if (codeExpression is CodePrimitiveExpression)
            {
                //TODO: Handle it better way
                var primitive = codeExpression as CodePrimitiveExpression;
                return primitive.Value.ToString();
            }
            else if (codeExpression is CodeCastExpression)
            {
                var cast = codeExpression as CodeCastExpression;
                return ConvertCodeExpression(cast.Expression);
            }
            else if (codeExpression is CodeBinaryOperatorExpression)
            {
                var codeBinaryOperatorExpression = codeExpression as CodeBinaryOperatorExpression;
                string left = ConvertCodeExpression(codeBinaryOperatorExpression.Left);
                string op = ConvertOperator(codeBinaryOperatorExpression.Operator.ToString());
                string right = ConvertCodeExpression(codeBinaryOperatorExpression.Right);

                return String.Format("{0} {1} {2}", left, op, right);
            }
            else if (codeExpression is CodeMethodInvokeExpression)
            {
                var methodInvoke = codeExpression as CodeMethodInvokeExpression;
                if (methodInvoke.Parameters.Count > 0)
                {
                    return String.Format("{0}({1})", methodInvoke.Method.MethodName,
                        ConvertParametersToCsv(methodInvoke.Parameters));
                }
                else
                    return String.Format("{0}()", methodInvoke.Method.MethodName);
            }
            else if (codeExpression is CodeMethodReferenceExpression)
            {
                var methodReference = codeExpression as CodeMethodReferenceExpression;
                return String.Format("{0}()", methodReference.MethodName);
            }
            else if (codeExpression is CodeThisReferenceExpression)
            {
                //CodeThisReferenceExpression thisReference = codeExpression as CodeThisReferenceExpression;
                return String.Empty;// String.Format("{0}", thisReference);
            }
            else if (codeExpression is CodeArgumentReferenceExpression)
            {
                var input = codeExpression as CodeArgumentReferenceExpression;
                return input.ParameterName;
            }
            else if (codeExpression is CodeArrayCreateExpression)
            {
                var input = codeExpression as CodeArrayCreateExpression;
                return String.Format("Creating array of {0} of size {1} with values {2}",
                                ConvertCodeExpression(input.SizeExpression),
                                input.Size,
                                ConvertParametersToCsv(input.Initializers));
            }
            else if (codeExpression is CodeArrayIndexerExpression)
            {
                var input = codeExpression as CodeArrayIndexerExpression;
                return String.Format("Accessing an array {0} index {1}", ConvertCodeExpression(input.TargetObject),
                    ConvertParametersToCsv(input.Indices));
            }
            else if (codeExpression is CodeBaseReferenceExpression)
            {
                var input = codeExpression as CodeBaseReferenceExpression;
                return "Executes some server side code (TBD)";
            }
            else if (codeExpression is CodeDefaultValueExpression)
            {
                var input = codeExpression as CodeDefaultValueExpression;
                return "Executes some server side code (TBD)";
            }
            else if (codeExpression is CodeDirectionExpression)
            {
                var input = codeExpression as CodeDirectionExpression;
                return String.Format("{0} with direction {1}", ConvertCodeExpression(input.Expression), input.Direction.ToString());
            }
            else if (codeExpression is CodeEventReferenceExpression)
            {
                var input = codeExpression as CodeEventReferenceExpression;
                return String.Format("Event raised {0} on target {1}", input.EventName, ConvertCodeExpression(input.TargetObject));
            }
            else if (codeExpression is CodeArgumentReferenceExpression)
            {
                var input = codeExpression as CodeArgumentReferenceExpression;
                return input.ParameterName;
            }
            else if (codeExpression is CodeFieldReferenceExpression)
            {
                var input = codeExpression as CodeFieldReferenceExpression;
                return String.Format("{0}.{1}", input.FieldName, ConvertCodeExpression(input.TargetObject));
            }
            else if (codeExpression is CodeTypeReferenceExpression)
            {
                return string.Empty;
                //CodeTypeReferenceExpression input = codeExpression as CodeTypeReferenceExpression;
            }
            else
            {
                return string.Empty;
                // throw new NotSupportedException("Expression Conversion to Word Document not supported: " + codeExpression.GetType());
            }
        }

        internal static string ConvertParametersToCsv(CodeExpressionCollection parameters)
        {
            if (parameters.Count == 0)
                return "None";

            string output = parameters.Cast<CodeExpression>().Aggregate(string.Empty,
                        (current, expression) => current + (ConvertCodeExpression(expression) + "  "));

            return output.TrimEnd().Replace("  ", ", ");
        }

        internal static string ConvertOperator(string input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            return input.Replace("ValueEquality", "==")
                .Replace("Add", "+")
                .Replace("Subtract", "-")
                .Replace("Multiply", "*")
                .Replace("Divide", "/")
                .Replace("Modulus", "%")
                .Replace("Assign", "=")
                .Replace("IdentityInequality", "!=")
                .Replace("IdentityEquality", "==")
                .Replace("BitwiseOr", "|")
                .Replace("BitwiseAnd", "&")
                .Replace("BooleanOr", "||")
                .Replace("BooleanAnd", "&&")
                .Replace("LessThan", "<")
                .Replace("LessThanOrEqual", "<=")
                .Replace("GreaterThan", ">")
                .Replace("GreaterThanOrEqual", ">=");
        }
    }
}
