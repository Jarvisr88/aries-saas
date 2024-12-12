namespace DevExpress.Office.Model
{
    using System;

    public class UndoableCollectionAddEventArgs<T> : EventArgs
    {
        private readonly T item;

        public UndoableCollectionAddEventArgs(T item)
        {
            this.item = item;
        }

        public T Item =>
            this.item;
    }
}

