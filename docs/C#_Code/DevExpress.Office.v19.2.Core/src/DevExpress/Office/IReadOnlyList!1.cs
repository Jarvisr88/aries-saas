namespace DevExpress.Office
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IReadOnlyList<T> : DevExpress.Office.IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
    {
        T this[int index] { get; }
    }
}

