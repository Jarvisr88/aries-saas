namespace DevExpress.Xpf.Grid
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class GridSummaryData : INotifyPropertyChanged
    {
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        public GridSummaryData(SummaryItemBase item, object value, ColumnBase column)
        {
            this.Item = item;
            this.Value = value;
            this.Column = column;
        }

        public SummaryItemBase Item { get; private set; }

        public object Value { get; private set; }

        public ColumnBase Column { get; private set; }
    }
}

