using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Workflow.Activities.Rules;

namespace WFE.Models
{
    public class RulesDomModel : IDisposable
    {
        public CodeExpression Condition { get; set; }
        public List<CodeStatement> Then { get; set; }
        public List<CodeStatement> Else { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public List<CodeCommentStatement> Comments { get; set; }
        public RuleSet ParentRuleSet { get; set; }

        public RulesDomModel(RuleSet parentRuleSet)
        {
            ParentRuleSet = parentRuleSet;
            Then = new List<CodeStatement>();
            Else = new List<CodeStatement>();
            Comments = new List<CodeCommentStatement>();
        }

        #region IDisposable Members
        public void Dispose()
        {
            Condition = null;
            Then = null;
            Else = null;
            Name = null;
            Comment = null;
            Comments = null;
        }
        #endregion
    }
}
