namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Data;
    using System;
    using System.Collections.Generic;

    public class RowsContainerSyncronizer : RowsContainerSyncronizerBase
    {
        private readonly Queue<RowDataBase> unmatchedRowItems;
        private readonly Queue<RowDataBase> unmatchedGroupRowItems;
        private readonly Queue<RowDataBase> unmatchedGroupSummaryRowItems;
        private readonly Queue<RowDataBase> unmatchedServiceRowItems;

        public RowsContainerSyncronizer(RowsContainer dataContainer) : base(dataContainer)
        {
            this.unmatchedRowItems = new Queue<RowDataBase>();
            this.unmatchedGroupRowItems = new Queue<RowDataBase>();
            this.unmatchedGroupSummaryRowItems = new Queue<RowDataBase>();
            this.unmatchedServiceRowItems = new Queue<RowDataBase>();
        }

        protected override RowDataBase DequeueUnmatchedItem(object matchKey)
        {
            Queue<RowDataBase> queue = this.GetQueue(matchKey);
            return ((queue.Count <= 0) ? null : queue.Dequeue());
        }

        protected override void EnqueueUnmatchedItem(RowDataBase rowData)
        {
            this.GetQueue(rowData.MatchKey).Enqueue(rowData);
        }

        private Queue<RowDataBase> GetQueue(object matchKey)
        {
            if (matchKey is GroupSummaryRowKey)
            {
                return this.unmatchedGroupSummaryRowItems;
            }
            RowHandle handle = (RowHandle) matchKey;
            return ((handle.Value != -2147483646) ? (((handle.Value >= 0) || (handle.Value == -2147483647)) ? this.unmatchedRowItems : this.unmatchedGroupRowItems) : this.unmatchedServiceRowItems);
        }

        protected override IEnumerable<RowDataBase> GetUnmatchedItems()
        {
            List<RowDataBase> list = new List<RowDataBase>();
            list.AddRange(this.unmatchedRowItems);
            list.AddRange(this.unmatchedGroupRowItems);
            list.AddRange(this.unmatchedGroupSummaryRowItems);
            list.AddRange(this.unmatchedServiceRowItems);
            return list;
        }
    }
}

