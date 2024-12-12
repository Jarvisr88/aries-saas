namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class FilterEditorEventArgs : RoutedEventArgs
    {
        public FilterEditorEventArgs(DataViewBase source, DevExpress.Xpf.Editors.Filtering.FilterControl control)
        {
            this.FilterControl = control;
            this.Source = source;
        }

        public DevExpress.Xpf.Editors.Filtering.FilterControl FilterControl { get; private set; }

        public DataViewBase Source { get; private set; }
    }
}

