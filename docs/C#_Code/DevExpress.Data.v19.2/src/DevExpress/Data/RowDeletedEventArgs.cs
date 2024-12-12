namespace DevExpress.Data
{
    using System;

    public class RowDeletedEventArgs : EventArgs
    {
        private int rowHandle;
        private int listSourceIndex;
        private object row;

        public RowDeletedEventArgs(int rowHandle, int listIndex, object row);

        public int RowHandle { get; }

        public int ListSourceIndex { get; }

        public object Row { get; }
    }
}

