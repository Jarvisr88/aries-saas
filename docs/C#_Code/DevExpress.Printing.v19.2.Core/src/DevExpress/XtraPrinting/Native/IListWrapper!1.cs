namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IListWrapper<T> : IEnumerable<T>, IEnumerable
    {
        void Add(T item);
        void Clear();
        int IndexOf(T item);
        void Insert(T item, int index);
        void RemoveAt(int index);

        int Count { get; }

        T this[int index] { get; }
    }
}

