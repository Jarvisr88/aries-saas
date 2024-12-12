namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    internal interface IGroupValuesCache : IEnumerable<object>, IEnumerable, IDisposable
    {
        int GetIndex(int group);
        int? GetParent(int group);
        object[] GetPath(int group);
        bool IsLoaded(int group);
        bool Reload(int group, object[] children, int? depth = new int?(), string[] texts = null);
        bool TryGetValue(int group, out object[] values);

        int Depth { get; }

        object[] this[int group] { get; }
    }
}

