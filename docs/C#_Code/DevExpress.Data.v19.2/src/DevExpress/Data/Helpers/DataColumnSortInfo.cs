namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Utils;
    using DevExpress.XtraGrid;
    using System;

    public class DataColumnSortInfo
    {
        private DataColumnInfo columnInfo;
        private ColumnSortOrder sortOrder;
        private DefaultBoolean requireOnCellCompare;
        private ColumnGroupInterval groupInterval;
        private bool runningSummary;
        private bool crossGroupRunningSummary;
        private DataColumnInfo auxColumnInfo;

        public DataColumnSortInfo(DataColumnInfo columnInfo);
        public DataColumnSortInfo(DataColumnInfo columnInfo, ColumnSortOrder sortOrder);
        public DataColumnSortInfo(DataColumnInfo columnInfo, ColumnSortOrder sortOrder, ColumnGroupInterval groupInterval);
        protected internal virtual bool ContainsColumn(string columnName);
        protected internal virtual bool ContainsColumnInfo(DataColumnInfo info);
        public virtual bool IsEquals(DataColumnSortInfo info);

        protected internal virtual string ColumnGroupData { get; }

        public ColumnGroupInterval GroupInterval { get; set; }

        public DataColumnInfo ColumnInfo { get; }

        public DataColumnInfo AuxColumnInfo { get; set; }

        public ColumnSortOrder SortOrder { get; }

        public DefaultBoolean RequireOnCellCompare { get; set; }

        public bool RunningSummary { get; set; }

        public bool CrossGroupRunningSummary { get; set; }
    }
}

