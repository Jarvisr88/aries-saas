namespace DevExpress.Xpf.Data
{
    using DevExpress.Xpf.Data.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class PagedSource : PagedSourceBase, ISyncSourceEventsHelperClient
    {
        public event EventHandler<CreateSourceEventArgs> CreateSource;

        public event EventHandler<DisposeSourceEventArgs> DisposeSource;

        public event EventHandler<FetchPageEventArgs> FetchPage;

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
            FetchPageEventArgs e = new FetchPageEventArgs(cancellationToken, this.SyncEvents.Source, state.Skip, state.SkipToken, state.Take.Value, state.SortOrder, state.Filter);
            this.FetchPage(this, e);
            return this.SyncEvents.GetFetchResult(e.Result, e.HasMoreRows, e.NextSkipToken);
        }

        protected override VirtualSourceBase.Handlers GetHandlers() => 
            new VirtualSourceBase.Handlers(this.FetchPage, this.GetTotalSummaries, this.GetUniqueValues);

        protected override void HandlePendingPages()
        {
            base.HandlePendingPages();
            foreach (VirtualSourceBase.GetRowsState state in base.ThrottlePendingPages())
            {
                int pageIndex = base.SkipToPageIndex(state.Skip);
                if (base.pageCache.IsPageLoading(pageIndex))
                {
                    base.pageCache.RemovePage(pageIndex);
                }
            }
        }

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

