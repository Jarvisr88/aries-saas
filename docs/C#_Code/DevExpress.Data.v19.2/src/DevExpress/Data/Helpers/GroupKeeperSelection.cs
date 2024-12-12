namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;

    public class GroupKeeperSelection : GroupKeeperBase<IDictionary<GroupRowInfo, object>>
    {
        private SelectionKeeper keeper;
        private GroupDataInfo currentRowData;

        public GroupKeeperSelection(SelectionKeeper keeper);
        public override void Clear();
        protected override IDictionary<GroupRowInfo, object> GetSelectedGroups();
        protected bool IsCurrentRow(object selectionObject);
        public bool IsRestoreSelectedCurrent(GroupRowInfo group);
        protected override void OnRestoreAllSelected();
        protected virtual void OnRestoreCurrentGroup(GroupRowInfo group);
        protected override void OnRestoreGroup(GroupRowInfo group, object selection);
        protected override void OnSaveGroup(GroupRowInfo group, GroupDataInfo groupData, object selectionObject);
        protected override int TryRestoreData(GroupRowInfo group, GroupDataInfo data);

        protected SelectionKeeper Keeper { get; }

        protected override int SelectionCount { get; }
    }
}

