using System;
using System.Collections.Generic;
using System.Linq;
using System.Workflow.ComponentModel;

namespace WFE.Models
{
    public class ActivityWrapper
    {
        private int? _depth;

        public int Depth
        {
            get
            {
                if (_depth.HasValue == false)
                {
                    _depth = FindDepth();
                }
                return _depth.Value;
            }
        }

        private int FindDepth()
        {
            return Children.Count + Children.Sum(t => t.Depth);
        }

        public Activity Current { get; private set; }
        public string Path { get; set; }
        public string RootPath
        {
            get
            {
                return Path.Contains(".") ? Path.Substring(0, Path.LastIndexOf('.')) : Path;
            }
        }

        public bool IsRoot
        {
            get
            {
                return string.Equals(Path, RootPath);
            }
        }

        public string Name
        {
            get
            {
                if (Current == null)
                    throw new NullReferenceException("Activity has not been loaded");

                return Current.Name;
            }
        }

        public List<ActivityWrapper> Children { get; private set; }
        public Guid ActivityId
        {
            //get
            //{
            //    if (Current == null)
            //        throw new NullReferenceException("Activity has not been loaded");

            //    return Guid.NewGuid();
            //}

            get;
            set;
        }

        public ActivityWrapper(Activity current, string path)
        {
            this.Current = current;
            this.ActivityId = Guid.NewGuid();
            this.Path = path;
            this.Children = new List<ActivityWrapper>();
        }

        public void AddChildren(ActivityWrapper child)
        {
            lock (this)
            {
                if (Children.Contains(child) != false) return;

                lock (this)
                {
                    if (Children.Contains(child) == false)
                    {
                        // avoid duplicacy
                        Children.Add(child);
                    }
                }
            }
        }

        public override string ToString()
        {
            return String.Format("{0} (Root: {1}, Children: {2})", Name, IsRoot, Children.Count);
        }

        internal string[] GetDependentList()
        {
            return Children.Select(x => x.Name).ToArray();
        }
    }
}
