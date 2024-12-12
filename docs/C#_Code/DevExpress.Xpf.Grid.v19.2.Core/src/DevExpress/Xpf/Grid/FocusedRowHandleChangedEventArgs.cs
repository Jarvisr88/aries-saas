namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class FocusedRowHandleChangedEventArgs : RoutedEventArgs
    {
        public FocusedRowHandleChangedEventArgs(DevExpress.Xpf.Grid.RowData rowData)
        {
            this.RowData = rowData;
        }

        public DevExpress.Xpf.Grid.RowData RowData { get; private set; }
    }
}

