namespace DevExpress.SpreadsheetSource.Implementation
{
    using DevExpress.SpreadsheetSource;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class CellCollection : ICellCollection, IEnumerable<ICell>, IEnumerable, ICollection
    {
        private readonly List<ICell> innerList = new List<ICell>();

        public void Add(ICell item)
        {
            this.innerList.Add(item);
        }

        public void Clear()
        {
            this.innerList.Clear();
        }

        public void CopyTo(Array array, int index)
        {
            Array.Copy(this.innerList.ToArray(), 0, array, index, this.innerList.Count);
        }

        public IEnumerator<ICell> GetEnumerator() => 
            this.innerList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            this.innerList.GetEnumerator();

        public ICell this[int index] =>
            this.innerList[index];

        public int Count =>
            this.innerList.Count;

        public bool IsSynchronized =>
            ((ICollection) this.innerList).IsSynchronized;

        public object SyncRoot =>
            ((ICollection) this.innerList).SyncRoot;
    }
}

