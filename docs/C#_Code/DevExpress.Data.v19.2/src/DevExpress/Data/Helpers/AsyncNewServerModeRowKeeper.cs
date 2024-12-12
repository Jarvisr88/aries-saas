namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class AsyncNewServerModeRowKeeper : NewListSourceRowKeeper, IClassicRowKeeperAsync
    {
        private bool totalsReceived;
        private bool restoreRequested;

        public AsyncNewServerModeRowKeeper(AsyncServerModeDataController controller, ExpandedGroupKeeper groupsKeeper, SelectionKeeper selectionKeeper);
        protected override void ClearCore();
        bool IClassicRowKeeperAsync.IsRestoredAsExpanded(GroupRowInfo group);
        void IClassicRowKeeperAsync.OnTotalsReceived();
        protected override void RestoreClear();
        protected override bool RestoreCore();
        protected override bool RestoreIncrementalCore();
        protected override void SaveCore();

        protected AsyncServerModeDataController Controller { get; }

        protected AsyncServerModeSelectionAndCurrentKeeper Selection { get; }

        bool IClassicRowKeeperAsync.IsRestoreAllExpanded { get; }
    }
}

