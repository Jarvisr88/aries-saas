namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ShowingEditorEventArgsBase : EditorEventArgsBase
    {
        public ShowingEditorEventArgsBase(RoutedEvent routedEvent, DataViewBase view, int rowHandle, ColumnBase column) : base(routedEvent, view, rowHandle, column)
        {
        }

        public bool Cancel { get; set; }
    }
}

