namespace DevExpress.Data.Selection
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class SelectionController : IDisposable
    {
        private SelectedRowsCollection selectedRows;
        private SelectedGroupsCollection selectedGroupRows;
        private DataController controller;
        private int lockAddRemoveActions;
        private int selectionLockCount;
        private bool actuallyChanged;
        private List<SelectedRowsCollection> selectionCollections;

        public SelectionController(DataController controller);
        private void AddGroup(List<int> rows, GroupRowInfo group, Dictionary<int, bool> selectedGroups);
        public void BeginSelection();
        public void CancelSelection();
        public virtual void Clear();
        protected virtual List<SelectedRowsCollection> CreateSelectionCollections();
        public virtual void Dispose();
        public void EndSelection();
        protected int[] GetNonGroupedSelectedRows();
        public virtual int[] GetNormalizedSelectedRows();
        public virtual int[] GetNormalizedSelectedRowsEx();
        public virtual int[] GetNormalizedSelectedRowsEx2();
        public bool GetSelected(int controllerRow);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool GetSelectedByListSource(int listSourceRow);
        public object GetSelectedObject(int controllerRow);
        public int[] GetSelectedRows();
        public SelectionController.SelectionInfo GetSelectionInfo();
        internal object GetSelectionObject(int controllerRow);
        protected internal int InternalClear();
        internal void LockAddRemoveAction();
        internal void OnGroupDeleted(GroupRowInfo groupInfo);
        internal void OnItemAdded(int listSourceRow);
        internal void OnItemDeleted(int listSourceRow);
        internal void OnItemFilteredOut(int listSourceRow);
        internal void OnItemMoved(int oldListSourceRow, int newListSourceRow);
        internal void OnReplaceGroupSelection(GroupRowInfo oldGroupInfo, GroupRowInfo newGroupInfo);
        protected internal void OnSelectionChanged(SelectionChangedEventArgs e);
        private void ProcessRows(Dictionary<int, List<int>> groups, int[] rows, Dictionary<int, int> nonSelectedGroups);
        public void RaiseChanged();
        protected internal void RaiseSelectionChanged();
        public void SelectAll();
        public void SetActuallyChanged();
        internal void SetListSourceRowSelected(int listSourceRow, bool selected, object selectionObject);
        public void SetSelected(int controllerRow, bool selected);
        public void SetSelected(int controllerRow, bool selected, object selectionObject);
        internal void UnLockAddRemoveAction();

        private List<SelectedRowsCollection> SelectionCollections { get; }

        protected internal DataController Controller { get; }

        protected internal SelectedRowsCollection SelectedRows { get; }

        protected internal SelectedGroupsCollection SelectedGroupRows { get; }

        public bool IsSelectionLocked { get; }

        public int Count { get; }

        private class ReverseComparer : IComparer
        {
            int IComparer.Compare(object a, object b);
        }

        public class SelectionInfo
        {
            public int SelectedCount;
            public int SelectedDataRows;
            public int SelectedGroupRows;
            public int SelectedRowsCRC;
            public int SelectedGroupRowsCRC;

            public bool Equals(SelectionController.SelectionInfo info);
        }
    }
}

