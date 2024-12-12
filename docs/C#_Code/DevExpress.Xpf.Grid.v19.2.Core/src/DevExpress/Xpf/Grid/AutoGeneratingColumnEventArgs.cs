namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;

    public class AutoGeneratingColumnEventArgs : CancelRoutedEventArgs
    {
        public AutoGeneratingColumnEventArgs(ColumnBase column)
        {
            this.Column = column;
        }

        public ColumnBase Column { get; private set; }
    }
}

