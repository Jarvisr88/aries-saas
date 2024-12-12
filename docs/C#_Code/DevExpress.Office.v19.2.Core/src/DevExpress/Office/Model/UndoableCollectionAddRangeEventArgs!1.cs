namespace DevExpress.Office.Model
{
    using System;
    using System.Collections.Generic;

    public class UndoableCollectionAddRangeEventArgs<T> : EventArgs
    {
        private readonly IEnumerable<T> collection;

        public UndoableCollectionAddRangeEventArgs(IEnumerable<T> collection)
        {
            this.collection = collection;
        }

        public IEnumerable<T> Collection =>
            this.collection;
    }
}

