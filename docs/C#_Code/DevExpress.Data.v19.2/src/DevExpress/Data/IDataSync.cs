namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public interface IDataSync
    {
        event CollectionViewFilterSortGroupInfoChangedEventHandler FilterSortGroupInfoChanged;

        void Initialize();
        bool ResetCache();

        List<ListSortInfo> Sort { get; }

        int GroupCount { get; }

        bool AllowSyncSortingAndGrouping { get; set; }

        bool HasFilter { get; }
    }
}

