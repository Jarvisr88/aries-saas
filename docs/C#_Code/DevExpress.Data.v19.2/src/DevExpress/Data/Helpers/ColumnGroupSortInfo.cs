namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class ColumnGroupSortInfo
    {
        private int columnHandle;
        private int groupIndex;
        private string fieldName;
        private ColumnSortOrder order;

        public ColumnGroupSortInfo();
        public ColumnGroupSortInfo(int columnHandle, int groupIndex, ColumnSortOrder order, string fieldName);
        public virtual bool IsEquals(DataColumnSortInfo si);

        public string FieldName { get; }

        public int ColumnHandle { get; }

        public ColumnSortOrder Order { get; }

        public int GroupIndex { get; }
    }
}

