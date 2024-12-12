namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.InteropServices;

    public class NullDetailProvider : MasterDetailProviderBase
    {
        public static NullDetailProvider Instance = new NullDetailProvider();

        private NullDetailProvider()
        {
        }

        public override int CalcDetailRowsCountBeforeRow(int visibleIndex) => 
            0;

        public override MasterRowNavigationInfo CalcMasterRowNavigationInfo(int visibleIndex) => 
            new MasterRowNavigationInfo(visibleIndex, 0, false);

        public override MasterRowScrollInfo CalcMasterRowScrollInfo(int commonScrollIndex) => 
            new MasterRowScrollInfo(commonScrollIndex, 0, true);

        public override int CalcTotalLevel(int visibleIndex) => 
            0;

        public override int CalcVisibleDetailDataRowCount() => 
            0;

        public override int CalcVisibleDetailRowsCount() => 
            0;

        public override int CalcVisibleDetailRowsCountBeforeRow(int scrollIndex) => 
            0;

        public override int CalcVisibleDetailRowsCountForRow(int rowHandle) => 
            0;

        public override void ChangeMasterRowExpanded(int rowHandle)
        {
        }

        public override DataControlBase FindDetailDataControl(int rowHandle, DataControlDetailDescriptor descriptor) => 
            null;

        public override DataViewBase FindFirstDetailView(int visibleIndex) => 
            null;

        public override DataViewBase FindLastInnerDetailView(int visibleIndex) => 
            null;

        public override DataViewBase FindTargetView(DataViewBase rootView, object originalSource) => 
            rootView;

        public override bool FindViewAndVisibleIndexByScrollIndex(int scrollIndex, int visibleIndex, bool forwardIfServiceRow, out DataViewBase targetView, out int targetVisibleIndex)
        {
            targetView = null;
            targetVisibleIndex = -1;
            return false;
        }

        public override DataControlBase FindVisibleDetailDataControl(int rowHandle) => 
            null;

        public override DetailDescriptorBase FindVisibleDetailDescriptor(int rowHandle) => 
            null;

        public override IEnumerable<DataControlDetailDescriptor> GetDetailDescriptors(DataTreeBuilder treeBuilder, int rowHandle) => 
            DetailDescriptorBase.EmptyDetailDescriptors;

        public override NodeContainer GetDetailNodeContainer(int rowHandle) => 
            null;

        public override RowDetailInfoBase GetReadOnlyRowDetailInfo(int rowHandle) => 
            EmptyRowDetailInfo.Instance;

        public override bool HasDataControlDetailDescriptor() => 
            false;

        public override void InvalidateDetailScrollInfoCache()
        {
        }

        public override bool IsMasterRowExpanded(int rowHandle, DetailDescriptorBase descriptor = null) => 
            false;

        public override void OnDetach()
        {
        }

        public override bool SetMasterRowExpanded(int rowHandle, bool expand, DetailDescriptorBase descriptor) => 
            false;

        public override void SynchronizeDetailTree()
        {
        }

        public override void UpdateDetailDataControls(Action<DataControlBase> updateMethod)
        {
        }

        public override void UpdateDetailDataControls(Action<DataControlBase> updateOpenDetailMethod, Action<DataControlBase> updateClosedDetailMethod)
        {
        }

        public override void UpdateDetailViewIndents(ObservableCollection<DetailIndent> ownerIndents)
        {
        }

        public override void UpdateMasterDetailInfo(RowData rowData, bool updateDetailRow)
        {
        }

        public override void UpdateOriginationDataControls(Action<DataControlBase> updateMethod)
        {
        }

        public override void ValidateMasterDetailConsistency()
        {
        }
    }
}

