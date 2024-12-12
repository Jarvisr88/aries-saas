namespace DevExpress.Xpf.Core
{
    using System;

    public interface ISupportGetCachedIndex<T>
    {
        int GetCachedIndex(T item);
    }
}

