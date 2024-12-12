namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class MasterDetailProvider : MasterDetailProviderBase, IDetailDescriptorOwner
    {
        private readonly TableViewBehavior viewBehavior;
        private DetailScrollInfoCache detailScrollInfoCache;

        public MasterDetailProvider(TableViewBehavior viewBehavior)
        {
            this.viewBehavior = viewBehavior;
            this.DetailDescriptor = this.View.DataControl.DetailDescriptorCore;
            this.ValidateMasterDetailConsistency();
            this.detailScrollInfoCache = new DetailScrollInfoCache(this);
        }

        public override int CalcDetailRowsCountBeforeRow(int visibleIndex) => 
            this.detailScrollInfoCache.CalcVisibleDetailRowsCountBeforeRow(visibleIndex);

        public override MasterRowNavigationInfo CalcMasterRowNavigationInfo(int visibleIndex) => 
            this.detailScrollInfoCache.CalcMasterRowNavigationInfo(visibleIndex);

        public override MasterRowScrollInfo CalcMasterRowScrollInfo(int commonScrollIndex) => 
            this.detailScrollInfoCache.CalcMasterRowScrollInfo(commonScrollIndex);

        public override int CalcTotalLevel(int visibleIndex) => 
            this.GetReadOnlyRowDetailInfo(this.View.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex)).CalcTotalLevel();

        public int CalcVisibleDataRowCountForRow(int rowHandle) => 
            this.GetReadOnlyRowDetailInfo(rowHandle).CalcVisibleDataRowCount();

        public override int CalcVisibleDetailDataRowCount() => 
            this.detailScrollInfoCache.CalcVisibleDetailDataRowCount();

        public override int CalcVisibleDetailRowsCount() => 
            this.detailScrollInfoCache.CalcScrollDetailRowsCount();

        public override int CalcVisibleDetailRowsCountBeforeRow(int scrollIndex) => 
            this.detailScrollInfoCache.CalcScrollDetailRowsCountBeforeRow(scrollIndex);

        public override int CalcVisibleDetailRowsCountForRow(int rowHandle) => 
            this.GetReadOnlyRowDetailInfo(rowHandle).CalcRowsCount();

        public override void ChangeMasterRowExpanded(int rowHandle)
        {
            RowDetailInfoBase rowDetailInfo = this.GetRowDetailInfo(rowHandle);
            if (rowDetailInfo.IsExpanded)
            {
                this.SetMasterRowExpandedCore(rowHandle, rowDetailInfo, false, null);
            }
            else
            {
                this.SetMasterRowExpandedCore(rowHandle, rowDetailInfo, true, null);
            }
        }

        private RowDetailContainer CreateRowDetailContainer(int rowHandle)
        {
            RowDetailContainer container = new RowDetailContainer(this.View.DataControl, this.View.DataProviderBase.GetRowValue(rowHandle));
            container.RootDetailInfo = this.DetailDescriptor.CreateRowDetailInfo(container);
            return container;
        }

        bool IDetailDescriptorOwner.CanAssignTo(DataControlBase dataControl) => 
            ReferenceEquals(dataControl, this.View.DataControl);

        void IDetailDescriptorOwner.EnumerateOwnerDataControls(Action<DataControlBase> action)
        {
            if (this.View.DataControl != null)
            {
                action(this.View.DataControl);
                this.View.DataControl.DataControlOwner.EnumerateOwnerDataControls(action);
            }
        }

        void IDetailDescriptorOwner.EnumerateOwnerDetailDescriptors(Action<DetailDescriptorBase> action)
        {
            this.View.DataControl.DataControlOwner.EnumerateOwnerDetailDescriptors(action);
        }

        void IDetailDescriptorOwner.InvalidateIndents()
        {
            this.View.DataControl.UpdateAllDetailViewIndents();
            Action<DataControlBase> updateMethod = <>c.<>9__47_0;
            if (<>c.<>9__47_0 == null)
            {
                Action<DataControlBase> local1 = <>c.<>9__47_0;
                updateMethod = <>c.<>9__47_0 = x => x.DataView.UpdateColumnsPositions();
            }
            this.View.DataControl.GetRootDataControl().UpdateAllDetailAndOriginationDataControls(updateMethod);
        }

        void IDetailDescriptorOwner.InvalidateTree()
        {
            Action<DataControlBase> action = <>c.<>9__44_0;
            if (<>c.<>9__44_0 == null)
            {
                Action<DataControlBase> local1 = <>c.<>9__44_0;
                action = <>c.<>9__44_0 = delegate (DataControlBase dataControl) {
                    Func<DataControlBase, DataControlBase> getTarget = <>c.<>9__44_1;
                    if (<>c.<>9__44_1 == null)
                    {
                        Func<DataControlBase, DataControlBase> local1 = <>c.<>9__44_1;
                        getTarget = <>c.<>9__44_1 = dc => dc;
                    }
                    DataControlOriginationElementHelper.EnumerateDependentElementsIncludingSource<DataControlBase>(dataControl, getTarget, <>c.<>9__44_2 ??= dc => dc.MasterDetailProvider.InvalidateDetailScrollInfoCache(), null);
                };
            }
            this.View.DataControl.EnumerateThisAndOwnerDataControls(action);
            this.View.RootView.OnDataReset();
        }

        public override DataControlBase FindDetailDataControl(int rowHandle, DataControlDetailDescriptor descriptor)
        {
            RowDetailInfoBase readOnlyRowDetailInfo = this.GetReadOnlyRowDetailInfo(rowHandle);
            return (readOnlyRowDetailInfo.IsExpanded ? readOnlyRowDetailInfo.FindDetailDataControl(descriptor) : null);
        }

        public override DataViewBase FindFirstDetailView(int visibleIndex)
        {
            int rowHandleByVisibleIndexCore = this.View.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex);
            RowDetailInfoBase readOnlyRowDetailInfo = this.GetReadOnlyRowDetailInfo(rowHandleByVisibleIndexCore);
            return (readOnlyRowDetailInfo.IsExpanded ? readOnlyRowDetailInfo.FindFirstDetailView() : null);
        }

        public override DataViewBase FindLastInnerDetailView(int visibleIndex)
        {
            int rowHandleByVisibleIndexCore = this.View.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex);
            RowDetailInfoBase readOnlyRowDetailInfo = this.GetReadOnlyRowDetailInfo(rowHandleByVisibleIndexCore);
            return (readOnlyRowDetailInfo.IsExpanded ? readOnlyRowDetailInfo.FindLastInnerDetailView() : null);
        }

        public override DataViewBase FindTargetView(DataViewBase rootView, object originalSource)
        {
            RowData data = RowData.FindRowData((DependencyObject) originalSource);
            return ((data != null) ? data.View : rootView);
        }

        public override bool FindViewAndVisibleIndexByScrollIndex(int scrollIndex, int visibleIndex, bool forwardIfServiceRow, out DataViewBase targetView, out int targetVisibleIndex)
        {
            int rowHandleByVisibleIndexCore = this.View.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex);
            RowDetailInfoBase readOnlyRowDetailInfo = this.GetReadOnlyRowDetailInfo(rowHandleByVisibleIndexCore);
            if (readOnlyRowDetailInfo.IsExpanded)
            {
                return readOnlyRowDetailInfo.FindViewAndVisibleIndexByScrollIndex(scrollIndex, forwardIfServiceRow, out targetView, out targetVisibleIndex);
            }
            targetView = null;
            targetVisibleIndex = -1;
            return false;
        }

        public override DataControlBase FindVisibleDetailDataControl(int rowHandle)
        {
            RowDetailInfoBase readOnlyRowDetailInfo = this.GetReadOnlyRowDetailInfo(rowHandle);
            return (readOnlyRowDetailInfo.IsExpanded ? readOnlyRowDetailInfo.FindVisibleDetailDataControl() : null);
        }

        public override DetailDescriptorBase FindVisibleDetailDescriptor(int rowHandle)
        {
            RowDetailInfoBase readOnlyRowDetailInfo = this.GetReadOnlyRowDetailInfo(rowHandle);
            return (readOnlyRowDetailInfo.IsExpanded ? readOnlyRowDetailInfo.FindVisibleDetailDescriptor() : null);
        }

        public override IEnumerable<DataControlDetailDescriptor> GetDetailDescriptors(DataTreeBuilder treeBuilder, int rowHandle) => 
            this.DetailDescriptor.GetDetailDescriptors(treeBuilder, rowHandle);

        public override NodeContainer GetDetailNodeContainer(int rowHandle) => 
            this.GetReadOnlyRowDetailInfo(rowHandle).GetNodeContainer();

        private Thickness GetGroupDetailMargin(Thickness actualMargin) => 
            new Thickness((this.View.DataControl.ActualGroupCountCore * this.viewBehavior.TableView.LeftGroupAreaIndent) + actualMargin.Left, actualMargin.Top, actualMargin.Right, actualMargin.Bottom);

        public override RowDetailInfoBase GetReadOnlyRowDetailInfo(int rowHandle) => 
            this.View.AllowMasterDetailCore ? (this.GetRowDetailInfoCore(rowHandle, false) ?? EmptyRowDetailInfo.Instance) : EmptyRowDetailInfo.Instance;

        private DataControlBase GetRootDataControl(DataControlBase dataControl) => 
            this.View.DataControl.GetRootDataControl();

        internal RowDetailInfoBase GetRowDetailInfo(int rowHandle) => 
            this.GetRowDetailInfoCore(rowHandle, true);

        internal RowDetailInfoBase GetRowDetailInfoCore(int rowHandle, bool createNewIfNotExist)
        {
            RowDetailContainer container = this.View.DataProviderBase.GetRowDetailContainer(rowHandle, () => this.CreateRowDetailContainer(rowHandle), createNewIfNotExist);
            return container?.RootDetailInfo;
        }

        internal RowDetailInfoBase GetRowDetailInfoForPrinting(int rowHandle) => 
            this.GetRowDetailInfoCore(rowHandle, false) ?? this.CreateRowDetailContainer(rowHandle).RootDetailInfo;

        public override bool HasDataControlDetailDescriptor() => 
            this.DetailDescriptor.HasDataControlDetailDescriptor();

        public override void InvalidateDetailScrollInfoCache()
        {
            this.detailScrollInfoCache.InvalidateCache();
        }

        public override bool IsMasterRowExpanded(int rowHandle, DetailDescriptorBase descriptor = null) => 
            this.GetReadOnlyRowDetailInfo(rowHandle).IsDetailRowExpanded(descriptor);

        public override void OnDetach()
        {
            this.View.DataProviderBase.ClearDetailInfo();
            this.DetailDescriptor.OnDetach();
        }

        public override bool SetMasterRowExpanded(int rowHandle, bool expand, DetailDescriptorBase descriptor)
        {
            RowDetailInfoBase rowDetailInfoCore = this.GetRowDetailInfoCore(rowHandle, expand);
            if (rowDetailInfoCore == null)
            {
                return false;
            }
            this.SetMasterRowExpandedCore(rowHandle, rowDetailInfoCore, expand, descriptor);
            return true;
        }

        protected virtual void SetMasterRowExpandedCore(int rowHandle, RowDetailInfoBase detailInfo, bool expand, DetailDescriptorBase descriptor)
        {
            if (this.View.CommitEditing() && this.View.DataControl.RaiseMasterRowExpandStateChanging(rowHandle, detailInfo.IsExpanded))
            {
                this.View.DataControl.InvalidateDetailScrollInfoCache(false);
                detailInfo.SetDetailRowExpanded(expand, descriptor);
                this.View.OnDataReset();
                this.View.DataControl.RaiseMasterRowExpandStateChanged(rowHandle, detailInfo.IsExpanded);
            }
        }

        public override void SynchronizeDetailTree()
        {
            this.DetailDescriptor.SynchronizeDetailTree();
        }

        public override void UpdateDetailDataControls(Action<DataControlBase> updateMethod)
        {
            this.DetailDescriptor.UpdateDetailDataControls(updateMethod, null);
        }

        public override void UpdateDetailDataControls(Action<DataControlBase> updateOpenDetailMethod, Action<DataControlBase> updateClosedDetailMethod)
        {
            this.DetailDescriptor.UpdateDetailDataControls(updateOpenDetailMethod, updateClosedDetailMethod);
        }

        public override void UpdateDetailViewIndents(ObservableCollection<DetailIndent> ownerIndents)
        {
            this.DetailDescriptor.UpdateDetailViewIndents(ownerIndents, this.GetGroupDetailMargin(this.viewBehavior.TableView.ActualDetailMargin));
        }

        public override void UpdateMasterDetailInfo(RowData rowData, bool updateDetailRow)
        {
            int rowHandle = rowData.RowHandle.Value;
            if (updateDetailRow)
            {
                this.GetReadOnlyRowDetailInfo(rowHandle).OnUpdateRow(rowData.Row);
            }
            rowData.IsRowExpanded = this.IsMasterRowExpanded(rowHandle, null);
            rowData.RowsContainer = this.GetReadOnlyRowDetailInfo(rowHandle).GetRowsContainerAndUpdateMasterRowData(rowData);
        }

        public override void UpdateOriginationDataControls(Action<DataControlBase> updateMethod)
        {
            this.DetailDescriptor.UpdateOriginationDataControls(updateMethod);
        }

        public override void ValidateMasterDetailConsistency()
        {
            this.View.DataProviderBase.ThrowNotSupportedExceptionIfInServerMode();
            this.View.ThrowNotSupportedInMasterDetailException();
            this.View.DataControl.ThrowNotSupportedInMasterDetailException();
        }

        internal DataViewBase View =>
            this.viewBehavior.View;

        private DetailDescriptorBase DetailDescriptor { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MasterDetailProvider.<>c <>9 = new MasterDetailProvider.<>c();
            public static Func<DataControlBase, DataControlBase> <>9__44_1;
            public static Action<DataControlBase> <>9__44_2;
            public static Action<DataControlBase> <>9__44_0;
            public static Action<DataControlBase> <>9__47_0;

            internal void <DevExpress.Xpf.Grid.Native.IDetailDescriptorOwner.InvalidateIndents>b__47_0(DataControlBase x)
            {
                x.DataView.UpdateColumnsPositions();
            }

            internal void <DevExpress.Xpf.Grid.Native.IDetailDescriptorOwner.InvalidateTree>b__44_0(DataControlBase dataControl)
            {
                Func<DataControlBase, DataControlBase> getTarget = <>9__44_1;
                if (<>9__44_1 == null)
                {
                    Func<DataControlBase, DataControlBase> local1 = <>9__44_1;
                    getTarget = <>9__44_1 = dc => dc;
                }
                DataControlOriginationElementHelper.EnumerateDependentElementsIncludingSource<DataControlBase>(dataControl, getTarget, <>9__44_2 ??= dc => dc.MasterDetailProvider.InvalidateDetailScrollInfoCache(), null);
            }

            internal DataControlBase <DevExpress.Xpf.Grid.Native.IDetailDescriptorOwner.InvalidateTree>b__44_1(DataControlBase dc) => 
                dc;

            internal void <DevExpress.Xpf.Grid.Native.IDetailDescriptorOwner.InvalidateTree>b__44_2(DataControlBase dc)
            {
                dc.MasterDetailProvider.InvalidateDetailScrollInfoCache();
            }
        }
    }
}

