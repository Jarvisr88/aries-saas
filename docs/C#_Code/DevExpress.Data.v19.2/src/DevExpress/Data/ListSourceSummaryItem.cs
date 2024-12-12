namespace DevExpress.Data
{
    using System;

    public class ListSourceSummaryItem
    {
        private SummaryItemType summaryType;
        private DataColumnInfo info;
        private SummaryItem item;

        public ListSourceSummaryItem();
        internal ListSourceSummaryItem(SummaryItem item);
        public ListSourceSummaryItem(DataColumnInfo info, SummaryItemType summaryType);

        internal SummaryItem Item { get; set; }

        public SummaryItemType SummaryType { get; set; }

        public DataColumnInfo Info { get; set; }
    }
}

