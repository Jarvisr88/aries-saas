namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class GenericListWrapper<T> : IList, ICollection, IEnumerable
    {
        private readonly IList<T> List;
        private object syncRoot;

        public GenericListWrapper(IList<T> list);
        public int Add(object value);
        public void Clear();
        public bool Contains(object value);
        public void CopyTo(Array array, int index);
        public IEnumerator GetEnumerator();
        public int IndexOf(object value);
        public void Insert(int index, object value);
        public void Remove(object value);
        public void RemoveAt(int index);

        public bool IsReadOnly { get; }

        public bool IsFixedSize { get; }

        public int Count { get; }

        public object SyncRoot { get; }

        public bool IsSynchronized { get; }

        public object this[int index] { get; set; }
    }
}

