namespace DevExpress.Office
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    public interface ISimpleCollection<T> : IEnumerable<T>, IEnumerable, ICollection
    {
        T this[int index] { get; }
    }
}

