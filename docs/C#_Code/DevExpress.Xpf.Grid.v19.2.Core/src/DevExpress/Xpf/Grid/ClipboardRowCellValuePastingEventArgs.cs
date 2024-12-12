namespace DevExpress.Xpf.Grid
{
    using System;

    public class ClipboardRowCellValuePastingEventArgs : EventArgs
    {
        private bool cancel;
        private ColumnBase column;
        private object oldValue;
        private int rowHandle;
        private object value;
        private object originalValue;

        protected ClipboardRowCellValuePastingEventArgs()
        {
        }

        internal ClipboardRowCellValuePastingEventArgs(int rowHandle, ColumnBase column, object value, object originalValue)
        {
            this.oldValue = this.GetRowCellValueCore(rowHandle, column);
            this.rowHandle = rowHandle;
            this.column = column;
            this.value = value;
            this.originalValue = originalValue;
            this.cancel = Equals(this.OldValue, this.Value);
        }

        private object GetRowCellValueCore(int rowHandle, ColumnBase column) => 
            ((column == null) || ((column.View == null) || (column.View.DataControl == null))) ? null : column.View.DataControl.GetCellValue(rowHandle, column.FieldName);

        public bool Cancel
        {
            get => 
                this.cancel;
            set => 
                this.cancel = value;
        }

        public ColumnBase Column =>
            this.column;

        public object OldValue =>
            this.oldValue;

        public int RowHandle =>
            this.rowHandle;

        public object Value
        {
            get => 
                this.value;
            set => 
                this.value = value;
        }

        public object OriginalValue =>
            this.originalValue;
    }
}

