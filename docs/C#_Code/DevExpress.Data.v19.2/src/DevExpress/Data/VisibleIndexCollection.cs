namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class VisibleIndexCollection : ICollection, IEnumerable
    {
        private int[] list;
        private int[] scrollableIndexes;
        private int count;
        private int scrollableIndexesCount;
        private bool expandedGroupsIncludedInScrollableIndexes;
        private DataControllerBase controller;
        private GroupRowInfoCollection groupInfo;
        private bool isDirty;
        private bool modified;
        private Dictionary<int, bool> singleItems;
        private VisibleIndexHeightInfo scrollHeightInfo;
        private uint hashIndex;

        public VisibleIndexCollection(DataControllerBase controller, GroupRowInfoCollection groupInfo);
        public void Add(int controllerRowHandle);
        protected void AddVisibleDataRows(GroupRowInfo rowInfo);
        protected virtual void BuildScrollableIndexes();
        public void BuildVisibleIndexes(GroupRowInfo groupRow, bool expandAll);
        public void BuildVisibleIndexes(int visibleCount, bool allowNonGroupedList, bool expandAll);
        protected virtual void BuildVisibleIndexesEx(GroupRowInfo groupRow, bool expandAll, bool expanded);
        public void Clear();
        protected void ClearCore(bool recreateList);
        public bool Contains(int controllerRowHandle);
        public int ConvertIndexToScrollIndex(int index, bool allowFixedGroups);
        public int ConvertScrollIndexToIndex(int scrollIndex, bool allowFixedGroups);
        public void CopyTo(Array array, int index);
        private void ExtendArray();
        private int FindScrollableIndex(int index);
        public IEnumerator GetEnumerator();
        public int GetHandle(int visibleIndex);
        protected virtual int GetMaxCount();
        protected virtual List<GroupRowInfo> GetRootGroups();
        public int IndexOf(int controllerRowHandle);
        public bool IsSingleGroupRow(int controllerDataRowHanlde);
        protected virtual void PutLastGroupsIntoScrollableIndexes(List<VisibleIndexCollection.GroupInfoVisibleIndexPair> lastGroups, int tillLevel);
        public void ResetModified();
        public void SetDirty();
        public void SetDirty(bool value);

        [CLSCompliant(false)]
        public uint HashIndex { get; }

        public bool IsModified { get; }

        public bool IsDirty { get; }

        public VisibleIndexHeightInfo ScrollHeightInfo { get; }

        public bool ExpandedGroupsIncludedInScrollableIndexes { get; set; }

        public int[] ScrollableIndexes { get; }

        public int ScrollableIndexesCount { get; }

        public int Count { get; }

        protected DataControllerBase Controller { get; }

        protected internal virtual GroupRowInfoCollection GroupInfo { get; set; }

        public GroupRowInfoCollection GroupInfoCore { get; }

        public int this[int visibleIndex] { get; }

        public bool IsEmpty { get; }

        public bool IsSynchronized { get; }

        public object SyncRoot { get; }

        private class ArrayEnumerator : IEnumerator
        {
            private VisibleIndexCollection indexes;
            private int position;

            public ArrayEnumerator(VisibleIndexCollection indexes);
            bool IEnumerator.MoveNext();
            void IEnumerator.Reset();

            object IEnumerator.Current { get; }
        }

        [StructLayout(LayoutKind.Sequential)]
        protected struct GroupInfoVisibleIndexPair
        {
            public GroupRowInfo Group;
            public int VisibleIndex;
        }
    }
}

