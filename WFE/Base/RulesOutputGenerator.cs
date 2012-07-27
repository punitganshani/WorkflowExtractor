using System;
using System.Collections.Generic;
using WFE.Models;
using WFE.Models.Output;

namespace WFE.Base
{
    public abstract class RulesOutputGenerator : OutputGenerator
    {
        protected List<RuleSetDomModel> Model;
        protected readonly bool RunAsync;
        protected RulesOutputGenerator(List<RuleSetDomModel> model, Settings settings, bool runAsync)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (settings == null)
                throw new ArgumentNullException("settings");

            Model = model;
            Settings = settings;
            RunAsync = runAsync;
        }

        //public abstract bool Generate();

        #region IDisposable Members

        public new void Dispose()
        {
            Model = null;
            Settings = null;
        }

        #endregion
    }
}