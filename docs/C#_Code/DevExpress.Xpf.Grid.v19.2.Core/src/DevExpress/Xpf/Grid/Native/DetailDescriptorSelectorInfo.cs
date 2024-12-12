namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DetailDescriptorSelectorInfo : DetailInfoWithContent
    {
        private BindingValueHelper valueHelper;
        private RowDetailInfoBase activeDetailInfo;

        public DetailDescriptorSelectorInfo(DevExpress.Xpf.Grid.DetailDescriptorSelector detailDescriptor, RowDetailContainer container) : base(detailDescriptor, container)
        {
            this.valueHelper = new BindingValueHelper(new Action<object>(this.OnDetailDescriptorChanged));
            this.valueHelper.ApplyBindings(this.DetailDescriptorSelector, container.Row);
        }

        public override DataViewBase FindFirstDetailView() => 
            this.ActiveDetailInfo.FindFirstDetailView();

        public override DataViewBase FindLastInnerDetailView() => 
            this.ActiveDetailInfo.FindLastInnerDetailView();

        public override bool FindViewAndVisibleIndexByScrollIndex(int scrollIndex, bool forwardIfServiceRow, out DataViewBase targetView, out int targetVisibleIndex) => 
            this.ActiveDetailInfo.FindViewAndVisibleIndexByScrollIndex(scrollIndex, forwardIfServiceRow, out targetView, out targetVisibleIndex);

        public override DataControlBase FindVisibleDetailDataControl() => 
            this.ActiveDetailInfo.FindVisibleDetailDataControl();

        public override DetailDescriptorBase FindVisibleDetailDescriptor() => 
            this.CurrentDetailDescriptor;

        protected internal override DataControlDetailInfo GetActualDetailInfo(DataControlDetailDescriptor detailDescriptor) => 
            this.ActiveDetailInfo.GetActualDetailInfo(detailDescriptor);

        protected internal override int GetBottomServiceRowsCount() => 
            this.ActiveDetailInfo.GetBottomServiceRowsCount();

        protected internal override int GetNextLevelRowCount() => 
            this.ActiveDetailInfo.GetNextLevelRowCount();

        protected internal override DetailNodeKind[] GetNodeScrollOrder() => 
            this.ActiveDetailInfo.GetNodeScrollOrder();

        internal override IEnumerable<RowDetailInfoBase> GetRowDetailInfoEnumerator() => 
            this.ActiveDetailInfo.GetRowDetailInfoEnumerator();

        internal override IEnumerator<RowNode> GetRowNodesEnumeratorCore(int nextLevelStartVisibleIndex, int nextLevelVisibleRowsCount, int serviceVisibleRowsCount) => 
            this.ActiveDetailInfo.GetRowNodesEnumeratorCore(nextLevelStartVisibleIndex, nextLevelVisibleRowsCount, serviceVisibleRowsCount);

        public override RowsContainer GetRowsContainerAndUpdateMasterRowData(RowData masterRowData)
        {
            this.UpdateMasterRowData(masterRowData);
            this.UpdateMasterRowData();
            return this.ActiveDetailInfo.RowsContainer;
        }

        protected internal override int GetTopServiceRowsCount() => 
            this.ActiveDetailInfo.GetTopServiceRowsCount();

        protected internal override int GetVisibleDataRowCount() => 
            this.ActiveDetailInfo.GetVisibleDataRowCount();

        protected internal override void OnCollapsed()
        {
            this.ActiveDetailInfo.IsExpanded = false;
            base.OnCollapsed();
        }

        private void OnDetailDescriptorChanged(object oldDetailDescriptor)
        {
            int startScrollIndex = 0;
            Func<DataPresenterBase, double> evaluator = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<DataPresenterBase, double> local1 = <>c.<>9__10_0;
                evaluator = <>c.<>9__10_0 = x => x.ActualScrollOffset;
            }
            double num2 = base.MasterDataControl.DataView.RootDataPresenter.Return<DataPresenterBase, double>(evaluator, <>c.<>9__10_1 ??= () => 0.0);
            if (base.MasterDataControl.DataView.IsFocusedRowModified && ((base.MasterDataControl.CurrentItem == base.Row) && (base.NodeContainer != null)))
            {
                startScrollIndex = base.NodeContainer.StartScrollIndex;
            }
            this.ActiveDetailInfo = this.CurrentDetailDescriptor.Return<DetailDescriptorBase, DetailInfoWithContent>(x => x.CreateRowDetailInfo(base.container), null);
            int num3 = this.CalcRowsCount();
            if (startScrollIndex > num3)
            {
                base.MasterDataControl.DataView.RootView.LockEditorClose = true;
                base.MasterDataControl.DataView.RootDataPresenter.SetVerticalOffsetForce(((num2 - startScrollIndex) + num3) - 1.0);
                base.MasterDataControl.DataView.RootView.LockEditorClose = false;
            }
        }

        protected internal override void OnExpanded()
        {
            base.OnExpanded();
            this.UpdateMasterRowData();
            this.ActiveDetailInfo.IsExpanded = true;
        }

        public override void SetDetailRowExpanded(bool expand, DetailDescriptorBase descriptor)
        {
            base.SetDetailRowExpanded(expand, descriptor);
            if (this.ActiveDetailInfo != null)
            {
                this.ActiveDetailInfo.SetDetailRowExpanded(expand, descriptor);
            }
        }

        private void UpdateMasterRowData()
        {
            if (this.RowsContainer != null)
            {
                this.ActiveDetailInfo.UpdateMasterRowData(this.RowsContainer.MasterRowData);
            }
        }

        private DetailDescriptorBase CurrentDetailDescriptor =>
            (DetailDescriptorBase) this.valueHelper.Value;

        private DevExpress.Xpf.Grid.DetailDescriptorSelector DetailDescriptorSelector =>
            (DevExpress.Xpf.Grid.DetailDescriptorSelector) base.detailDescriptor;

        private RowDetailInfoBase ActiveDetailInfo
        {
            get => 
                this.activeDetailInfo ?? EmptyRowDetailInfo.Instance;
            set
            {
                if (!ReferenceEquals(this.activeDetailInfo, value))
                {
                    if (this.activeDetailInfo != null)
                    {
                        this.activeDetailInfo.IsExpanded = false;
                        this.activeDetailInfo.Detach();
                    }
                    this.activeDetailInfo = value;
                    if (this.IsExpanded)
                    {
                        this.OnExpanded();
                    }
                    base.detailDescriptor.InvalidateTree();
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DetailDescriptorSelectorInfo.<>c <>9 = new DetailDescriptorSelectorInfo.<>c();
            public static Func<DataPresenterBase, double> <>9__10_0;
            public static Func<double> <>9__10_1;

            internal double <OnDetailDescriptorChanged>b__10_0(DataPresenterBase x) => 
                x.ActualScrollOffset;

            internal double <OnDetailDescriptorChanged>b__10_1() => 
                0.0;
        }
    }
}

