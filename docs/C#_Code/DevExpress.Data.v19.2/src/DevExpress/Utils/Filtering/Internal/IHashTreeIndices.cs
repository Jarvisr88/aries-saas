namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public interface IHashTreeIndices
    {
        bool EnsureVisibleIndex(int index, out int value);
        int GetIndex(int visibleIndex);
        int GetVisibleIndex(int index);
        IEnumerable<int> GetVisibleIndices(IEnumerable<int> indexes);
        IEnumerator<int> GetVisibleIndices(int index);
        IEnumerator<int> GetVisibleIndicesInverted(int index);

        int VisibleCount { get; }
    }
}

