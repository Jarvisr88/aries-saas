namespace DevExpress.Office.Model
{
    using System;

    public class UndoableCollectionRemoveAtEventArgs : EventArgs
    {
        private readonly int index;

        public UndoableCollectionRemoveAtEventArgs(int index)
        {
            this.index = index;
        }

        public int Index =>
            this.index;
    }
}

