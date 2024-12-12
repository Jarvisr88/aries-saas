namespace DevExpress.Xpf.Grid
{
    using DevExpress.XtraExport.Helpers;
    using System;

    public class ClipboardRowPastingEventArgs : EventArgs
    {
        private bool cancel;
        private string errorText;
        private ColumnBase[] _columns;
        private bool isValid;
        private IClipboardRow row;
        private readonly int _rowHandle;
        private readonly object[] _originalCellValues;
        private readonly int dataRowCountCore;
        private readonly int rowCountCore;

        protected ClipboardRowPastingEventArgs()
        {
            this.isValid = true;
        }

        internal ClipboardRowPastingEventArgs(IClipboardRow row, ColumnBase[] columns, string errorText, bool isValid, int rowHandle, object[] originalCellValues, int dataRowCountCore, int rowCountCore)
        {
            this.isValid = true;
            this.row = row;
            this._columns = columns;
            this.isValid = isValid;
            this.errorText = errorText;
            this.Cancel = !this.IsValid;
            this._rowHandle = rowHandle;
            this._originalCellValues = originalCellValues;
            this.dataRowCountCore = dataRowCountCore;
            this.rowCountCore = rowCountCore;
        }

        public bool Cancel
        {
            get => 
                this.cancel;
            set => 
                this.cancel = value;
        }

        public object[] CellValues =>
            this.row.Cells;

        public int DataRowCount =>
            this.dataRowCountCore;

        public int RowCount =>
            this.rowCountCore;

        public ColumnBase[] Columns =>
            this._columns;

        public string ErrorText =>
            this.errorText;

        public bool IsValid =>
            this.isValid;

        public int RowHandle =>
            this._rowHandle;

        public object[] OriginalCellValues =>
            this._originalCellValues;
    }
}

