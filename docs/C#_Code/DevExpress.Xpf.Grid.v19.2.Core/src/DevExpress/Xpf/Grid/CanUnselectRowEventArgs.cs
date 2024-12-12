namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class CanUnselectRowEventArgs : EventArgs
    {
        private int rowHandle;
        private DataViewBase view;
        private object row;

        public CanUnselectRowEventArgs(DataViewBase view, int rowHandle, bool canUnselectRow = true)
        {
            this.rowHandle = rowHandle;
            this.view = view;
            this.CanUnselectRow = canUnselectRow;
        }

        public bool CanUnselectRow { get; set; }

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

