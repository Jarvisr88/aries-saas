namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal interface IVisibleItemsSource
    {
        void Expand(int group);
        int GetIndex(int index, bool returnSourceIndex = false);
        IEnumerable<int> Indices(HashSet<int> groups);
        IEnumerable<int> Indices(IEnumerable<int> indexes);
        IEnumerator<int> Indices(int index, bool forwardDirection = true);

        int Count { get; }
    }
}

