namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Selection;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SelectionKeeperBase
    {
        private SelectedRowInfo[] dataRowsInfo;
        private DataController controller;

        public SelectionKeeperBase(DataController controller);
        public virtual void Clear();
        private bool CompareRowKeys(object key1, object key2);
        protected virtual object GetDataRowKey(int listSourceRow);
        protected virtual Dictionary<object, int> GetRestoreDictionary();
        protected bool IsValidListSourceIndex(int listIndex);
        protected internal virtual void OnRestoreCurrentGroup(GroupRowInfo group);
        protected virtual void OnRestoreDataRow(ref SelectedRowInfo info, int listIndex);
        protected virtual void OnRestoreDataRowAlt(int index, int listIndex);
        protected virtual void OnRestoreEnd();
        protected virtual void OnRestoreSelection();
        protected virtual void ResetRestoredFlag();
        public virtual void Restore();
        protected virtual bool RestoreDataRow(ref SelectedRowInfo info, bool allowIndexOf);
        protected virtual void RestoreDataRowCore(int controllerRow, int listIndex, object selectionObject);
        protected virtual void RestoreDataRows();
        protected virtual void RestoreDataRowsAlt();
        public virtual void Save();
        protected virtual void SaveDataRow(ref SelectedRowInfo info, int listIndex, object selectedObject);
        protected virtual void SaveDataRows(Dictionary<int?, object> dataRows);
        protected virtual bool TrySmartRestoreDataRows();

        public DataController Controller { get; }

        public virtual bool AllowKeepSelection { get; }

        protected virtual SelectedRowsCollection SelectedRows { get; }

        protected virtual int SelectionCount { get; }

        public virtual bool HasSelection { get; }

        protected virtual bool IsAllowIndexOfSearch { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectionKeeperBase.<>c <>9;
            public static Func<SelectedRowInfo, bool> <>9__22_0;

            static <>c();
            internal bool <GetRestoreDictionary>b__22_0(SelectedRowInfo q);
        }
    }
}

