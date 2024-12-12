namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IXlReadonlyCollection<T> : IEnumerable<T>, IEnumerable
    {
        T this[int index] { get; }

        T this[string name] { get; }

        int Count { get; }
    }
}

