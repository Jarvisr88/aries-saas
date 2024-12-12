namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.XtraGrid;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class DataColumnSortInfoCollection : ColumnInfoNotificationCollection<DataColumnSortInfo>
    {
        private int groupCount;

        public DataColumnSortInfoCollection(DataControllerBase controller);
        public DataColumnSortInfoCollection(DataControllerBase controller, CollectionChangeEventHandler collectionChanged);
        private DataColumnSortInfo Add(DataColumnSortInfo info);
        public DataColumnSortInfo Add(DataColumnInfo columnInfo, ColumnSortOrder sortOrder);
        public DataColumnSortInfo Add(DataColumnInfo columnInfo, ColumnSortOrder sortOrder, ColumnGroupInterval groupInterval, bool runningSummary, bool crossGroupRunningSummary);
        public void AddRange(params DataColumnSortInfo[] sortInfos);
        public void AddRange(int groupCount, params DataColumnSortInfo[] sortInfos);
        public void AddRange(DataColumnSortInfo[] sortInfos, int groupCount);
        [Obsolete]
        public void Assign(ColumnGroupSortInfoCollection oldSort);
        public void ChangeGroupSorting(int index);
        public void ClearAndAddRange(params DataColumnSortInfo[] sortInfos);
        public void ClearAndAddRange(int groupCount, params DataColumnSortInfo[] sortInfos);
        public void ClearAndAddRange(DataColumnSortInfo[] sortInfos, int groupCount);
        public virtual DataColumnSortInfoCollection Clone();
        protected internal bool Contains(string columnName);
        public int GetGroupIndex(DataColumnInfo info);
        public int GetSortIndex(DataColumnInfo info);
        protected override bool IsColumnInfoUsed(int index, IList<DataColumnInfo> unusedColumns);
        public bool IsEquals(DataColumnSortInfoCollection collection);
        public DataColumnSortInfo[] ToArray();

        public int GroupCount { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataColumnSortInfoCollection.<>c <>9;
            public static Func<DataColumnSortInfo, DataColumnSortInfo> <>9__23_0;

            static <>c();
            internal DataColumnSortInfo <Clone>b__23_0(DataColumnSortInfo q);
        }
    }
}

