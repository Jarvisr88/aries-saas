namespace DevExpress.Xpf.Data.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Data;
    using System;
    using System.Collections.Generic;

    public class SummaryCache
    {
        private readonly Dictionary<SummaryDefinition, object> actualValues = new Dictionary<SummaryDefinition, object>();
        private readonly Dictionary<SummaryDefinition, object> staleValues = new Dictionary<SummaryDefinition, object>();
        private readonly Action startCalculation;
        private readonly Func<string> getSummaryInProgressText;

        public SummaryCache(Action startCalculation, Func<string> getSummaryInProgressText)
        {
            this.startCalculation = startCalculation;
            this.getSummaryInProgressText = getSummaryInProgressText;
        }

        public void Add(IEnumerable<KeyValuePair<SummaryDefinition, object>> values)
        {
            foreach (KeyValuePair<SummaryDefinition, object> pair in values)
            {
                this.actualValues[pair.Key] = pair.Value;
                this.staleValues.Remove(pair.Key);
            }
        }

        public void Clear()
        {
            foreach (KeyValuePair<SummaryDefinition, object> pair in this.actualValues)
            {
                this.staleValues[pair.Key] = pair.Value;
            }
            this.actualValues.Clear();
        }

        public object GetCount() => 
            this.GetValueOrStartCalculation(SummaryDefinition.Count);

        public IEnumerable<SummaryDefinition> GetSummaries() => 
            this.actualValues.Keys;

        public object GetValueOrStartCalculation(SummaryDefinition summary)
        {
            if (this.actualValues.ContainsKey(summary))
            {
                return this.actualValues[summary];
            }
            this.startCalculation();
            return this.staleValues.GetValueOrDefault<SummaryDefinition, object>(summary, this.getSummaryInProgressText());
        }

        public void Remove(SummaryDefinition summary)
        {
            this.actualValues.Remove(summary);
            this.staleValues.Remove(summary);
        }

        public bool IsCountCalculated =>
            this.actualValues.ContainsKey(SummaryDefinition.Count);
    }
}

