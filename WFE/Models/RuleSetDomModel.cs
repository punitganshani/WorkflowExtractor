using System.Collections.Generic;
using System;
using System.Workflow.Activities.Rules;

namespace WFE.Models
{
    public class RuleSetDomModel : IDisposable
    {
        public string Name { get; set; }
        public string RuleSetName { get; set; }
        public List<RulesDomModel> MethodsRuleDom { get; set; }
        public RuleSet RuleSet { get; set; }

        public RuleSetDomModel()
        {
            MethodsRuleDom = new List<RulesDomModel>();
        }

        #region IDisposable Members

        public void Dispose()
        {
            Name = null;
            RuleSetName = null;

            if (MethodsRuleDom != null)
            {
                MethodsRuleDom.Clear();
                MethodsRuleDom = null;
            }
        }

        #endregion
    }
}
