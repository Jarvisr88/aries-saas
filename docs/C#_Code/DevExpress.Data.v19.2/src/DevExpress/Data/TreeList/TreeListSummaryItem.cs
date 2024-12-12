namespace DevExpress.Data.TreeList
{
    using DevExpress.Data;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class TreeListSummaryItem : SummaryItem
    {
        public TreeListSummaryItem(DataColumnInfo columnInfo, SummaryItemType summaryType, bool? ignoreNullValues = new bool?(), bool isRecursive = true);
        public TreeListSummaryItem(DataColumnInfo columnInfo, SummaryItemTypeEx summaryType, decimal argument, bool? ignoreNullValues = new bool?(), bool isRecursive = true);
        public bool ShouldIgnoreNullValues(bool defaultValue);

        public bool IsRecursive { get; set; }

        public bool IsTotal { get; set; }

        public bool? AllowIgnoreNullValues { get; set; }
    }
}

