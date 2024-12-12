namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct SummaryFilterInfo
    {
        public readonly SummaryItemType SummaryType;
        public readonly CriteriaOperator Filter;
        public SummaryFilterInfo(SummaryItemType summaryType, CriteriaOperator filter)
        {
            this.SummaryType = summaryType;
            this.Filter = filter;
        }
    }
}

