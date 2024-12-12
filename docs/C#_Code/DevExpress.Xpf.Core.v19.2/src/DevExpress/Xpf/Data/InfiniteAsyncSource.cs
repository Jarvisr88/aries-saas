namespace DevExpress.Xpf.Data
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Data.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class InfiniteAsyncSource : InfiniteSourceBase, IAsyncSourceEventsHelperClient
    {
        public event EventHandler<FetchRowsAsyncEventArgs> FetchRows;

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
            FetchRowsAsyncEventArgs fetchArgs = new FetchRowsAsyncEventArgs(cancellationToken, state.Skip, state.SkipToken, state.SortOrder, state.Filter);
            this.FetchRows(this, fetchArgs);
            return fetchArgs.Result.Linq<FetchRowsResult>(((TaskScheduler) null)).MapException<FetchRowsResult>(exception => InfiniteSourceBase.AllowRetryWrapperException.Wrap(exception, fetchArgs)).Schedule<FetchRowsResult>(TaskScheduler.Default);
        }

        protected override VirtualSourceBase.Handlers GetHandlers() => 
            new VirtualSourceBase.Handlers(this.FetchRows, this.GetTotalSummaries, this.GetUniqueValues);
    }
}

