namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class DataControlDetailInfo : DetailInfoWithContent, IDataControlParent
    {
        private DataControlBase dataControl;

        public DataControlDetailInfo(DevExpress.Xpf.Grid.DataControlDetailDescriptor detailDescriptor, RowDetailContainer container) : base(detailDescriptor, container)
        {
        }

        public override void Clear()
        {
            base.Clear();
            this.ClearGrid();
        }

        private void ClearGrid()
        {
            if (this.DataControl != null)
            {
                this.DataControl.DataView.DataControlMenu.Destroy();
                this.DataControl.UnselectAll();
                this.DataControlDetailDescriptor.DataControl.DetailClones.Remove(this.dataControl);
            }
        }

        public void CloneDetail()
        {
            if (this.dataControl == null)
            {
                this.UpdateDataControl(true);
            }
            this.DataControlDetailDescriptor.PopulateColumnsIfNeeded(this.dataControl.DataProviderBase);
            this.DataControlDetailDescriptor.DataControl.CopyToDetail(this.dataControl);
            this.dataControl.DataView.UpdateContentLayout();
            if (this.dataControl.DataView.RootDataPresenter != null)
            {
                this.dataControl.DataView.RootDataPresenter.ScrollInfoCore.SecondarySizeScrollInfo.Invalidate();
            }
        }

        internal override DetailInfoWithContent.DetailRowNode CreateDetailRowNode(DetailNodeKind detailNodeKind, Func<DetailNodeKind, RowDataBase> createRowDataDelegate)
        {
            if (detailNodeKind > DetailNodeKind.DetailContent)
            {
                if ((detailNodeKind != DetailNodeKind.TopMarginContainer) && (detailNodeKind != DetailNodeKind.BottomMarginContainer))
                {
                    goto TR_0000;
                }
            }
            else if ((detailNodeKind != DetailNodeKind.DetailHeader) && (detailNodeKind != DetailNodeKind.DetailContent))
            {
                goto TR_0000;
            }
            return base.CreateDetailRowNode(detailNodeKind, createRowDataDelegate);
        TR_0000:
            return new DetailViewRowNode(this, this.DataControl.DataView, detailNodeKind, createRowDataDelegate);
        }

        public override void Detach()
        {
            base.Detach();
            this.ClearGrid();
        }

        int IDataControlParent.CalcTotalLevel() => 
            (base.MasterDataControl.CalcTotalLevel(base.MasterVisibleIndex) + base.MasterDataControl.MasterDetailProvider.CalcTotalLevel(base.MasterVisibleIndex)) + 1;

        void IDataControlParent.CollectParentFixedRowsScrollIndexes(List<int> scrollIndexes)
        {
            int item = base.MasterDataControl.DataView.ConvertVisibleIndexToScrollIndex(base.MasterVisibleIndex);
            for (int i = this.GetTopServiceRowsCount(); i >= 1; i--)
            {
                scrollIndexes.Add(item - i);
            }
            scrollIndexes.Add(item);
            base.MasterDataControl.CollectParentFixedRowsScrollIndexes(base.MasterVisibleIndex, scrollIndexes);
        }

        void IDataControlParent.CollectViewVisibleIndexChain(List<KeyValuePair<DataViewBase, int>> chain)
        {
            chain.Insert(0, new KeyValuePair<DataViewBase, int>(base.MasterDataControl.DataView, base.MasterVisibleIndex));
            base.MasterDataControl.CollectViewVisibleIndexChain(chain);
        }

        void IDataControlParent.EnumerateParentDataControls(Action<DataControlBase, int> action)
        {
            action(base.MasterDataControl, base.MasterVisibleIndex);
            base.MasterDataControl.DataControlParent.EnumerateParentDataControls(action);
        }

        bool IDataControlParent.FindMasterRow(out DataViewBase targetView, out int targetVisibleIndex)
        {
            targetView = base.MasterDataControl.DataView;
            targetVisibleIndex = base.MasterVisibleIndex;
            return true;
        }

        DataViewBase IDataControlParent.FindMasterView() => 
            base.MasterDataControl.DataView;

        bool IDataControlParent.FindNextOuterMasterRow(out DataViewBase targetView, out int targetVisibleIndex) => 
            base.MasterDataControl.FindNextOuterMasterRow(base.MasterVisibleIndex, out targetView, out targetVisibleIndex);

        IEnumerable<ColumnsRowDataBase> IDataControlParent.GetColumnsRowDataEnumerator()
        {
            Func<RowDataBase, bool> predicate = <>c.<>9__29_0;
            if (<>c.<>9__29_0 == null)
            {
                Func<RowDataBase, bool> local1 = <>c.<>9__29_0;
                predicate = <>c.<>9__29_0 = rowData => rowData is ColumnsRowDataBase;
            }
            return this.RowsContainer.Items.Where<RowDataBase>(predicate).Cast<ColumnsRowDataBase>();
        }

        ColumnsRowDataBase IDataControlParent.GetHeadersRowData() => 
            (ColumnsRowDataBase) this.GetRowDataByKind(DetailNodeKind.ColumnHeaders);

        ColumnsRowDataBase IDataControlParent.GetNewItemRowData() => 
            (ColumnsRowDataBase) this.GetRowDataByKind(DetailNodeKind.NewItemRow);

        void IDataControlParent.InvalidateTree()
        {
            this.DataControlDetailDescriptor.InvalidateTree();
        }

        void IDataControlParent.ValidateMasterDetailConsistency(DataControlBase dataControl)
        {
            dataControl.DataProviderBase.ThrowNotSupportedExceptionIfInServerMode();
        }

        public override DataViewBase FindFirstDetailView() => 
            ((this.DataControl.VisibleRowCount > 0) || this.DataControl.DataView.IsNewItemRowVisible) ? this.DataControl.DataView : null;

        public override DataViewBase FindLastInnerDetailView() => 
            this.DataControl.FindLastInnerDetailView();

        public override bool FindViewAndVisibleIndexByScrollIndex(int scrollIndex, bool forwardIfServiceRow, out DataViewBase targetView, out int targetVisibleIndex)
        {
            int num = this.DataControl.VisibleRowCount + this.DataControl.MasterDetailProvider.CalcVisibleDetailRowsCount();
            if (scrollIndex < num)
            {
                return this.DataControl.FindViewAndVisibleIndexByScrollIndexCore(scrollIndex, forwardIfServiceRow, out targetView, out targetVisibleIndex);
            }
            if (scrollIndex >= (num + this.GetBottomServiceRowsCount()))
            {
                if (!forwardIfServiceRow)
                {
                    if (((IDataControlParent) this).FindMasterRow(out targetView, out targetVisibleIndex))
                    {
                        return true;
                    }
                }
                else
                {
                    targetView = this.FindFirstDetailView();
                    if (targetView != null)
                    {
                        targetVisibleIndex = 0;
                        return true;
                    }
                }
            }
            else if (forwardIfServiceRow)
            {
                if (((IDataControlParent) this).FindNextOuterMasterRow(out targetView, out targetVisibleIndex))
                {
                    return true;
                }
            }
            else
            {
                targetView = this.FindLastInnerDetailView();
                if (targetView != null)
                {
                    targetVisibleIndex = targetView.DataControl.VisibleRowCount - 1;
                    return true;
                }
            }
            targetView = null;
            targetVisibleIndex = -1;
            return false;
        }

        public override DataControlBase FindVisibleDetailDataControl() => 
            this.DataControl;

        public override DetailDescriptorBase FindVisibleDetailDescriptor() => 
            this.DataControlDetailDescriptor;

        protected internal override DataControlDetailInfo GetActualDetailInfo(DevExpress.Xpf.Grid.DataControlDetailDescriptor detailDescriptor) => 
            this;

        protected internal override int GetBottomServiceRowsCount()
        {
            int num = base.CalcBottomRowCount();
            if (this.DataControl != null)
            {
                if (this.DataControl.DataView.ShowTotalSummary)
                {
                    num++;
                }
                if (this.DataControl.DataView.ShowFixedTotalSummary)
                {
                    num++;
                }
            }
            return num;
        }

        protected internal override int GetNextLevelRowCount() => 
            (this.DataControl != null) ? ((this.DataControl.VisibleRowCount + this.DataControl.viewCore.CalcGroupSummaryVisibleRowCount()) + this.DataControl.MasterDetailProvider.CalcVisibleDetailRowsCount()) : 0;

        protected internal override DetailNodeKind[] GetNodeScrollOrder() => 
            new DetailNodeKind[] { DetailNodeKind.DataRowsContainer, DetailNodeKind.NewItemRow, DetailNodeKind.TotalSummary, DetailNodeKind.FixedTotalSummary, DetailNodeKind.ColumnHeaders, DetailNodeKind.DetailContent, DetailNodeKind.DetailHeader, DetailNodeKind.TopMarginContainer, DetailNodeKind.BottomMarginContainer };

        private RowDataBase GetRowDataByKind(DetailNodeKind kind) => 
            this.RowsContainer.Items.FirstOrDefault<RowDataBase>(rowData => Equals(rowData.MatchKey, kind));

        [IteratorStateMachine(typeof(<GetRowNodesEnumeratorCore>d__26))]
        internal override IEnumerator<RowNode> GetRowNodesEnumeratorCore(int nextLevelStartVisibleIndex, int nextLevelVisibleRowsCount, int serviceVisibleRowsCount)
        {
            <GetRowNodesEnumeratorCore>d__26 d__1 = new <GetRowNodesEnumeratorCore>d__26(0);
            d__1.<>4__this = this;
            d__1.nextLevelStartVisibleIndex = nextLevelStartVisibleIndex;
            d__1.nextLevelVisibleRowsCount = nextLevelVisibleRowsCount;
            d__1.serviceVisibleRowsCount = serviceVisibleRowsCount;
            return d__1;
        }

        protected internal override int GetTopServiceRowsCount()
        {
            int num = base.CalcHeaderRowCount() + base.CalcContentRowCount();
            if (this.DataControl != null)
            {
                if (this.DataControl.DataView.CanShowDetailColumnHeadersControl)
                {
                    num++;
                }
                if (this.DataControl.DataView.IsNewItemRowVisible)
                {
                    num++;
                }
            }
            return num;
        }

        protected internal override int GetVisibleDataRowCount()
        {
            if (this.DataControl == null)
            {
                return 0;
            }
            int num = this.DataControl.VisibleRowCount + this.DataControl.MasterDetailProvider.CalcVisibleDetailDataRowCount();
            if (this.DataControl.DataView.IsNewItemRowVisible)
            {
                num++;
            }
            return num;
        }

        internal override bool IsFixedNode(DetailInfoWithContent.DetailRowNode detailRowNode) => 
            false;

        protected internal override void OnCollapsed()
        {
            base.OnCollapsed();
        }

        protected internal override void OnExpanded()
        {
            this.CloneDetail();
        }

        internal override void RemoveFromDetailClones()
        {
            if (this.DataControl != null)
            {
                this.DataControlDetailDescriptor.DataControl.DetailClones.Remove(this.DataControl);
            }
        }

        private T SafeGetViewProperty<T>(Func<DataViewBase, T> getPropertyDelegate) where T: class
        {
            if ((this.dataControl != null) && (this.dataControl.DataView != null))
            {
                return getPropertyDelegate(this.dataControl.DataView);
            }
            return default(T);
        }

        internal void UpdateDataControl(bool addToClones)
        {
            this.DataControlDetailDescriptor.DataControl.CloneDetail(base.MasterDataControl.DataView.MasterRootNodeContainer, base.MasterRootRowsContainer, base.Row, this.DataControlDetailDescriptor.GetItemsSourceBinding(), this, !this.DataControlDetailDescriptor.DataControl.CanAutoPopulateColumns, addToClones);
        }

        protected override void UpdateRow(object row)
        {
            base.UpdateRow(row);
            if (this.DataControl != null)
            {
                this.DataControl.DataContext = row;
            }
        }

        internal DataControlBase DataControl
        {
            get => 
                this.dataControl;
            set => 
                this.dataControl = value;
        }

        internal DevExpress.Xpf.Grid.DataControlDetailDescriptor DataControlDetailDescriptor =>
            (DevExpress.Xpf.Grid.DataControlDetailDescriptor) base.detailDescriptor;

        private DevExpress.Xpf.Grid.DetailNodeContainer DetailRootNodeContainer
        {
            get
            {
                Func<DataViewBase, DevExpress.Xpf.Grid.DetailNodeContainer> getPropertyDelegate = <>c.<>9__9_0;
                if (<>c.<>9__9_0 == null)
                {
                    Func<DataViewBase, DevExpress.Xpf.Grid.DetailNodeContainer> local1 = <>c.<>9__9_0;
                    getPropertyDelegate = <>c.<>9__9_0 = view => view.RootNodeContainer;
                }
                return this.SafeGetViewProperty<DevExpress.Xpf.Grid.DetailNodeContainer>(getPropertyDelegate);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataControlDetailInfo.<>c <>9 = new DataControlDetailInfo.<>c();
            public static Func<DataViewBase, DevExpress.Xpf.Grid.DetailNodeContainer> <>9__9_0;
            public static Func<DataViewBase, DataTreeBuilder> <>9__26_1;
            public static Func<DataViewBase, DataTreeBuilder> <>9__26_3;
            public static Func<FrameworkElement> <>9__26_5;
            public static Func<DetailNodeKind, RowDataBase> <>9__26_4;
            public static Func<DataViewBase, RowsContainer> <>9__26_6;
            public static Func<DataViewBase, DataTreeBuilder> <>9__26_8;
            public static Func<RowDataBase, bool> <>9__29_0;

            internal bool <DevExpress.Xpf.Grid.Native.IDataControlParent.GetColumnsRowDataEnumerator>b__29_0(RowDataBase rowData) => 
                rowData is ColumnsRowDataBase;

            internal DevExpress.Xpf.Grid.DetailNodeContainer <get_DetailRootNodeContainer>b__9_0(DataViewBase view) => 
                view.RootNodeContainer;

            internal DataTreeBuilder <GetRowNodesEnumeratorCore>b__26_1(DataViewBase view) => 
                view.VisualDataTreeBuilder;

            internal DataTreeBuilder <GetRowNodesEnumeratorCore>b__26_3(DataViewBase view) => 
                view.VisualDataTreeBuilder;

            internal RowDataBase <GetRowNodesEnumeratorCore>b__26_4(DetailNodeKind nodeKind)
            {
                Func<FrameworkElement> createRowElementDelegate = <>9__26_5;
                if (<>9__26_5 == null)
                {
                    Func<FrameworkElement> local1 = <>9__26_5;
                    createRowElementDelegate = <>9__26_5 = () => new EmptyDetailRowControl();
                }
                return new DetailInfoWithContent.DetailRowData(nodeKind, createRowElementDelegate);
            }

            internal FrameworkElement <GetRowNodesEnumeratorCore>b__26_5() => 
                new EmptyDetailRowControl();

            internal RowsContainer <GetRowNodesEnumeratorCore>b__26_6(DataViewBase view) => 
                view.RootRowsContainer;

            internal DataTreeBuilder <GetRowNodesEnumeratorCore>b__26_8(DataViewBase view) => 
                view.VisualDataTreeBuilder;
        }

        [CompilerGenerated]
        private sealed class <GetRowNodesEnumeratorCore>d__26 : IEnumerator<RowNode>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private RowNode <>2__current;
            public DataControlDetailInfo <>4__this;
            public int serviceVisibleRowsCount;
            private bool <hasContentRow>5__1;
            public int nextLevelVisibleRowsCount;
            public int nextLevelStartVisibleIndex;
            private bool <isTotalSummaryVisible>5__2;
            private bool <isFixedTotalSummaryRowVisible>5__3;
            private bool <isBottomMarginVisible>5__4;

            [DebuggerHidden]
            public <GetRowNodesEnumeratorCore>d__26(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int serviceVisibleRowsCount;
                switch (this.<>1__state)
                {
                    case 0:
                        this.<>1__state = -1;
                        this.<isBottomMarginVisible>5__4 = this.<>4__this.detailDescriptor.HasBottomMargin && (this.serviceVisibleRowsCount > 0);
                        if (this.<isBottomMarginVisible>5__4)
                        {
                            serviceVisibleRowsCount = this.serviceVisibleRowsCount;
                            this.serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                        }
                        if (!this.<>4__this.detailDescriptor.HasTopMargin || (this.serviceVisibleRowsCount <= 0))
                        {
                            break;
                        }
                        serviceVisibleRowsCount = this.serviceVisibleRowsCount;
                        this.serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                        this.<>2__current = this.<>4__this.GetDetailMarginNode(DetailNodeKind.TopMarginContainer, true);
                        this.<>1__state = 1;
                        return true;

                    case 1:
                        this.<>1__state = -1;
                        break;

                    case 2:
                        this.<>1__state = -1;
                        goto TR_001D;

                    case 3:
                        this.<>1__state = -1;
                        goto TR_001C;

                    case 4:
                        this.<>1__state = -1;
                        goto TR_001A;

                    case 5:
                        this.<>1__state = -1;
                        goto TR_0011;

                    case 6:
                        this.<>1__state = -1;
                        goto TR_0010;

                    case 7:
                        this.<>1__state = -1;
                        goto TR_000F;

                    case 8:
                        this.<>1__state = -1;
                        goto TR_000E;

                    case 9:
                        this.<>1__state = -1;
                        goto TR_000D;

                    default:
                        return false;
                }
                this.<hasContentRow>5__1 = this.<>4__this.detailDescriptor.ContentTemplate != null;
                if (this.<>4__this.DataControlDetailDescriptor.ShowHeader && (this.serviceVisibleRowsCount > 0))
                {
                    serviceVisibleRowsCount = this.serviceVisibleRowsCount;
                    this.serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                    DetailInfoWithContent.DetailHeaderRowNode detailHeaderNode = (DetailInfoWithContent.DetailHeaderRowNode) this.<>4__this.GetDetailHeaderNode(true);
                    detailHeaderNode.ShowBottomLine = !this.<hasContentRow>5__1;
                    this.<>2__current = detailHeaderNode;
                    this.<>1__state = 2;
                    return true;
                }
                goto TR_001D;
            TR_000D:
                return false;
            TR_000E:
                if (this.<isBottomMarginVisible>5__4)
                {
                    serviceVisibleRowsCount = this.serviceVisibleRowsCount;
                    this.serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                    this.<>2__current = this.<>4__this.GetDetailMarginNode(DetailNodeKind.BottomMarginContainer, true);
                    this.<>1__state = 9;
                    return true;
                }
                goto TR_000D;
            TR_000F:
                if (this.<isFixedTotalSummaryRowVisible>5__3)
                {
                    this.<>2__current = this.<>4__this.GetRowNode(DetailNodeKind.FixedTotalSummary, new Func<DetailNodeKind, RowDataBase>(this.<>4__this.<GetRowNodesEnumeratorCore>b__26_9));
                    this.<>1__state = 8;
                    return true;
                }
                goto TR_000E;
            TR_0010:
                if (this.<isTotalSummaryVisible>5__2)
                {
                    this.<>2__current = this.<>4__this.GetRowNode(DetailNodeKind.TotalSummary, new Func<DetailNodeKind, RowDataBase>(this.<>4__this.<GetRowNodesEnumeratorCore>b__26_7));
                    this.<>1__state = 7;
                    return true;
                }
                goto TR_000F;
            TR_0011:
                if (this.nextLevelVisibleRowsCount > 0)
                {
                    Func<DetailNodeKind, RowDataBase> createRowDataDelegate = DataControlDetailInfo.<>c.<>9__26_4;
                    if (DataControlDetailInfo.<>c.<>9__26_4 == null)
                    {
                        Func<DetailNodeKind, RowDataBase> local1 = DataControlDetailInfo.<>c.<>9__26_4;
                        createRowDataDelegate = DataControlDetailInfo.<>c.<>9__26_4 = new Func<DetailNodeKind, RowDataBase>(this.<GetRowNodesEnumeratorCore>b__26_4);
                    }
                    DetailInfoWithContent.DetailRowNode rowNode = this.<>4__this.GetRowNode(DetailNodeKind.DataRowsContainer, createRowDataDelegate);
                    rowNode.UpdateExpandInfoEx(this.<>4__this.DetailRootNodeContainer, this.<>4__this.SafeGetViewProperty<RowsContainer>(DataControlDetailInfo.<>c.<>9__26_6 ??= new Func<DataViewBase, RowsContainer>(this.<GetRowNodesEnumeratorCore>b__26_6)), this.nextLevelStartVisibleIndex, false);
                    this.<>2__current = rowNode;
                    this.<>1__state = 6;
                    return true;
                }
                goto TR_0010;
            TR_001A:
                this.<isFixedTotalSummaryRowVisible>5__3 = this.<>4__this.DataControl.DataView.ShowFixedTotalSummary && (this.serviceVisibleRowsCount > 0);
                if (this.<isFixedTotalSummaryRowVisible>5__3)
                {
                    serviceVisibleRowsCount = this.serviceVisibleRowsCount;
                    this.serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                }
                this.<isTotalSummaryVisible>5__2 = this.<>4__this.DataControl.DataView.ShowTotalSummary && (this.serviceVisibleRowsCount > 0);
                if (this.<isTotalSummaryVisible>5__2)
                {
                    serviceVisibleRowsCount = this.serviceVisibleRowsCount;
                    this.serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                }
                bool flag = this.<>4__this.DataControl.DataView.IsNewItemRowVisible && (this.serviceVisibleRowsCount > 0);
                if (flag)
                {
                    serviceVisibleRowsCount = this.serviceVisibleRowsCount;
                    this.serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                }
                if (flag)
                {
                    this.<>2__current = this.<>4__this.GetRowNode(DetailNodeKind.NewItemRow, new Func<DetailNodeKind, RowDataBase>(this.<>4__this.<GetRowNodesEnumeratorCore>b__26_2));
                    this.<>1__state = 5;
                    return true;
                }
                goto TR_0011;
            TR_001C:
                if (this.<>4__this.DataControl.DataView.CanShowDetailColumnHeadersControl && (this.serviceVisibleRowsCount > 0))
                {
                    serviceVisibleRowsCount = this.serviceVisibleRowsCount;
                    this.serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                    this.<>2__current = this.<>4__this.GetRowNode(DetailNodeKind.ColumnHeaders, new Func<DetailNodeKind, RowDataBase>(this.<>4__this.<GetRowNodesEnumeratorCore>b__26_0));
                    this.<>1__state = 4;
                    return true;
                }
                goto TR_001A;
            TR_001D:
                if (this.<hasContentRow>5__1 && (this.serviceVisibleRowsCount > 0))
                {
                    serviceVisibleRowsCount = this.serviceVisibleRowsCount;
                    this.serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                    DetailInfoWithContent.DetailHeaderRowNode detailContentNode = (DetailInfoWithContent.DetailHeaderRowNode) this.<>4__this.GetDetailContentNode(true);
                    detailContentNode.ShowBottomLine = true;
                    this.<>2__current = detailContentNode;
                    this.<>1__state = 3;
                    return true;
                }
                goto TR_001C;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            RowNode IEnumerator<RowNode>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        public class DetailViewRowData : DetailInfoWithContent.DetailRowData, IViewRowData
        {
            public DetailViewRowData(DetailNodeKind nodeKind, Func<FrameworkElement> createRowElementDelegate) : base(nodeKind, createRowElementDelegate)
            {
            }

            void IViewRowData.SetViewAndUpdate(DataViewBase view)
            {
                base.View = view;
            }

            protected override int GetLineLevel(RowDataBase rowData, int lineLevel, int detailCount) => 
                (!(rowData is RowData) || (((rowData as RowData).RowPosition == RowPosition.Bottom) && ((rowData as RowData).RowPosition == RowPosition.Single))) ? base.GetLineLevel(rowData, lineLevel, detailCount) : lineLevel;

            internal override void SetMasterView(DataViewBase view)
            {
            }

            internal override void UpdateLineLevel()
            {
                DataViewBase targetView = null;
                int targetVisibleIndex = -1;
                if (!base.View.DataControl.DataControlParent.FindMasterRow(out targetView, out targetVisibleIndex) || (targetView.GetRowData(targetView.DataControl.GetRowHandleByVisibleIndexCore(targetVisibleIndex)) == null))
                {
                    base.LineLevel = 0;
                    base.DetailLevel = 0;
                }
                else
                {
                    base.LineLevel = this.GetLineLevel(this, 0, 0);
                    bool isLastRow = true;
                    base.DetailLevel = base.GetDetailLevel(this, 0, ref isLastRow);
                    if (isLastRow)
                    {
                        base.LineLevel = base.DetailLevel;
                    }
                }
            }
        }

        internal class DetailViewRowNode : DetailInfoWithContent.DetailRowNode
        {
            private readonly DataViewBase view;

            public DetailViewRowNode(DetailInfoWithContent owner, DataViewBase view, DetailNodeKind detailNodeKind, Func<DetailNodeKind, RowDataBase> createRowDataDelegate) : base(owner, detailNodeKind, createRowDataDelegate)
            {
                this.view = view;
            }

            internal override void AssignToRowData(RowDataBase rowData, RowDataBase masterRowData)
            {
                IViewRowData data = rowData as IViewRowData;
                if (data != null)
                {
                    data.SetViewAndUpdate(this.view);
                }
            }
        }
    }
}

