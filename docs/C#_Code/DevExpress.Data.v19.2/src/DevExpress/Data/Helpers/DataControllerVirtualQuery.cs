namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DataControllerVirtualQuery : IDisposable
    {
        private object source;
        private BaseGridController controller;
        private int lastRowCount;
        private OperationCompleted lastGetUniqueValuesCompleted;

        public DataControllerVirtualQuery(BaseGridController controller);
        protected internal void ApplySortGroup();
        protected virtual void ApplySortGroupCore();
        public void CalcTotalSummary();
        protected internal void CheckLoadMore();
        public virtual void Dispose();
        private ServerModeOrderDescriptor[] GetSortDescriptors();
        protected virtual ServerModeSummaryDescriptor[] GetTotalSummary();
        public virtual object[] GetUniqueColumnValues(DataColumnInfo columnInfo, ColumnValuesArguments args, OperationCompleted completed);
        public bool HasMoreData();
        private void OnBusyChanged(object sender, EventArgs e);
        private void OnErrorOccurred(object sender, ErrorEventArgs e);
        private void OnRowsLoaded(object sender, EventArgs e);
        protected virtual void OnRowsLoadedCore();
        private void OnTotalSummaryReady(object sender, VirtualServerModeTotalSummaryReadyEventArgs e);
        private void OnUniqueValuesReady(object sender, UniqueValuesReadyEventArgs e);
        public void Refresh();
        protected virtual void RefreshCore();
        public void RequestMoreData(int suggestedRowCount = 0);
        protected virtual void RequestMoreDataCore(int suggestedRowCount);
        private void SubscribeNotifications();
        private void UpdateTotalSummaryResults(VirtualServerModeTotalSummaryReadyEventArgs e);

        protected virtual bool AllowUnsafeThreading { get; }

        public virtual bool IsSupportUniqueValues { get; }

        public virtual bool IsSupportSummary { get; }

        public virtual bool CanReset { get; }

        public virtual bool CanFilter { get; }

        public virtual bool CanSort { get; }

        public virtual bool CanGroup { get; }

        protected BaseGridController Controller { get; }

        public bool IsSupported { get; }

        protected IXtraRefreshable RefreshableSource { get; }

        protected IXtraSourceError SourceError { get; }

        protected IXtraMoreRows MoreRowsSource { get; }

        protected IXtraBusySupport BusySource { get; }

        protected IVirtualListServer ServerSource { get; }

        protected IXtraGetUniqueValues UniqueValuesSource { get; }

        protected IVirtualListServerWithTotalSummary ServerSourceSummary { get; }

        public bool IsBusy { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataControllerVirtualQuery.<>c <>9;
            public static Func<ListSourceSummaryItem, CriteriaOperator, Aggregate, ServerModeSummaryDescriptor> <>9__57_0;

            static <>c();
            internal ServerModeSummaryDescriptor <GetTotalSummary>b__57_0(ListSourceSummaryItem s, CriteriaOperator c, Aggregate a);
        }

        private class VirtualServerModeSummaryDescriptor : ServerModeSummaryDescriptor
        {
            public VirtualServerModeSummaryDescriptor(CriteriaOperator expression, Aggregate summaryType);
            public override bool Equals(ServerModeSummaryDescriptor other);

            public DevExpress.Data.SummaryItem SummaryItem { get; set; }
        }
    }
}

