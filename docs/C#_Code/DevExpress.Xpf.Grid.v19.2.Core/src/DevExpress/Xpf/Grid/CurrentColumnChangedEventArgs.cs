namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CurrentColumnChangedEventArgs : RoutedEventArgs
    {
        public CurrentColumnChangedEventArgs(DataControlBase source, GridColumnBase oldColumn, GridColumnBase newColumn)
        {
            this.OldColumn = oldColumn;
            this.NewColumn = newColumn;
            this.Source = source;
        }

        public GridColumnBase OldColumn { get; private set; }

        public GridColumnBase NewColumn { get; private set; }

        public DataControlBase Source { get; private set; }
    }
}

