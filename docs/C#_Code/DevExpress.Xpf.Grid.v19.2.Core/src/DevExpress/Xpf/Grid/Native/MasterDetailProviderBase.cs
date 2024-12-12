namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.InteropServices;

    public abstract class MasterDetailProviderBase
    {
        protected MasterDetailProviderBase()
        {
        }

        public abstract int CalcDetailRowsCountBeforeRow(int visibleIndex);
        public abstract MasterRowNavigationInfo CalcMasterRowNavigationInfo(int visibleIndex);
        public abstract MasterRowScrollInfo CalcMasterRowScrollInfo(int commonScrollIndex);
        public abstract int CalcTotalLevel(int visibleIndex);
        public abstract int CalcVisibleDetailDataRowCount();
        public abstract int CalcVisibleDetailRowsCount();
        public abstract int CalcVisibleDetailRowsCountBeforeRow(int scrollIndex);
        public abstract int CalcVisibleDetailRowsCountForRow(int rowHandle);
        public abstract void ChangeMasterRowExpanded(int rowHandle);
        public abstract DataControlBase FindDetailDataControl(int rowHandle, DataControlDetailDescriptor descriptor);
        public abstract DataViewBase FindFirstDetailView(int visibleIndex);
        public abstract DataViewBase FindLastInnerDetailView(int visibleIndex);
        public abstract DataViewBase FindTargetView(DataViewBase rootView, object originalSource);
        public abstract bool FindViewAndVisibleIndexByScrollIndex(int scrollIndex, int visibleIndex, bool forwardIfServiceRow, out DataViewBase targetView, out int targetVisibleIndex);
        public abstract DataControlBase FindVisibleDetailDataControl(int rowHandle);
        public abstract DetailDescriptorBase FindVisibleDetailDescriptor(int rowHandle);
        public abstract IEnumerable<DataControlDetailDescriptor> GetDetailDescriptors(DataTreeBuilder treeBuilder, int rowHandle);
        public abstract NodeContainer GetDetailNodeContainer(int rowHandle);
        public abstract RowDetailInfoBase GetReadOnlyRowDetailInfo(int rowHandle);
        public abstract bool HasDataControlDetailDescriptor();
        public abstract void InvalidateDetailScrollInfoCache();
        public abstract bool IsMasterRowExpanded(int rowHandle, DetailDescriptorBase descriptor = null);
        public abstract void OnDetach();
        public abstract bool SetMasterRowExpanded(int rowHandle, bool expand, DetailDescriptorBase descriptor);
        public abstract void SynchronizeDetailTree();
        public abstract void UpdateDetailDataControls(Action<DataControlBase> updateMethod);
        public abstract void UpdateDetailDataControls(Action<DataControlBase> updateOpenDetailMethod, Action<DataControlBase> updateClosedDetailMethod);
        public abstract void UpdateDetailViewIndents(ObservableCollection<DetailIndent> ownerIndents);
        public abstract void UpdateMasterDetailInfo(RowData rowData, bool updateDetailRow);
        public abstract void UpdateOriginationDataControls(Action<DataControlBase> updateMethod);
        public abstract void ValidateMasterDetailConsistency();
    }
}

