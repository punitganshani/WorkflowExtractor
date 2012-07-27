using System;
using System.Collections.Generic;
using WFE.Base;
using WFE.Models;
using WFE.Models.Output;

namespace WFE.Extractors
{
    internal sealed class WorkflowConverter : Events, IDisposable
    {
        private readonly WorkflowExtractor _extractor;
        private readonly RulesExtractor _rulesExtractor;
        private List<GeneratorTypes> _generatorsTypes;

        internal WorkflowConverter(WorkflowExtractor extractor, RulesExtractor rulesExtractor)
        {
            if (extractor == null)
                throw new ArgumentNullException("extractor");

            if (rulesExtractor == null)
                throw new ArgumentNullException("rulesExtractor");

            _generatorsTypes = new List<GeneratorTypes>();

            if (extractor.Activities == null)
                extractor.GetActivityTree();

            _extractor = extractor;
            _rulesExtractor = rulesExtractor;
        }

        #region IDisposable Members

        public void Dispose()
        {
            _generatorsTypes = null;
        }

        #endregion

        internal void AddOutput<T>(Settings output) where T : WorkflowOutputGenerator
        {
            if (output == null)
                throw new ArgumentNullException("output");

            _generatorsTypes.Add(new GeneratorTypes(typeof (T), output));
        }

        internal bool Generate()
        {
            bool success = false;

            for (int i = 0; i < _generatorsTypes.Count; i++)
            {
                if (_generatorsTypes[i] != null)
                {
                    using (var instance = (WorkflowOutputGenerator) Activator.CreateInstance(_generatorsTypes[i].Type,
                                                                                             _extractor.Activities,
                                                                                             _rulesExtractor,
                                                                                             _generatorsTypes[i].Output)
                        )
                    {
                        success &= instance.Generate();
                    }
                }
            }

            SendMessage("Workflow conversion completed", EventType.Complete);

            OnComplete();

            return success;
        }

        #region Nested type: GeneratorTypes

        private class GeneratorTypes
        {
            public GeneratorTypes(Type type, Settings output)
            {
                Output = output;
                Type = type;
            }

            public Type Type { get; private set; }

            public Settings Output { get; private set; }
        }

        #endregion
    }
}