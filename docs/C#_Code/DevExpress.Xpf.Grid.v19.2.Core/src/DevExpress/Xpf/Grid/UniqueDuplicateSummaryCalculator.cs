namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Collections.Generic;

    public class UniqueDuplicateSummaryCalculator
    {
        private readonly UniqueDuplicateRule rule;
        private HashSet<object> uniqueValues;
        private HashSet<object> knownValues;
        private HashSet<object> duplicateValues;

        public UniqueDuplicateSummaryCalculator(UniqueDuplicateRule rule)
        {
            this.rule = rule;
            this.Reset();
        }

        private void CalcDuplicate(object currentValue)
        {
            if (!this.Register(currentValue))
            {
                this.duplicateValues.Add(currentValue);
            }
        }

        public void Calculate(object currentValue)
        {
            if (this.rule == UniqueDuplicateRule.Duplicate)
            {
                this.CalcDuplicate(currentValue);
            }
            else if (this.rule == UniqueDuplicateRule.Unique)
            {
                this.CalculateUnique(currentValue);
            }
        }

        private void CalculateUnique(object currentValue)
        {
            if (this.Register(currentValue))
            {
                this.uniqueValues.Add(currentValue);
            }
            else
            {
                this.uniqueValues.Remove(currentValue);
            }
        }

        public HashSet<object> Finish()
        {
            HashSet<object> duplicateValues = null;
            if (this.rule == UniqueDuplicateRule.Duplicate)
            {
                duplicateValues = this.duplicateValues;
            }
            else if (this.rule == UniqueDuplicateRule.Unique)
            {
                duplicateValues = this.uniqueValues;
            }
            this.Reset();
            return duplicateValues;
        }

        private bool Register(object currentValue) => 
            this.knownValues.Add(currentValue);

        private void Reset()
        {
            this.uniqueValues = new HashSet<object>();
            this.knownValues = new HashSet<object>();
            this.duplicateValues = new HashSet<object>();
        }
    }
}

