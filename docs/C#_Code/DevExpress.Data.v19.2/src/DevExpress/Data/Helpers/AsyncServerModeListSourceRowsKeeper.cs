namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;

    public class AsyncServerModeListSourceRowsKeeper : ListSourceRowsKeeper, IClassicRowKeeperAsync
    {
        private bool needRestoreGrouping;
        private int maxRestoreGroupLevel;
        private List<AsyncServerModeListSourceRowsKeeper.RowInfo> currentRowInfo;

        public AsyncServerModeListSourceRowsKeeper(AsyncServerModeDataController controller, SelectedRowsKeeper rowsKeeper);
        public override void Clear();
        protected override GroupedRowsKeeperEx CreateGroupRowsKeeper();
        bool IClassicRowKeeperAsync.IsRestoredAsExpanded(GroupRowInfo group);
        void IClassicRowKeeperAsync.OnTotalsReceived();
        protected AsyncServerModeListSourceRowsKeeper.RowInfo GetCurrentRowKey(int index);
        protected AsyncServerModeListSourceRowsKeeper.RowInfo GetRowKey(int controllerRow);
        public virtual bool IsExpandGroup(GroupRowInfo group);
        protected internal virtual void OnTotalsReceived();
        protected override bool RestoreCore(bool clear);
        public override void Save();
        private void SaveCurrentRow();
        public override void SaveIncremental();
        public override void SaveOnRefresh(bool isEndUpdate);
        public virtual bool TryRestoreCurrentControllerRow(int index);
        private bool TryRestoreGroupedCurrentControllerRow(int index);

        protected internal AsyncServerModeGroupedRowsKeeperEx GroupHashEx { get; }

        protected AsyncServerModeDataController Controller { get; }

        protected internal bool AllowRestoreGrouping { get; }

        bool IClassicRowKeeperAsync.IsRestoreAllExpanded { get; }

        protected class RowInfo
        {
            private int level;
            private object key;

            public RowInfo(object key);
            public RowInfo(int level, object key);

            public object Key { get; }

            public int Level { get; }

            public bool IsGroupRow { get; }
        }
    }
}

