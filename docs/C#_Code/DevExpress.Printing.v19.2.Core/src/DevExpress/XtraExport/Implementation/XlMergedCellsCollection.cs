namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class XlMergedCellsCollection : IXlMergedCells, IEnumerable<XlCellRange>, IEnumerable, ICollection
    {
        private readonly List<XlCellRange> innerList = new List<XlCellRange>();
        private readonly XlRangeOverlapChecker overlapChecker = new XlRangeOverlapChecker();

        public void Add(XlCellRange range)
        {
            if (!this.overlapChecker.IsNotOverlapped(range))
            {
                throw new ArgumentException("Cell range shouldn't overlap other ranges of merged cells.");
            }
            this.innerList.Add(range);
        }

        public void Add(XlCellRange range, bool checkOverlap)
        {
            if (checkOverlap && !this.overlapChecker.IsNotOverlapped(range))
            {
                throw new ArgumentException("Cell range shouldn't overlap other ranges of merged cells.");
            }
            this.innerList.Add(range);
        }

        public void Clear()
        {
            this.overlapChecker.Clear();
            this.innerList.Clear();
        }

        public bool Contains(XlCellRange range) => 
            this.innerList.Contains(range);

        public IEnumerator<XlCellRange> GetEnumerator() => 
            this.innerList.GetEnumerator();

        public int IndexOf(XlCellRange range) => 
            this.innerList.IndexOf(range);

        public void Remove(XlCellRange range)
        {
            this.overlapChecker.Remove(range);
            this.innerList.Remove(range);
        }

        public void RemoveAt(int index)
        {
            XlCellRange range = this.innerList[index];
            this.overlapChecker.Remove(range);
            this.innerList.RemoveAt(index);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            Array.Copy(this.innerList.ToArray(), 0, array, index, this.innerList.Count);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.innerList.GetEnumerator();

        public XlCellRange this[int index] =>
            this.innerList[index];

        public int Count =>
            this.innerList.Count;

        bool ICollection.IsSynchronized =>
            ((ICollection) this.innerList).IsSynchronized;

        object ICollection.SyncRoot =>
            ((ICollection) this.innerList).SyncRoot;
    }
}

