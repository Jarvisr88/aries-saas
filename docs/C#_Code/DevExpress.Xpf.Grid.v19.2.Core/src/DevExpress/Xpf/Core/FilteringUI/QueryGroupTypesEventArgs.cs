namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class QueryGroupTypesEventArgs
    {
        private readonly Lazy<CriteriaOperator> lazyFilter;

        internal QueryGroupTypesEventArgs(AllowedGroupFilters allowedGroupFilters, Lazy<CriteriaOperator> filter)
        {
            this.lazyFilter = filter;
            if (allowedGroupFilters.HasFlag(AllowedGroupFilters.And))
            {
                this.AllowAnd = true;
            }
            if (allowedGroupFilters.HasFlag(AllowedGroupFilters.NotAnd))
            {
                this.AllowNotAnd = true;
            }
            if (allowedGroupFilters.HasFlag(AllowedGroupFilters.Or))
            {
                this.AllowOr = true;
            }
            if (allowedGroupFilters.HasFlag(AllowedGroupFilters.NotOr))
            {
                this.AllowNotOr = true;
            }
        }

        internal AllowedGroupFilters GetActualAllowedGroupFilters()
        {
            AllowedGroupFilters none = AllowedGroupFilters.None;
            if (this.AllowAnd)
            {
                none |= AllowedGroupFilters.And;
            }
            if (this.AllowNotAnd)
            {
                none |= AllowedGroupFilters.NotAnd;
            }
            if (this.AllowOr)
            {
                none |= AllowedGroupFilters.Or;
            }
            if (this.AllowNotOr)
            {
                none |= AllowedGroupFilters.NotOr;
            }
            return none;
        }

        public bool AllowAnd { get; set; }

        public bool AllowNotAnd { get; set; }

        public bool AllowOr { get; set; }

        public bool AllowNotOr { get; set; }

        public CriteriaOperator Filter =>
            this.lazyFilter.Value;
    }
}

