namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CustomFilterDisplayTextEventArgs : RoutedEventArgs
    {
        public CustomFilterDisplayTextEventArgs(DataViewBase source, object value)
        {
            this.Value = value;
            this.Source = source;
        }

        public object Value { get; set; }

        public DataViewBase Source { get; private set; }
    }
}

