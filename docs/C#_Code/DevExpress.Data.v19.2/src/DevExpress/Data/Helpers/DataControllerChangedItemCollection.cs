namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class DataControllerChangedItemCollection : IEnumerable<DataControllerChangedItem>, IEnumerable
    {
        private List<DataControllerChangedItem> innerList;
        public bool AlwaysNotifyVisualClient;

        public void AddItem(int controllerRowHandle, NotifyChangeType changedType, GroupRowInfo parentGroupRow);
        public void AddItem(int controllerRowHandle, NotifyChangeType changedType, GroupRowInfo parentGroupRow, bool groupSimpleChange);
        protected virtual int CalculateTopRowDelta(DataController controller, IDataControllerVisualClient visualClient);
        private void EnsureInnerList();
        private bool HasItem(int visibleRow, NotifyChangeType changedType);
        protected virtual bool IsChangedOnly(DataController controller);
        private bool IsVisibleRangeChanged(int topRowIndex, int pageRowCount);
        private void NotifyChanges(IDataControllerVisualClient visualClient);
        public virtual void NotifyVisualClient(DataController controller, IDataControllerVisualClient visualClient);
        public void RemoveAt(int index);
        protected virtual void RemoveNonVisibleItems();
        IEnumerator<DataControllerChangedItem> IEnumerable<DataControllerChangedItem>.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator();
        protected virtual void UpdateRows(IDataControllerVisualClient visualClient, int topRowIndexDelta);
        public void UpdateVisibleIndexes(VisibleIndexCollection visibleIndexes, bool isAdded);

        public int Count { get; }

        public DataControllerChangedItem this[int index] { get; }

        protected virtual bool IsVisibleRowCountChanged { get; }
    }
}

