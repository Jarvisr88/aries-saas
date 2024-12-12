namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Grid.Hierarchy;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class SynchronizationQueues
    {
        private readonly Queue<UnsynchronizedNodeInfo> unsynchronizedNodes = new Queue<UnsynchronizedNodeInfo>();
        private readonly LinkedList<FreeRowDataInfo> freeRowDataQueue = new LinkedList<FreeRowDataInfo>();
        private readonly LinkedList<FreeRowDataInfo> freeGroupRowDataQueue = new LinkedList<FreeRowDataInfo>();
        private readonly LinkedList<FreeRowDataInfo> freeGroupSummaryRowDataQueue = new LinkedList<FreeRowDataInfo>();
        private readonly LinkedList<FreeRowDataInfo> freeServiceRowDataQueue = new LinkedList<FreeRowDataInfo>();
        private List<LinkedList<FreeRowDataInfo>> allFreeQueues;

        public virtual void Clear()
        {
            foreach (LinkedList<FreeRowDataInfo> list in this.AllFreeQueues)
            {
                this.ClearCore(list);
            }
        }

        protected void ClearCore(LinkedList<FreeRowDataInfo> list)
        {
            while (list.Count > 0)
            {
                FreeRowDataInfo info = list.First.Value;
                if (info.RowData != null)
                {
                    HierarchyPanel.DetachItem(info.RowData);
                }
                list.RemoveFirst();
            }
        }

        public void ClearFreeRowData()
        {
            Func<LinkedList<FreeRowDataInfo>, IEnumerable<FreeRowDataInfo>> selector = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                Func<LinkedList<FreeRowDataInfo>, IEnumerable<FreeRowDataInfo>> local1 = <>c.<>9__18_0;
                selector = <>c.<>9__18_0 = x => x;
            }
            foreach (FreeRowDataInfo info in this.AllFreeQueues.SelectMany<LinkedList<FreeRowDataInfo>, FreeRowDataInfo>(selector))
            {
                info.RowData.ClearRow();
            }
        }

        public void SynchronizeUnsynchronizedNodes()
        {
            while (this.UnsynchronizedNodes.Count > 0)
            {
                UnsynchronizedNodeInfo info = this.UnsynchronizedNodes.Dequeue();
                RowNode node = info.NodeContainer.Items[info.NodeIndex];
                RowDataBase item = null;
                bool forceUpdate = true;
                LinkedList<FreeRowDataInfo> freeRowDataQueue = node.GetFreeRowDataQueue(this);
                if (freeRowDataQueue.Count > 0)
                {
                    FreeRowDataInfo info2 = freeRowDataQueue.First.Value;
                    item = info2.RowData;
                    if (!ReferenceEquals(info2.DataContainer, info.RowsContainer))
                    {
                        info2.DataContainer.Items.Remove(item);
                    }
                    freeRowDataQueue.RemoveFirst();
                }
                if (item == null)
                {
                    item = node.CreateRowData();
                    forceUpdate = false;
                }
                item.SetVisibleIndex(info.NodeIndex);
                item.AssignVirtualizedRowData(info.RowsContainer, info.NodeContainer, node, forceUpdate);
                item.UpdateFixedRowPosition();
                if (!info.RowsContainer.Items.Contains(item))
                {
                    info.RowsContainer.Items.Add(item);
                }
            }
        }

        public Queue<UnsynchronizedNodeInfo> UnsynchronizedNodes =>
            this.unsynchronizedNodes;

        public LinkedList<FreeRowDataInfo> FreeRowDataQueue =>
            this.freeRowDataQueue;

        public LinkedList<FreeRowDataInfo> FreeGroupRowDataQueue =>
            this.freeGroupRowDataQueue;

        public LinkedList<FreeRowDataInfo> FreeGroupSummaryRowDataQueue =>
            this.freeGroupSummaryRowDataQueue;

        public LinkedList<FreeRowDataInfo> FreeServiceRowDataQueue =>
            this.freeServiceRowDataQueue;

        internal List<LinkedList<FreeRowDataInfo>> AllFreeQueues
        {
            get
            {
                if (this.allFreeQueues == null)
                {
                    List<LinkedList<FreeRowDataInfo>> list1 = new List<LinkedList<FreeRowDataInfo>>();
                    list1.Add(this.FreeRowDataQueue);
                    list1.Add(this.FreeGroupRowDataQueue);
                    list1.Add(this.FreeGroupSummaryRowDataQueue);
                    list1.Add(this.FreeServiceRowDataQueue);
                    this.allFreeQueues = list1;
                }
                return this.allFreeQueues;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SynchronizationQueues.<>c <>9 = new SynchronizationQueues.<>c();
            public static Func<LinkedList<FreeRowDataInfo>, IEnumerable<FreeRowDataInfo>> <>9__18_0;

            internal IEnumerable<FreeRowDataInfo> <ClearFreeRowData>b__18_0(LinkedList<FreeRowDataInfo> x) => 
                x;
        }
    }
}

