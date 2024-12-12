namespace DevExpress.Xpf.Data
{
    using DevExpress.Xpf.Data.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class InfiniteSource : InfiniteSourceBase, ISyncSourceEventsHelperClient
    {
        public event EventHandler<CreateSourceEventArgs> CreateSource;

        public event EventHandler<DisposeSourceEventArgs> DisposeSource;

        public event EventHandler<FetchRowsEventArgs> FetchRows;

        public event EventHandler<GetSummariesEventArgs> GetTotalSummaries;

        public event EventHandler<GetUniqueValuesEventArgs> GetUniqueValues;

        private event EventHandler<UpdateRowEventArgs> UpdateRow;

        protected override VirtualSourceEventsHelper CreateEventsHelper() => 
            new SyncSourceEventsHelper(this);

        EventHandler<CreateSourceEventArgs> ISyncSourceEventsHelperClient.CreateSourceHandler() => 
            this.CreateSource;

        EventHandler<DisposeSourceEventArgs> ISyncSourceEventsHelperClient.DisposeSourceHandler() => 
            this.DisposeSource;

        EventHandler<GetSummariesEventArgs> ISyncSourceEventsHelperClient.GetTotalSummariesHandler() => 
            this.GetTotalSummaries;

        EventHandler<GetUniqueValuesEventArgs> ISyncSourceEventsHelperClient.GetUniqueValuesHandler() => 
            this.GetUniqueValues;

        EventHandler<UpdateRowEventArgs> ISyncSourceEventsHelperClient.UpdateRowHandler() => 
            this.UpdateRow;

        protected override Task<FetchRowsResult> FetchRowsAsync(VirtualSourceBase.GetRowsState state, CancellationToken cancellationToken)
        {
            FetchRowsEventArgs e = new FetchRowsEventArgs(cancellationToken, this.SyncEvents.Source, state.Skip, state.SkipToken, state.SortOrder, state.Filter);
            try
            {
                this.FetchRows(this, e);
            }
            catch (Exception exception1)
            {
                throw InfiniteSourceBase.AllowRetryWrapperException.Wrap(exception1, e);
            }
            return this.SyncEvents.GetFetchResult(e.Result, e.HasMoreRows, e.NextSkipToken);
        }

        protected override VirtualSourceBase.Handlers GetHandlers() => 
            new VirtualSourceBase.Handlers(this.FetchRows, this.GetTotalSummaries, this.GetUniqueValues);

        public bool AreSourceRowsThreadSafe
        {
            get => 
                this.SyncEvents.AreSourceRowsThreadSafe;
            set => 
                this.SyncEvents.AreSourceRowsThreadSafe = value;
        }

        private SyncSourceEventsHelper SyncEvents =>
            (SyncSourceEventsHelper) base.events;
    }
}

