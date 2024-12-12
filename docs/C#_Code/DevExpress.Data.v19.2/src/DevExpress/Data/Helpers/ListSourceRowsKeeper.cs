namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;
    using System.IO;

    public class ListSourceRowsKeeper : IDisposable, IClassicRowKeeper
    {
        private SelectedRowsKeeper selectionHash;
        private GroupedRowsKeeperEx groupHashEx;
        internal PropertyDescriptor[] groupColumnsInfo;
        private DataController controller;
        public static bool SuppressRestoreSelection;
        public static bool SuppressRestoreGrouping;

        public ListSourceRowsKeeper(DataController controller, SelectedRowsKeeper rowsKeeper);
        internal bool CheckGroupedColumns();
        public virtual void Clear();
        protected virtual GroupedRowsKeeperEx CreateGroupRowsKeeper();
        void IClassicRowKeeper.ClearSelection();
        bool IClassicRowKeeper.GroupsRestoreFromStream(Stream stream);
        void IClassicRowKeeper.GroupsSaveToStream(Stream stream);
        void IClassicRowKeeper.SaveOnFilter();
        public void Dispose();
        protected virtual object ExGetGroupRowKeyCore(GroupRowInfo group);
        protected PropertyDescriptor[] GetGroupedColumns();
        protected int GetMaxAllowedGroupLevel();
        private void RemoveCollapsedGroupsFromHash();
        public bool Restore();
        protected bool RestoreCore();
        protected virtual bool RestoreCore(bool clear);
        protected virtual bool RestoreGrouping();
        public bool RestoreIncremental();
        private void RestoreSelection(int count);
        protected virtual void RestoreSelectionCore(int count);
        public bool RestoreStream();
        public virtual void Save();
        public virtual void SaveIncremental();
        public virtual void SaveOnRefresh(bool isEndUpdate);

        protected SelectedRowsKeeper SelectionHash { get; }

        protected internal GroupedRowsKeeperEx GroupHashEx { get; }

        protected DataController Controller { get; }

        protected BaseDataControllerHelper Helper { get; }

        protected bool HasSaved { get; }
    }
}

