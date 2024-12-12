namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class QueryGroupOperationsEventArgs
    {
        private readonly Lazy<CriteriaOperator> lazyFilter;

        internal QueryGroupOperationsEventArgs(Lazy<CriteriaOperator> filter, bool allowAddCustomExpression)
        {
            this.lazyFilter = filter;
            this.AllowAddCondition = true;
            this.AllowAddGroup = true;
            this.AllowAddCustomExpression = allowAddCustomExpression;
        }

        public bool AllowAddCondition { get; set; }

        public bool AllowAddGroup { get; set; }

        public bool AllowAddCustomExpression { get; set; }

        public CriteriaOperator Filter =>
            this.lazyFilter.Value;
    }
}

