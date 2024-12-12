namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IHashTree
    {
        bool EnsureExpanded(int hash);
        bool Expand(int hash);
        bool IsExpanded(int hash);
    }
}

