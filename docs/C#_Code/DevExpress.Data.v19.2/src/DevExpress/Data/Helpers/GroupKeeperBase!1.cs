namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;

    public abstract class GroupKeeperBase<T> where T: IEnumerable
    {
        private DataController controller;
        private string[] groupDescriptors;
        private bool? storeAllSelected;
        private Dictionary<GroupDataInfo, object> groupsData;

        public GroupKeeperBase(DataController controller);
        public virtual void Clear();
        protected GroupDataInfo GetGroupData(GroupRowInfo group);
        protected internal string[] GetGroupDescriptors();
        protected internal virtual int GetMaxRestoreLevel();
        protected abstract T GetSelectedGroups();
        protected virtual bool IsAllSelected();
        public virtual bool IsRestoreSelected(GroupRowInfo group, bool removeAfterRestore = false);
        protected abstract void OnRestoreAllSelected();
        protected abstract void OnRestoreGroup(GroupRowInfo group, object selection);
        protected virtual void OnSaveGroup(GroupRowInfo group, GroupDataInfo groupData, object selectionObject);
        public virtual bool Restore();
        internal bool RestoreFromStream(Stream stream);
        internal void RestoreFromStreamCore(Stream stream);
        protected virtual bool RestoreGroups();
        public virtual void Save();
        protected internal virtual void SaveGroup(GroupRowInfo group, object selectionObject);
        protected internal virtual void SaveGroupDescriptors();
        protected virtual void SaveGroups();
        internal void SaveToStream(Stream stream);
        protected virtual int TryRestoreData(GroupRowInfo group, GroupDataInfo data);

        public string[] GroupDescriptors { get; }

        public virtual bool HasSelection { get; }

        protected DataController Controller { get; }

        protected Dictionary<GroupDataInfo, object> GroupsData { get; }

        protected internal bool IsRestoreAllSelected { get; }

        protected virtual int SelectionCount { get; }
    }
}

