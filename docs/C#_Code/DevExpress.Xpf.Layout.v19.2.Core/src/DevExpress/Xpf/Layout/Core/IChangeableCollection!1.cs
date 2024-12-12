namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IChangeableCollection<T> : DevExpress.Xpf.Layout.Core.IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, ISupportVisitor<T>, ISupportNotification<T> where T: class
    {
        void Add(T element);
        void AddRange(T[] elements);
        void Clear();
        void CopyTo(T[] array, int index);
        bool Remove(T element);
    }
}

