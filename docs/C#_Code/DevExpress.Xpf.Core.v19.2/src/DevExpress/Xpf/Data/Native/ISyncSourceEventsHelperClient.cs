namespace DevExpress.Xpf.Data.Native
{
    using System;

    public interface ISyncSourceEventsHelperClient
    {
        EventHandler<CreateSourceEventArgs> CreateSourceHandler();
        EventHandler<DisposeSourceEventArgs> DisposeSourceHandler();
        EventHandler<GetSummariesEventArgs> GetTotalSummariesHandler();
        EventHandler<GetUniqueValuesEventArgs> GetUniqueValuesHandler();
        EventHandler<UpdateRowEventArgs> UpdateRowHandler();
    }
}

