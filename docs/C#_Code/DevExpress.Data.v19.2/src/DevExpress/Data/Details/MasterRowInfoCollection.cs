namespace DevExpress.Data.Details
{
    using DevExpress.Data;
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class MasterRowInfoCollection : CollectionBase, IIndexRenumber
    {
        private List<MasterRowInfo> fastIndex;
        private DataController controller;
        private bool fastIndexDirty;

        public MasterRowInfoCollection(DataController controller);
        public MasterRowInfo CreateRow(DataController parentController, int parentListSourceRow, object parentRowKey);
        int IIndexRenumber.GetCount();
        int IIndexRenumber.GetValue(int pos);
        void IIndexRenumber.SetValue(int pos, int val);
        public virtual MasterRowInfo Find(int listSourceRow);
        public MasterRowInfo FindByKey(object rowKey);
        public DetailInfo FindOwner(object detailOwner);
        public MasterRowInfo GetByCachedIndex(int listSourceRow);
        protected internal void OnItemAdded(int listSourceRow);
        protected internal void OnItemDeleted(int listSourceRow, bool filterChange);
        protected internal void OnItemMoved(int oldListSourceRow, int newListSourceRow);
        private void RebuildFastIndex();
        public virtual void Remove(MasterRowInfo row);
        private void RenumberIndexes(int listSourceRow, bool increment);
        private void SetFastIndexDirty();

        protected DataController Controller { get; }

        public MasterRowInfo this[int index] { get; }
    }
}

