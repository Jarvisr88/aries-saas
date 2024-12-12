namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IHashTree<T> : IHashTree
    {
        bool EnsureExpanded(T element);
        bool Expand(T element);
        bool IsExpanded(T element);
    }
}

