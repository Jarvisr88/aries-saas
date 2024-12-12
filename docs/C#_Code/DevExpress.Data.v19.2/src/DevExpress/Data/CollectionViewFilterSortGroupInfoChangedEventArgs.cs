namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;

    public class CollectionViewFilterSortGroupInfoChangedEventArgs
    {
        private int groupCount;
        private bool filterChanged;
        private List<ListSortInfo> sortInfo;
        private bool needRefresh;

        public CollectionViewFilterSortGroupInfoChangedEventArgs(List<ListSortInfo> sortInfo, int groupCount, bool filterChanged, bool needRefresh);

        public List<ListSortInfo> SortInfo { get; private set; }

        public int GroupCount { get; private set; }

        public bool FilterChanged { get; private set; }

        public bool NeedRefresh { get; private set; }
    }
}

