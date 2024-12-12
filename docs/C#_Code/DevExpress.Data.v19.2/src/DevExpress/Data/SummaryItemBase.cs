namespace DevExpress.Data
{
    using System;

    public class SummaryItemBase
    {
        private DataColumnInfo columnInfo;
        private object tag;

        public SummaryItemBase();
        public SummaryItemBase(DataColumnInfo columnInfo);
        public SummaryItemBase(DataColumnInfo columnInfo, object tag);
        protected virtual void OnSummaryChanged();

        public DataColumnInfo ColumnInfo { get; set; }

        public object Key { get; }

        public object Tag { get; set; }
    }
}

