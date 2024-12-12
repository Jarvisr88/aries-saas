namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections;

    public interface IVirtualListServerCaps : IVirtualListServer, IList, ICollection, IEnumerable
    {
        bool SortByDataController { get; }

        bool GroupByDataController { get; }
    }
}

