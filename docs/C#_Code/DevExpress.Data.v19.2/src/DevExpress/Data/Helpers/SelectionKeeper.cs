namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class SelectionKeeper : SelectionKeeperBase
    {
        private GroupKeeperSelection groupSelection;

        public SelectionKeeper(DataController controller);
        public override void Clear();
        protected virtual GroupKeeperSelection CreateGroupSelectionKeeper();
        protected override void OnRestoreSelection();
        public override void Save();

        protected internal GroupKeeperSelection GroupSelection { get; }

        public override bool HasSelection { get; }
    }
}

