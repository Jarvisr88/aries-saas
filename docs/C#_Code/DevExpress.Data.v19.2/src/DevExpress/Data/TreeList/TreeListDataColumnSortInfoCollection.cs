namespace DevExpress.Data.TreeList
{
    using DevExpress.Data;
    using DevExpress.Data.Helpers;
    using DevExpress.XtraGrid;
    using System;
    using System.ComponentModel;

    public class TreeListDataColumnSortInfoCollection : TreeListNotificationCollectionBase<TreeListDataColumnSortInfo>
    {
        public TreeListDataColumnSortInfoCollection();
        public TreeListDataColumnSortInfoCollection(CollectionChangeEventHandler collectionChanged);
        public DataColumnSortInfo Add(DataColumnInfo columnInfo, ColumnSortOrder sortOrder, ColumnSortMode sortMode);
    }
}

