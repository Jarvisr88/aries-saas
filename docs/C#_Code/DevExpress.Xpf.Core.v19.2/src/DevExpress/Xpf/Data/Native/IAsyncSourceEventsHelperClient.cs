namespace DevExpress.Xpf.Data.Native
{
    using System;

    public interface IAsyncSourceEventsHelperClient
    {
        EventHandler<GetSummariesAsyncEventArgs> GetTotalSummariesHandler();
        EventHandler<GetUniqueValuesAsyncEventArgs> GetUniqueValuesHandler();
        EventHandler<UpdateRowAsyncEventArgs> GetUpdateRowHandler();
    }
}

