namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using System;
    using System.Runtime.InteropServices;

    internal static class ColumnValuesArgumentsHelper
    {
        internal static ColumnValuesArguments GetColumnValuesArgs(CriteriaOperator columnFilter, bool includeFilteredOut = true, bool roundDateTime = true, bool implyNullLikeEmptyStringWhenFiltering = true) => 
            new ColumnValuesArguments { 
                IgnoreAppliedFilter = includeFilteredOut,
                ImplyNullLikeEmptyStringWhenFiltering = implyNullLikeEmptyStringWhenFiltering,
                MaxCount = -1,
                RoundDateTime = roundDateTime,
                Filter = columnFilter
            };
    }
}

