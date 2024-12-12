namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class QueryConditionOperationsEventArgs
    {
        private readonly Lazy<CriteriaOperator> lazyFilter;

        internal QueryConditionOperationsEventArgs(Lazy<CriteriaOperator> lazyFilter, string fieldName)
        {
            this.AllowRemoveCondition = true;
            this.<FieldName>k__BackingField = fieldName;
            this.lazyFilter = lazyFilter;
        }

        public bool AllowRemoveCondition { get; set; }

        public CriteriaOperator Filter =>
            this.lazyFilter.Value;

        public string FieldName { get; }
    }
}

