namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    internal class IndexSynchronizer
    {
        private Queue<BandColumnsMoveAdapter> syncQueue = new Queue<BandColumnsMoveAdapter>();

        protected virtual void BeforeProcess()
        {
        }

        public void EnqueueSyncRequest(BandColumnsMoveAdapter adapter)
        {
            if ((adapter != null) && this.IsSyncNeeded(adapter))
            {
                this.syncQueue.Enqueue(adapter);
            }
        }

        private IList<BaseColumn> GetColumns(BandColumnsMoveAdapter adapter)
        {
            if (adapter == null)
            {
                return null;
            }
            ColumnNodeCollection columnNodes = adapter.ColumnNodes;
            if ((columnNodes == null) || (columnNodes.Owner == null))
            {
                return null;
            }
            return columnNodes.Owner.GetRoot()?.FindColumns(columnNodes);
        }

        public ReadOnlyCollection<BandColumnsMoveAdapter> GetUnsyncedAdapters() => 
            new ReadOnlyCollection<BandColumnsMoveAdapter>(this.syncQueue.ToList<BandColumnsMoveAdapter>());

        protected virtual bool IsSyncNeeded(BandColumnsMoveAdapter adapter) => 
            !adapter.IndexedMode;

        public void Process()
        {
            this.BeforeProcess();
            this.ProcessCore();
        }

        private void ProcessCore()
        {
            while (this.syncQueue.Count != 0)
            {
                BandColumnsMoveAdapter adapter = this.syncQueue.Dequeue();
                this.SyncCurrentValueIndexes(this.GetColumns(adapter));
            }
        }

        private void SyncCurrentValueIndexes(IList<BaseColumn> columns)
        {
            if (columns != null)
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    columns[i].VisibleIndex = i;
                }
            }
        }
    }
}

