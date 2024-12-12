namespace DevExpress.Data
{
    using System;

    public class RowDeletingEventArgs : RowDeletedEventArgs
    {
        private bool cancel;

        public RowDeletingEventArgs(int rowHandle, int listIndex, object row);

        public bool Cancel { get; set; }
    }
}

