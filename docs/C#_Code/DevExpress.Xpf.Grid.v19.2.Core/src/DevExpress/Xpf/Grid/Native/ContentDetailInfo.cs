namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ContentDetailInfo : DetailInfoWithContent
    {
        public ContentDetailInfo(DevExpress.Xpf.Grid.ContentDetailDescriptor contentDetailDescriptor, RowDetailContainer container) : base(contentDetailDescriptor, container)
        {
        }

        public override DataViewBase FindFirstDetailView() => 
            null;

        public override DataViewBase FindLastInnerDetailView() => 
            null;

        public override bool FindViewAndVisibleIndexByScrollIndex(int scrollIndex, bool forwardIfServiceRow, out DataViewBase targetView, out int targetVisibleIndex)
        {
            targetView = null;
            targetVisibleIndex = -1;
            return false;
        }

        public override DataControlBase FindVisibleDetailDataControl() => 
            null;

        public override DetailDescriptorBase FindVisibleDetailDescriptor() => 
            this.ContentDetailDescriptor;

        protected internal override DataControlDetailInfo GetActualDetailInfo(DataControlDetailDescriptor detailDescriptor) => 
            null;

        protected internal override int GetBottomServiceRowsCount() => 
            base.CalcBottomRowCount();

        protected internal override int GetNextLevelRowCount() => 
            0;

        protected internal override DetailNodeKind[] GetNodeScrollOrder() => 
            new DetailNodeKind[] { DetailNodeKind.DetailContent, DetailNodeKind.DetailHeader, DetailNodeKind.TopMarginContainer, DetailNodeKind.BottomMarginContainer };

        [IteratorStateMachine(typeof(<GetRowNodesEnumeratorCore>d__3))]
        internal override IEnumerator<RowNode> GetRowNodesEnumeratorCore(int nextLevelStartVisibleIndex, int nextLevelVisibleRowsCount, int serviceVisibleRowsCount)
        {
            int serviceVisibleRowsCount;
            bool <isBottomMarginVisible>5__1 = this.detailDescriptor.HasBottomMargin && (serviceVisibleRowsCount > 0);
            if (<isBottomMarginVisible>5__1)
            {
                serviceVisibleRowsCount = serviceVisibleRowsCount;
                serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
            }
            if (this.detailDescriptor.HasTopMargin && (serviceVisibleRowsCount > 0))
            {
                serviceVisibleRowsCount = serviceVisibleRowsCount;
                serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                yield return this.GetDetailMarginNode(DetailNodeKind.TopMarginContainer, true);
            }
            bool flag = this.detailDescriptor.ContentTemplate != null;
            if (this.detailDescriptor.ShowHeader && (serviceVisibleRowsCount > 0))
            {
                serviceVisibleRowsCount = serviceVisibleRowsCount;
                serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                DetailInfoWithContent.DetailHeaderRowNode detailHeaderNode = (DetailInfoWithContent.DetailHeaderRowNode) this.GetDetailHeaderNode(true);
                detailHeaderNode.ShowBottomLine = !flag || (serviceVisibleRowsCount == 0);
                yield return detailHeaderNode;
            }
            if (serviceVisibleRowsCount > 0)
            {
                DetailInfoWithContent.DetailHeaderRowNode detailContentNode = (DetailInfoWithContent.DetailHeaderRowNode) this.GetDetailContentNode(true);
                detailContentNode.ShowBottomLine = true;
                yield return detailContentNode;
            }
            while (true)
            {
                if (<isBottomMarginVisible>5__1)
                {
                    serviceVisibleRowsCount = serviceVisibleRowsCount;
                    serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                    yield return this.GetDetailMarginNode(DetailNodeKind.BottomMarginContainer, true);
                }
            }
        }

        protected internal override int GetTopServiceRowsCount() => 
            base.CalcHeaderRowCount() + 1;

        protected internal override int GetVisibleDataRowCount() => 
            0;

        private DevExpress.Xpf.Grid.ContentDetailDescriptor ContentDetailDescriptor =>
            (DevExpress.Xpf.Grid.ContentDetailDescriptor) base.detailDescriptor;

    }
}

