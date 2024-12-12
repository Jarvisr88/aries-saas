namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class CanUnselectCellEventArgs : EventArgs
    {
        private int rowHandle;
        private ColumnBase column;
        private DataViewBase view;
        private object row;

        public CanUnselectCellEventArgs(DataViewBase view, int rowHandle, ColumnBase column, bool canUnselectCell = true)
        {
            this.rowHandle = rowHandle;
            this.view = view;
            this.column = column;
            this.CanUnselectCell = canUnselectCell;
        }

        public bool CanUnselectCell { get; set; }

        public int RowHandle =>
            this.rowHandle;

        public ColumnBase Column =>
            this.column;

        public DataViewBase View =>
            this.view;

        public object Row
        {
            get
            {
                this.row ??= this.View.DataControl.GetRow(this.RowHandle);
                return this.row;
            }
        }
    }
}

