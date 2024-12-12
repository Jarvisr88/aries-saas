namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IXlPageBreaks : IEnumerable<int>, IEnumerable, ICollection
    {
        void Add(int position);
        void Clear();
        bool Contains(int position);
        int IndexOf(int position);
        void Remove(int position);
        void RemoveAt(int index);

        int this[int index] { get; }
    }
}

