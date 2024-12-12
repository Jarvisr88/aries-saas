namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IReadOnlyCollection<T> : IEnumerable<T>, IEnumerable, ISupportVisitor<T> where T: class
    {
        bool Contains(T element);

        int Count { get; }
    }
}

