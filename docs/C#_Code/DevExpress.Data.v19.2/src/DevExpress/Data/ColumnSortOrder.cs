namespace DevExpress.Data
{
    using DevExpress.Data.Design;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(ColumnSortOrderTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum ColumnSortOrder
    {
        public const ColumnSortOrder None = ColumnSortOrder.None;,
        public const ColumnSortOrder Ascending = ColumnSortOrder.Ascending;,
        public const ColumnSortOrder Descending = ColumnSortOrder.Descending;
    }
}

