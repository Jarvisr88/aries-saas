namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ObjectContainer<T> : IObjectContainer, IEnumerable<T>, IEnumerable where T: IDisposable
    {
        private readonly Dictionary<object, T> items;

        public ObjectContainer();
        public virtual void Clear();
        protected virtual bool EntryReferencesEquals(IDisposable entry1, IDisposable entry2);
        public IEnumerator<T> GetEnumerator();
        protected T GetObject(object key, T obj);
        IEnumerator IEnumerable.GetEnumerator();

        public int Count { get; }

        protected IDictionary<object, T> Items { get; }

        protected virtual IEqualityComparer<object> EqualityComparer { get; }
    }
}

