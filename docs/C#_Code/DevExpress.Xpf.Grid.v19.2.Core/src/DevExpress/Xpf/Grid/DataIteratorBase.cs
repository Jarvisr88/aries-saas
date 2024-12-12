namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Grid.Native;
    using System;

    public abstract class DataIteratorBase
    {
        protected readonly DataViewBase viewBase;

        public DataIteratorBase(DataViewBase viewBase)
        {
            this.viewBase = viewBase;
        }

        protected internal static DataControllerValuesContainer CreateValuesContainer(DataTreeBuilder treeBuilder, RowHandle rowHandle) => 
            new DataControllerValuesContainer(rowHandle, treeBuilder.GetRowVisibleIndexByHandleCore(rowHandle.Value), treeBuilder.GetRowLevelByControllerRow(rowHandle.Value));

        protected static DataControllerValuesContainer CreateValuesContainer(DataTreeBuilder treeBuilder, int visibleIndex)
        {
            int rowHandleByVisibleIndexCore = treeBuilder.GetRowHandleByVisibleIndexCore(visibleIndex);
            return new DataControllerValuesContainer(new RowHandle(rowHandleByVisibleIndexCore), visibleIndex, treeBuilder.GetRowLevelByControllerRow(rowHandleByVisibleIndexCore));
        }

        protected internal bool GetHasBottom(DataNodeContainer nodeContainer)
        {
            int lastVisibleIndex = (nodeContainer.StartScrollIndex + nodeContainer.Items.Count) - 1;
            return (((lastVisibleIndex + 1) == nodeContainer.TotalVisibleCount) || this.GetHasBottomCore(nodeContainer, lastVisibleIndex));
        }

        protected internal virtual bool GetHasBottomCore(DataNodeContainer nodeContainer, int lastVisibleIndex) => 
            false;

        protected internal virtual bool GetHasTop(DataNodeContainer nodeContainer) => 
            nodeContainer.StartScrollIndex == 0;

        internal DataRowNode GetRowNode(DataTreeBuilder treeBuilder, int startVisibleIndex, DataControllerValuesContainer controllerValues)
        {
            DataRowNode rowNode = treeBuilder.GetRowNode(values => new DataRowNode(treeBuilder, values), controllerValues);
            rowNode.summaryNode = null;
            rowNode.UpdateDetailInfo(startVisibleIndex);
            return rowNode;
        }

        protected internal abstract RowNode GetRowNodeForCurrentLevel(DataNodeContainer nodeContainer, int index, int startVisibleIndex, bool isFixedNode, ref bool shouldBreak);
        protected internal virtual int GetRowParentIndex(DataNodeContainer nodeContainer, int visibleIndex, int level) => 
            this.viewBase.GetRowParentIndex(visibleIndex, level);

        protected internal abstract RowNode GetSummaryNodeForCurrentNode(DataNodeContainer nodeContainer, RowHandle rowHandle, int index);
        protected internal virtual bool IsGroupRowsContainer(DataNodeContainer nodeContainer) => 
            false;
    }
}

