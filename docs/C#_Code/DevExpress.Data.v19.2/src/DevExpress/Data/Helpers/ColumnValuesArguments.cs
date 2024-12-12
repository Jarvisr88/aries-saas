namespace DevExpress.Data.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class ColumnValuesArguments
    {
        public bool RoundDateTime { get; set; }

        public int MaxCount { get; set; }

        public CriteriaOperator Filter { get; set; }

        public bool IgnoreAppliedFilter { get; set; }

        public bool ImplyNullLikeEmptyStringWhenFiltering { get; set; }
    }
}

