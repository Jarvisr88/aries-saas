namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class SummarySortInfo
    {
        private DevExpress.Data.SummaryItem summaryItem;
        private ColumnSortOrder sortOrder;
        private int groupLevel;

        public SummarySortInfo(DevExpress.Data.SummaryItem summaryItem);
        public SummarySortInfo(DevExpress.Data.SummaryItem summaryItem, int groupLevel, ColumnSortOrder sortOrder);

        public DevExpress.Data.SummaryItem SummaryItem { get; }

        public ColumnSortOrder SortOrder { get; }

        public int GroupLevel { get; }
    }
}

