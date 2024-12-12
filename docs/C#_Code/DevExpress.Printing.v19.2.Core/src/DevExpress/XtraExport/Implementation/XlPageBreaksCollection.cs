namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class XlPageBreaksCollection : IXlPageBreaks, IEnumerable<int>, IEnumerable, ICollection
    {
        private readonly List<int> innerList = new List<int>();
        private readonly int maxPosition;

        public XlPageBreaksCollection(int maxPosition)
        {
            this.maxPosition = maxPosition;
        }

        public void Add(int position)
        {
            if ((position <= 0) || (position > this.maxPosition))
            {
                throw new ArgumentOutOfRangeException($"Position out of range 0...{this.maxPosition}");
            }
            int index = this.innerList.BinarySearch(position);
            if (index < 0)
            {
                index = ~index;
                this.innerList.Insert(index, position);
            }
        }

        public void Clear()
        {
            this.innerList.Clear();
        }

        public bool Contains(int position) => 
            this.innerList.BinarySearch(position) >= 0;

        public IEnumerator<int> GetEnumerator() => 
            this.innerList.GetEnumerator();

        public int IndexOf(int position)
        {
            int num = this.innerList.BinarySearch(position);
            return ((num >= 0) ? num : -1);
        }

        public void Remove(int position)
        {
            int index = this.innerList.BinarySearch(position);
            if (index >= 0)
            {
                this.innerList.RemoveAt(index);
            }
        }

        public void RemoveAt(int index)
        {
            this.innerList.RemoveAt(index);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            Array.Copy(this.innerList.ToArray(), 0, array, index, this.innerList.Count);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.innerList.GetEnumerator();

        public int this[int index] =>
            this.innerList[index];

        public int Count =>
            this.innerList.Count;

        bool ICollection.IsSynchronized =>
            ((ICollection) this.innerList).IsSynchronized;

        object ICollection.SyncRoot =>
            ((ICollection) this.innerList).SyncRoot;
    }
}

