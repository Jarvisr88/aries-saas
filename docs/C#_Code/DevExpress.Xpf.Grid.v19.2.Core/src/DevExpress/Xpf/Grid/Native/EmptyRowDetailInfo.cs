namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class EmptyRowDetailInfo : RowDetailInfoBase
    {
        public static EmptyRowDetailInfo Instance = new EmptyRowDetailInfo();
        private readonly DetailNodeKind[] emptyDetailNodeKinds = new DetailNodeKind[0];

        private EmptyRowDetailInfo()
        {
        }

        public override int CalcRowsCount() => 
            0;

        public override int CalcTotalLevel() => 
            0;

        public override int CalcVisibleDataRowCount() => 
            0;

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
            null;

        protected internal override DataControlDetailInfo GetActualDetailInfo(DataControlDetailDescriptor detailDescriptor) => 
            null;

        protected internal override int GetBottomServiceRowsCount() => 
            0;

        protected internal override int GetNextLevelRowCount() => 
            0;

        public override NodeContainer GetNodeContainer() => 
            null;

        protected internal override DetailNodeKind[] GetNodeScrollOrder() => 
            this.emptyDetailNodeKinds;

        internal override IEnumerable<RowDetailInfoBase> GetRowDetailInfoEnumerator() => 
            new RowDetailInfoBase[0];

        internal override IEnumerator<RowNode> GetRowNodesEnumeratorCore(int nextLevelStartVisibleIndex, int nextLevelVisibleRowsCount, int serviceVisibleRowsCount) => 
            NodeContainer.EmptyEnumerator;

        public override DevExpress.Xpf.Grid.RowsContainer GetRowsContainerAndUpdateMasterRowData(RowData masterRowData) => 
            null;

        protected internal override int GetTopServiceRowsCount() => 
            0;

        protected internal override int GetVisibleDataRowCount() => 
            0;

        protected internal override void OnCollapsed()
        {
            throw new NotImplementedException();
        }

        protected internal override void OnExpanded()
        {
            throw new NotImplementedException();
        }

        public override void UpdateMasterRowData(RowData masterRowData)
        {
        }

        protected override void UpdateRow(object row)
        {
        }

        public override bool IsExpanded
        {
            get => 
                false;
            set
            {
            }
        }

        internal override RowDetailInfoBase.DetailRowsContainer RowsContainer =>
            null;
    }
}

