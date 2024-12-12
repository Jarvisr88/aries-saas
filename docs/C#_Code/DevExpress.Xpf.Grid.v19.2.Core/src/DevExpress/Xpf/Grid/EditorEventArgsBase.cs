namespace DevExpress.Xpf.Grid
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class EditorEventArgsBase : RoutedEventArgs
    {
        protected DataViewBase view;

        protected EditorEventArgsBase(RoutedEvent routedEvent, DataViewBase view, int rowHandle, ColumnBase column) : base(routedEvent, view)
        {
            this.RowHandle = rowHandle;
            this.Column = column;
            this.view = view;
        }

        private DataControlBase DataControl =>
            this.view.DataControl;

        [Description("Gets or sets the row handle, for which an event has been raised.")]
        public int RowHandle { get; private set; }

        [Description("Gets or sets a grid column, for which an event has been raised.")]
        public ColumnBase Column { get; private set; }

        [Description("Gets the edit value stored in the editor, for which an event has been raised.")]
        public object Value =>
            this.DataControl.GetCellValueCore(this.RowHandle, this.Column);

        public DataViewBase Source =>
            this.view;
    }
}

