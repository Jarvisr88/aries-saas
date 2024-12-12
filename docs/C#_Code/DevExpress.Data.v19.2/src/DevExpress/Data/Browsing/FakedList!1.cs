namespace DevExpress.Data.Browsing
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    internal class FakedList<T> : IList, ICollection, IEnumerable
    {
        private IList<T> items;
        private object syncRoot;

        public FakedList(IEnumerable<T> enumerable);
        public FakedList(IList<T> items);
        void ICollection.CopyTo(Array array, int index);
        IEnumerator IEnumerable.GetEnumerator();
        int IList.Add(object value);
        void IList.Clear();
        bool IList.Contains(object value);
        int IList.IndexOf(object value);
        void IList.Insert(int index, object value);
        void IList.Remove(object value);
        void IList.RemoveAt(int index);

        public T this[int index] { get; set; }

        bool IList.IsFixedSize { get; }

        bool IList.IsReadOnly { get; }

        object IList.this[int index] { get; set; }

        int ICollection.Count { get; }

        bool ICollection.IsSynchronized { get; }

        object ICollection.SyncRoot { get; }
    }
}

