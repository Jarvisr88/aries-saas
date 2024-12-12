namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface ISummaryItemsCollection : IEnumerable<SummaryItem>, IEnumerable
    {
        SummaryItem Add(IDataColumnInfo column, SummaryItemTypeEx type, decimal argument, ICustomUIFilterSummaryItem tag);
        void BeginUpdate();
        void CancelUpdate();
        void EndUpdate();
    }
}

