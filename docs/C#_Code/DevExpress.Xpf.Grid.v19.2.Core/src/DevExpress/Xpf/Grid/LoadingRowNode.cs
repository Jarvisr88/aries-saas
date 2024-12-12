namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;

    public class LoadingRowNode : DataRowNode
    {
        public LoadingRowNode(DataTreeBuilder treeBuilder, DataControllerValuesContainer controllerValues) : base(treeBuilder, controllerValues)
        {
        }

        internal override RowDataBase CreateRowData() => 
            new LoadingRowData(base.treeBuilder);

        internal override LinkedList<FreeRowDataInfo> GetFreeRowDataQueue(SynchronizationQueues synchronizationQueues) => 
            synchronizationQueues.FreeServiceRowDataQueue;
    }
}

