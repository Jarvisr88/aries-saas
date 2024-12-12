namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class ListAdapterBase<TSource> : IEnumerable where TSource: IEnumerable
    {
        private readonly TSource source;
        private IEnumerator lazyIndexIterator;

        protected ListAdapterBase(TSource source);
        public int Add(object value);
        public void Clear();
        public bool Contains(object value);
        protected int EnsureListCount(ref List<object> list);
        protected object EnsureListItem(ref List<object> list, int index, int count);
        public override int GetHashCode();
        public void Insert(int index, object value);
        protected static bool IsEmpty(IList<object> list, IEnumerable source);
        public void Remove(object value);
        public void RemoveAt(int index);
        IEnumerator IEnumerable.GetEnumerator();

        public TSource Source { get; }

        public bool IsFixedSize { get; }

        public bool IsReadOnly { get; }
    }
}

