using System;
using System.Collections.Generic;
using WFE.Extractors;
using WFE.Models;
using WFE.Lib;
using WFE.Models.Output;

namespace WFE.Base
{
    public abstract class WorkflowOutputGenerator : OutputGenerator
    {
        protected List<ActivityChainModel> _activities;
        protected Settings _settings;
        protected RulesExtractor _rulesExtractor;

        protected WorkflowOutputGenerator(List<ActivityChainModel> activities, 
                                        RulesExtractor rulesExtractor,
                                        Settings Settings)
        {
            if (activities == null)
                throw new ArgumentNullException("activities");

            if (rulesExtractor == null)
                throw new ArgumentNullException("rulesExtractor");

            if (Settings == null)
                throw new ArgumentNullException("Settings");

            _activities = activities;
            _settings = Settings;
            _rulesExtractor = rulesExtractor;
        }

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
