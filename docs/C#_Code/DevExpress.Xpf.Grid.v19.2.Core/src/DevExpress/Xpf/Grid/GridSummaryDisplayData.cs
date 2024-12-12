namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class GridSummaryDisplayData : GridSummaryData
    {
        public GridSummaryDisplayData(GridSummaryData data, string displayText) : this(data.Item, data.Value, data.Column, displayText)
        {
        }

        public GridSummaryDisplayData(SummaryItemBase item, object value, ColumnBase column, string displayText) : base(item, value, column)
        {
            this.DisplayText = displayText;
        }

        public string DisplayText { get; private set; }
    }
}

