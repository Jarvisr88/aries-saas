namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class DataMergedColumnSortInfo : DataColumnSortInfo
    {
        private DataColumnSortInfo[] infos;
        public static int InfosLengthThreshold;

        static DataMergedColumnSortInfo();
        public DataMergedColumnSortInfo(params DataColumnSortInfo[] infos);
        protected internal override bool ContainsColumn(string columnName);
        protected internal override bool ContainsColumnInfo(DataColumnInfo info);
        public int GetColumnIndex(int column);
        public override bool IsEquals(DataColumnSortInfo info);

        public DataColumnSortInfo[] Infos { get; }

        protected internal override string ColumnGroupData { get; }
    }
}

