namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class CanSelectRowEventArgs : EventArgs
    {
        private int rowHandle;
        private DataViewBase view;
        private object row;

        public CanSelectRowEventArgs(DataViewBase view, int rowHandle, bool canSelectRow = true)
        {
            this.rowHandle = rowHandle;
            this.view = view;
            this.CanSelectRow = canSelectRow;
        }

        public bool CanSelectRow { get; set; }

        public int RowHandle =>
            this.rowHandle;

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

