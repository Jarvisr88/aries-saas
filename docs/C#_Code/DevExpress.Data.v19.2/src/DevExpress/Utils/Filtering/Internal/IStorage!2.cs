namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IStorage<TPath, T> : IEnumerable<T>, IEnumerable
    {
        IEnumerable<KeyValuePair<TPath, TValue>> GetPairs<TValue>(Func<T, TPath> getPath, Func<T, TValue> accessor);
        IEnumerable<TPath> GetPaths(Func<T, TPath> getPath);

        T this[TPath path, Func<T, TPath> getPath] { get; }
    }
}

