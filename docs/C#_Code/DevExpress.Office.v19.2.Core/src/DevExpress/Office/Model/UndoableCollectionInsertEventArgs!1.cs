namespace DevExpress.Office.Model
{
    using System;

    public class UndoableCollectionInsertEventArgs<T> : UndoableCollectionRemoveAtEventArgs
    {
        private readonly T item;

        public UndoableCollectionInsertEventArgs(int index, T item) : base(index)
        {
            this.item = item;
        }

        public T Item =>
            this.item;
    }
}

