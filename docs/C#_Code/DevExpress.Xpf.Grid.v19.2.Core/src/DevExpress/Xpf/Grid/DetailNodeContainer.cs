namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DetailNodeContainer : DataNodeContainer
    {
        public DetailNodeContainer(DataTreeBuilder treeBuilder, int level) : base(treeBuilder, level, null)
        {
        }

        internal void OnMasterRooDataChanged()
        {
            if (base.treeBuilder.View.DataControl != null)
            {
                base.treeBuilder.View.DataControl.InvalidateDetailScrollInfoCache(false);
                base.treeBuilder.View.DataProviderBase.InvalidateVisibleIndicesCache();
                base.treeBuilder.MasterRootNodeContainer.OnDataChanged();
            }
        }

        internal override void ReGenerateExpandItems(int commonStartScrollIndex, int itemsCount)
        {
            this.ReGenerateItems(commonStartScrollIndex, itemsCount, false);
        }

        internal void ReGenerateItems(int commonStartScrollIndex, int itemsCount, bool checkShouldRegenerateItems = true)
        {
            int startScrollIndex = 0;
            int detailStartScrollIndex = 0;
            MasterRowScrollInfo info = base.treeBuilder.View.DataControl.MasterDetailProvider.CalcMasterRowScrollInfo(commonStartScrollIndex);
            if (info != null)
            {
                startScrollIndex = info.StartScrollIndex;
                detailStartScrollIndex = info.DetailStartScrollIndex;
            }
            if (checkShouldRegenerateItems)
            {
                if (!this.ShouldRegenerateItems(detailStartScrollIndex, startScrollIndex))
                {
                    return;
                }
                this.IsScrolling = true;
            }
            base.DetailStartScrollIndex = detailStartScrollIndex;
            base.ReGenerateItemsCore(startScrollIndex, itemsCount);
        }

        internal void ReGenerateMasterRootItems()
        {
            base.treeBuilder.View.DataControl.InvalidateDetailScrollInfoCache(false);
            base.treeBuilder.View.DataProviderBase.InvalidateVisibleIndicesCache();
            base.treeBuilder.MasterRootNodeContainer.ReGenerateItemsCore();
        }

        private bool ShouldRegenerateItems(int detailStartScrollIndex, int startScrollIndex) => 
            (base.DetailStartScrollIndex != detailStartScrollIndex) || ((base.StartScrollIndex != startScrollIndex) || (base.parentVisibleIndex != base.DataIterator.GetRowParentIndex(this, startScrollIndex + this.FixedTopRowsCount, base.GroupLevel)));

        internal bool IsScrolling { get; set; }
    }
}

