namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class SelectionAndCurrentKeeper : SelectionKeeper
    {
        internal static readonly object CurrentObject;
        private SelectedRowInfo currentRow;

        static SelectionAndCurrentKeeper();
        public SelectionAndCurrentKeeper(BaseGridController controller);
        public override void Clear();
        protected virtual bool IsAllowRestoreCurrentRow();
        protected internal override void OnRestoreCurrentGroup(GroupRowInfo group);
        protected virtual void OnRestoreCurrentRow(ref SelectedRowInfo info, int listIndex);
        protected override void OnRestoreDataRow(ref SelectedRowInfo info, int listIndex);
        protected override void OnRestoreEnd();
        protected override void ResetRestoredFlag();
        protected virtual void RestoreCurrentDataRow(ref SelectedRowInfo currentRow, bool allowIndexOf);
        public override void Save();
        protected override bool TrySmartRestoreDataRows();

        public BaseGridController Controller { get; }

        public override bool HasSelection { get; }

        protected bool IsAllowCurrentRow { get; }

        protected bool IsCurrentRowGroupRow { get; }

        protected bool HasCurrentRowSelection { get; }

        protected virtual bool IsCurrentRowRestored { get; }

        protected virtual bool AllowResetControllerRow { get; }
    }
}

