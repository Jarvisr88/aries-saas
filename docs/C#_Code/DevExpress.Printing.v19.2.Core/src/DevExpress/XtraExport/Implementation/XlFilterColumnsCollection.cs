namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class XlFilterColumnsCollection : IXlFilterColumns, IEnumerable<XlFilterColumn>, IEnumerable, ICollection
    {
        private readonly List<XlFilterColumn> innerList = new List<XlFilterColumn>();

        public void Add(XlFilterColumn column)
        {
            Guard.ArgumentNotNull(column, "column");
            int index = this.innerList.BinarySearch(column, XlFilterColumnComparer.Instance);
            if (index >= 0)
            {
                throw new ArgumentException("Duplicated column id");
            }
            index = ~index;
            this.innerList.Insert(index, column);
        }

        public void Clear()
        {
            this.innerList.Clear();
        }

        public bool Contains(XlFilterColumn column) => 
            (column != null) ? (this.innerList.BinarySearch(column, XlFilterColumnComparer.Instance) >= 0) : false;

        public void CopyTo(Array array, int index)
        {
            Array.Copy(this.innerList.ToArray(), 0, array, index, this.innerList.Count);
        }

        public XlFilterColumn FindById(int columnId)
        {
            int num = Algorithms.BinarySearch<XlFilterColumn>(this.innerList, new XlFilterColumnIdComparable(columnId));
            return ((num >= 0) ? this.innerList[num] : null);
        }

        public IEnumerator<XlFilterColumn> GetEnumerator() => 
            this.innerList.GetEnumerator();

        public int IndexOf(XlFilterColumn column)
        {
            if (column == null)
            {
                return -1;
            }
            int num = this.innerList.BinarySearch(column, XlFilterColumnComparer.Instance);
            return ((num >= 0) ? num : -1);
        }

        public void Remove(XlFilterColumn column)
        {
            if (column != null)
            {
                int index = this.innerList.BinarySearch(column, XlFilterColumnComparer.Instance);
                if (index >= 0)
                {
                    this.innerList.RemoveAt(index);
                }
            }
        }

        public void RemoveAt(int index)
        {
            this.innerList.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.innerList.GetEnumerator();

        public XlFilterColumn this[int index] =>
            this.innerList[index];

        public int Count =>
            this.innerList.Count;

        public bool IsSynchronized =>
            ((ICollection) this.innerList).IsSynchronized;

        public object SyncRoot =>
            ((ICollection) this.innerList).SyncRoot;
    }
}

