namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Collections;
    using System.Reflection;

    [Wrapper, BindingFlags(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)]
    public interface IMonoCollection<T>
    {
        void Add(T item);
        IEnumerator GetEnumerator();
        bool Remove(T item);
    }
}

