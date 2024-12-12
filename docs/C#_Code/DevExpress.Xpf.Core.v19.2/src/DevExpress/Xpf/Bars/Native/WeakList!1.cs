namespace DevExpress.Xpf.Bars.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class WeakList : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        private readonly List<WeakReference> references;
        private T[] lockedCollection;
        private bool needCleanup;

        public WeakList();
        public virtual void Add(T item);
        public void CleanResolvedReferences();
        protected internal virtual void CleanupOnRequest();
        public virtual void Clear();
        public bool Contains(T item);
        public void CopyTo(T[] array, int arrayIndex);
        public IEnumerator<T> GetEnumerator();
        private T[] GetItemsCollection();
        public int IndexOf(T item);
        public virtual void Insert(int index, T item);
        public void LockReferences();
        public virtual bool Remove(T item);
        public virtual void RemoveAt(int index);
        protected virtual bool RemoveAtImpl(int index);
        protected internal virtual void RequestCleanup();
        IEnumerator IEnumerable.GetEnumerator();
        public void UnlockReferences();

        public T this[int index] { get; set; }

        public int Count { get; }

        public bool IsReadOnly { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WeakList<T>.<>c <>9;
            public static Func<WeakReference, object> <>9__23_1;
            public static Func<WeakReference, T> <>9__23_0;
            public static Func<T, bool> <>9__23_2;

            static <>c();
            internal T <GetItemsCollection>b__23_0(WeakReference wr);
            internal object <GetItemsCollection>b__23_1(WeakReference x);
            internal bool <GetItemsCollection>b__23_2(T itm);
        }
    }
}

