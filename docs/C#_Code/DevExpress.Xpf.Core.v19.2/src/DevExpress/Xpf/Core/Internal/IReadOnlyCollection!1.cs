namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IReadOnlyCollection<out T> : IEnumerable<T>, IEnumerable
    {
        int Count { get; }
    }
}

