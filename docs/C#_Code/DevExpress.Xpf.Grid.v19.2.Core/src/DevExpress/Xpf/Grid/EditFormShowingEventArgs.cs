namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class EditFormShowingEventArgs : EventArgs
    {
        private readonly int rowHandleCore;

        public EditFormShowingEventArgs(int rowHandle)
        {
            this.rowHandleCore = rowHandle;
            this.Allow = true;
        }

        public int RowHandle =>
            this.rowHandleCore;

        public bool Allow { get; set; }
    }
}

