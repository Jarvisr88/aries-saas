namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ColumnHeaderClickEventArgs : RoutedEventArgs
    {
        public ColumnHeaderClickEventArgs(ColumnBase column, bool isShift, bool isCtrl)
        {
            this.Column = column;
            this.IsShift = isShift;
            this.IsCtrl = isCtrl;
        }

        public ColumnBase Column { get; private set; }

        public bool IsShift { get; set; }

        public bool IsCtrl { get; set; }

        public bool AllowSorting { get; set; }
    }
}

