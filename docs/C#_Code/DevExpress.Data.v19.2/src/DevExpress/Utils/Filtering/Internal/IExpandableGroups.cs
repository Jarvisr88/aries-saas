namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;

    public interface IExpandableGroups
    {
        void ChangeState(int group);
        int GetIndex(int visibleIndex);
        int GetVisibleIndex(int index);
        IEnumerable<int> GetVisibleIndices(IEnumerable<int> indexes);
        IEnumerator<int> GetVisibleIndices(int index);
        IEnumerator<int> GetVisibleIndicesInverted(int index);
        bool IsExpanded(int group);

        int VisibleItemsCount { get; }
    }
}

