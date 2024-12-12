namespace DevExpress.Xpf.Data
{
    using DevExpress.Xpf.Data.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class PagedAsyncSource : PagedSourceBase, IAsyncSourceEventsHelperClient
    {
        public event EventHandler<FetchPageAsyncEventArgs> FetchPage;

        public event EventHandler<GetSummariesAsyncEventArgs> GetTotalSummaries;

        public event EventHandler<GetUniqueValuesAsyncEventArgs> GetUniqueValues;

        private event EventHandler<UpdateRowAsyncEventArgs> UpdateRow;

        protected override VirtualSourceEventsHelper CreateEventsHelper() => 
            new AsyncSourceEventsHelper(this);

        EventHandler<GetSummariesAsyncEventArgs> IAsyncSourceEventsHelperClient.GetTotalSummariesHandler() => 
            this.GetTotalSummaries;

        EventHandler<GetUniqueValuesAsyncEventArgs> IAsyncSourceEventsHelperClient.GetUniqueValuesHandler() => 
            this.GetUniqueValues;

        EventHandler<UpdateRowAsyncEventArgs> IAsyncSourceEventsHelperClient.GetUpdateRowHandler() => 
            this.UpdateRow;

        protected override Task<FetchRowsResult> FetchRowsAsync(VirtualSourceBase.GetRowsState state, CancellationToken cancellationToken)
        {
            FetchPageAsyncEventArgs e = new FetchPageAsyncEventArgs(cancellationToken, state.Skip, state.SkipToken, state.Take.Value, state.SortOrder, state.Filter);
            this.FetchPage(this, e);
            return e.Result;
        }

        protected override VirtualSourceBase.Handlers GetHandlers() => 
            new VirtualSourceBase.Handlers(this.FetchPage, this.GetTotalSummaries, this.GetUniqueValues);
    }
}

