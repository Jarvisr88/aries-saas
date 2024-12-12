namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Collections.Generic;

    public class DetailSynchronizationQueues : SynchronizationQueues
    {
        private readonly Dictionary<DetailNodeKind, LinkedList<FreeRowDataInfo>> detailRowQueues = new Dictionary<DetailNodeKind, LinkedList<FreeRowDataInfo>>();

        public override void Clear()
        {
            base.Clear();
            foreach (LinkedList<FreeRowDataInfo> list in this.detailRowQueues.Values)
            {
                base.ClearCore(list);
            }
        }

        public LinkedList<FreeRowDataInfo> GetSynchronizationQueue(DetailNodeKind detailNodeKind)
        {
            LinkedList<FreeRowDataInfo> list;
            if (!this.detailRowQueues.TryGetValue(detailNodeKind, out list))
            {
                this.detailRowQueues[detailNodeKind] = list = new LinkedList<FreeRowDataInfo>();
            }
            return list;
        }
    }
}

