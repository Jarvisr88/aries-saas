namespace DevExpress.Data.TreeList
{
    using DevExpress.Data;
    using DevExpress.Data.Helpers;
    using DevExpress.XtraGrid;
    using System;
    using System.Runtime.CompilerServices;

    public class TreeListDataColumnSortInfo : DataColumnSortInfo
    {
        public TreeListDataColumnSortInfo(DataColumnInfo columnInfo, ColumnSortOrder sortOrder, ColumnSortMode sortMode);

        public ColumnSortMode SortMode { get; set; }

        internal bool UpdateCache { get; set; }
    }
}

