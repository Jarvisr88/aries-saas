namespace DevExpress.Office
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IReadOnlyCollection<T> : IEnumerable<T>, IEnumerable
    {
        int Count { get; }
    }
}

