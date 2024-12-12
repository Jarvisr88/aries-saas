namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Hierarchy;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public abstract class RowsContainerSyncronizerBase
    {
        protected readonly RowsContainer dataContainer;
        private Dictionary<object, RowDataBase> matchedItems = new Dictionary<object, RowDataBase>();
        private bool changed;

        protected RowsContainerSyncronizerBase(RowsContainer dataContainer)
        {
            this.dataContainer = dataContainer;
        }

        private void AfterSyncronize()
        {
            this.SetInvisibleIndex(this.GetUnmatchedItems(), true);
            if (this.changed)
            {
                ((VersionedObservableCollection<RowDataBase>) this.Items).RaiseCollectionChanged();
            }
        }

        private void AssignRowDataFromNodeContainer(NodeContainer nodeContainer, RowDataBase rowData, int nodeIndex)
        {
            if (rowData == null)
            {
                this.dataContainer.SynchronizationQueues.UnsynchronizedNodes.Enqueue(new UnsynchronizedNodeInfo(this.dataContainer, nodeContainer, nodeIndex));
            }
            else
            {
                this.SetRowDataVisibleIndex(rowData, nodeIndex);
                rowData.AssignVirtualizedRowData(this.dataContainer, nodeContainer, nodeContainer.Items[nodeIndex], false);
                this.UpdateFixedRowPosition(rowData);
            }
        }

        private void BeforeSyncronize(NodeContainer nodeContainer)
        {
            foreach (RowDataBase base2 in this.Items)
            {
                bool flag = false;
                foreach (RowNode node in nodeContainer.Items)
                {
                    if (node.IsMatchedRowData(base2) && !this.matchedItems.ContainsKey(node.MatchKey))
                    {
                        this.matchedItems.Add(node.MatchKey, base2);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    this.EnqueueUnmatchedItem(base2);
                }
            }
        }

        protected abstract RowDataBase DequeueUnmatchedItem(object matchKey);
        protected abstract void EnqueueUnmatchedItem(RowDataBase rowData);
        private RowDataBase GetItem(RowNode rowNode)
        {
            RowDataBase base2 = null;
            return (!this.matchedItems.TryGetValue(rowNode.MatchKey, out base2) ? this.DequeueUnmatchedItem(rowNode.MatchKey) : base2);
        }

        private RowDataBase GetRowData(NodeContainer nodeContainer, int nodeIndex) => 
            this.GetItem(nodeContainer.Items[nodeIndex]);

        protected abstract IEnumerable<RowDataBase> GetUnmatchedItems();
        private void SetInvisibleIndex(IEnumerable items, bool storeAsFreeData)
        {
            foreach (RowDataBase base2 in items)
            {
                if (!OrderPanelBase.IsInvisibleIndex(((ISupportVisibleIndex) base2).VisibleIndex))
                {
                    this.changed = true;
                    base2.SetVisibleIndex(-1);
                    this.SetInvisibleIndex(((IItem) base2).ItemsContainer.Items, false);
                }
                if (storeAsFreeData)
                {
                    base2.StoreAsFreeData(this.dataContainer);
                }
            }
        }

        private void SetRowDataVisibleIndex(RowDataBase rowData, int nodeIndex)
        {
            if (((ISupportVisibleIndex) rowData).VisibleIndex != nodeIndex)
            {
                this.changed = true;
                rowData.SetVisibleIndex(nodeIndex);
            }
        }

        public void Synchronize(NodeContainer nodeContainer)
        {
            bool flag = this.dataContainer.BaseSyncronize(nodeContainer);
            if (ReferenceEquals(this.Items, null) | flag)
            {
                this.dataContainer.StoreFreeData();
                this.dataContainer.RaiseItemsRemoved(this.Items);
                this.dataContainer.InitItemsCollection();
            }
            this.BeforeSyncronize(nodeContainer);
            for (int i = this.SyncronizeStartIndex; i < nodeContainer.Items.Count; i++)
            {
                RowDataBase rowData = this.GetRowData(nodeContainer, i);
                this.AssignRowDataFromNodeContainer(nodeContainer, rowData, i);
            }
            this.AfterSyncronize();
        }

        private void UpdateFixedRowPosition(RowDataBase rowData)
        {
            if (rowData.UpdateFixedRowPosition())
            {
                this.changed = true;
            }
        }

        protected int SyncronizeStartIndex =>
            0;

        protected ObservableCollection<RowDataBase> Items
        {
            get => 
                this.dataContainer.Items;
            set => 
                this.dataContainer.Items = value;
        }
    }
}

