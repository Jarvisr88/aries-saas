namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Hierarchy;
    using DevExpress.Xpf.Grid.Native;
    using System;

    public class DetailRowsContainer : DataRowsContainer, IDetailRootItemsContainer, IItemsContainer
    {
        public DetailRowsContainer(DataTreeBuilder treeBuilder, int level) : base(treeBuilder, level)
        {
        }

        internal override void StoreFreeData()
        {
            base.TreeBuilder.ClearRowsCache();
            base.StoreFreeData();
        }

        internal override void Synchronize(NodeContainer nodeContainer)
        {
            base.TreeBuilder.ClearRowsCache();
            base.Synchronize(nodeContainer);
        }

        double IItemsContainer.AnimationProgress =>
            1.0;
    }
}

