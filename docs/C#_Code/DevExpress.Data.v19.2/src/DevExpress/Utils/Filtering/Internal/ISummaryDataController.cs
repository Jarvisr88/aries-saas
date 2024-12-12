namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data;
    using System;

    public interface ISummaryDataController
    {
        IDataColumnInfo GetColumn(string fieldName);

        ISummaryItemsCollection Summary { get; }
    }
}

