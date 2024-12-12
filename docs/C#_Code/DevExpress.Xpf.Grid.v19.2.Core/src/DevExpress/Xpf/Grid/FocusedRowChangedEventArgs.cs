namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class FocusedRowChangedEventArgs : RoutedEventArgs
    {
        public FocusedRowChangedEventArgs(DataViewBase source, object oldRow, object newRow)
        {
            this.OldRow = oldRow;
            this.NewRow = newRow;
            this.Source = source;
        }

        public object OldRow { get; private set; }

        public object NewRow { get; private set; }

        public DataViewBase Source { get; private set; }
    }
}

