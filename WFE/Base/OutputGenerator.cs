using System;
using WFE.Models;
using WFE.Models.Output;

namespace WFE.Base
{
    public abstract class OutputGenerator : Events, IDisposable 
    {
        protected Settings Settings;
        protected string _name;

        public string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        public abstract bool Generate();

        #region IDisposable Members

        public void Dispose()
        {
            _name = null;
        }

        #endregion
    }
}
