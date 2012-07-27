using System;
using System.Collections.Generic;
using System.Workflow.ComponentModel;
using WFE.Lib;

namespace WFE.Models
{
    public sealed class ActivityChainModel
    {
        public Type WorkflowType { get; set; }
        public object Workflow { get; set; }

        public List<ActivityWrapper> UnresolvedActivities { get; private set; }
        public List<string> BaseTypes { get; private set; }
        public List<string> DeclarationTypes { get; private set; }
        public List<Exception> Errors { get; private set; }
        public List<ActivityWrapper> ActivityTree { get; private set; }

        public ActivityChainModel()
        {
            UnresolvedActivities = new List<ActivityWrapper>();
            BaseTypes = new List<string>();
            DeclarationTypes = new List<string>();
            Errors = new List<Exception>();
            ActivityTree = new List<ActivityWrapper>();
        }

        public void AddActivity(Activity input)
        {
            var path = ((Activity)input).GetPrivateMethodValue<string>("get_DottedPath");
            
            bool isRoot = (input.Parent == null) || (path.Contains(".") == false);

            if (isRoot)
                ActivityTree.Add(new ActivityWrapper(input, path));
            else
                UnresolvedActivities.Add(new ActivityWrapper(input, path));

            #region Populate Base and Declaration List
            lock (this)
            {
                string fullName = input.GetType().FullName;
                if (DeclarationTypes.Contains(fullName) == false)
                {
                    lock (this)
                    {
                        fullName = input.GetType().FullName;

                        if (DeclarationTypes.Contains(fullName) == false)
                        {
                            DeclarationTypes.Add(fullName);
                        }
                    }
                }
            }

            lock (this)
            {
                string baseTypeName = input.GetType().BaseType.FullName;
                if (BaseTypes.Contains(baseTypeName) == false)
                {
                    lock (this)
                    {
                        baseTypeName = input.GetType().BaseType.FullName;

                        if (BaseTypes.Contains(baseTypeName) == false)
                        {
                            BaseTypes.Add(baseTypeName);
                        }
                    }
                }
            }
            #endregion
        }

        public override string ToString()
        {
            return String.Format("{0} (Activities: {1}, Unresolved: {2}, Errors: {3})", WorkflowType.Name, ActivityTree.Count, UnresolvedActivities.Count, Errors.Count);
        }
    }
}
