namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class FontSizeSortedReadonlyList : IList<int>, ICollection<int>, IEnumerable<int>, IEnumerable
    {
        private readonly int count;

        public FontSizeSortedReadonlyList(int count)
        {
            Guard.ArgumentNonNegative(count, "count");
            this.count = count;
        }

        public void Add(int item)
        {
        }

        public void Clear()
        {
        }

        public bool Contains(int item) => 
            (item >= 0) && (item < this.count);

        public void CopyTo(int[] array, int arrayIndex)
        {
        }

        public IEnumerator<int> GetEnumerator() => 
            null;

        public int IndexOf(int item) => 
            item;

        public void Insert(int index, int item)
        {
        }

        public bool Remove(int item) => 
            false;

        public void RemoveAt(int index)
        {
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            null;

        public int this[int index]
        {
            get => 
                index;
            set
            {
            }
        }

        public int Count =>
            this.count;

        public bool IsReadOnly =>
            true;
    }
}

