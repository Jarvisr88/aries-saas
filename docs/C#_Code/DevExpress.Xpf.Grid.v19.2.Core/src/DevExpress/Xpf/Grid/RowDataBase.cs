namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Hierarchy;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public abstract class RowDataBase : DataObjectBase, IItem, ISupportVisibleIndex, INotifyPropertyChanged
    {
        protected static IEnumerable<RowDataBase> EmptyEnumerable = new RowData[0];
        private DataViewBase view;
        internal RowNode node;
        private FrameworkElement wholeRowElement;
        private DevExpress.Xpf.Grid.RowsContainer rowsContainer;
        private DevExpress.Xpf.Grid.IndicatorState indicatorState;
        private bool isMasterRowExpanded;
        private int lineLevel;
        private int detailLevel;
        private readonly NotImplementedRowDataReusingStrategy reusingStrategy;
        private int visibleIndex;

        protected RowDataBase(Func<FrameworkElement> createRowElementDelegate = null)
        {
            this.SetVisibleIndex(-1);
            this.reusingStrategy = this.CreateReusingStrategy(createRowElementDelegate);
        }

        protected virtual void ApplyRowDataToAdditionalElement()
        {
            this.AdditionalElement.DataContext = this;
        }

        protected void AssignChildItems()
        {
            if (this.RowsContainer != null)
            {
                if ((this.node.NodesContainer != null) && (this.node.NodesContainer.Items.Count > 0))
                {
                    this.RowsContainer.Synchronize(this.node.NodesContainer);
                }
                else
                {
                    this.RowsContainer.BaseSyncronize(this.node.NodesContainer);
                    this.ClearRowsContainer();
                }
            }
        }

        internal void AssignFromInternal(DevExpress.Xpf.Grid.RowsContainer parentRowsContainer, NodeContainer parentNodeContainer, RowNode rowNode, bool forceUpdate = false)
        {
            this.reusingStrategy.AssignFrom(parentRowsContainer, parentNodeContainer, rowNode, forceUpdate);
            this.AssignChildItems();
        }

        internal void AssignVirtualizedRowData(DevExpress.Xpf.Grid.RowsContainer parentRowsContainer, NodeContainer parentNodeContainer, RowNode node, bool forceUpdate)
        {
            bool flag = !ReferenceEquals(this.node, node);
            this.AssignFromInternal(parentRowsContainer, parentNodeContainer, node, forceUpdate);
            parentRowsContainer.UnstoreFreeRowData(node, this);
            this.reusingStrategy.CacheRowData();
            if (flag)
            {
                base.RaiseResetEvents();
            }
        }

        internal virtual void ClearRow()
        {
        }

        protected void ClearRowsContainer()
        {
            if (this.RowsContainer != null)
            {
                this.RowsContainer.StoreFreeData();
                this.RowsContainer.RaiseItemsRemoved(this.RowsContainer.Items);
                this.RowsContainer.Items.Clear();
            }
        }

        protected virtual NotImplementedRowDataReusingStrategy CreateReusingStrategy(Func<FrameworkElement> createRowElementDelegate) => 
            NotImplementedRowDataReusingStrategy.Instance;

        protected int GetDetailLevel(RowDataBase rowData, int level, ref bool isLastRow)
        {
            DataViewBase targetView = null;
            int targetVisibleIndex = -1;
            if (rowData.View.DataControl.DataControlParent.FindMasterRow(out targetView, out targetVisibleIndex))
            {
                RowData data = targetView.GetRowData(targetView.DataControl.GetRowHandleByVisibleIndexCore(targetVisibleIndex));
                if (data != null)
                {
                    bool flag = targetView.DataControl.VisibleRowCount == (targetVisibleIndex + 1);
                    bool flag2 = !targetView.IsRootView && (targetView.ShowFixedTotalSummary || targetView.ShowTotalSummary);
                    bool flag3 = rowData.View.DataControl.OwnerDetailDescriptor.Return<DetailDescriptorBase, bool>(<>c.<>9__53_0 ??= x => x.HasBottomMargin, <>c.<>9__53_1 ??= () => false);
                    if ((!flag | flag2) | flag3)
                    {
                        isLastRow = false;
                    }
                    level++;
                    return this.GetDetailLevel(data, level, ref isLastRow);
                }
            }
            return level;
        }

        protected internal virtual bool GetIsFocusable() => 
            this.GetIsVisible();

        internal bool GetIsVisible() => 
            this.visibleIndex != -1;

        protected virtual int GetLineLevel(RowDataBase rowData, int lineLevel, int detailCount)
        {
            DataViewBase targetView = null;
            int targetVisibleIndex = -1;
            if (rowData.View.DataControl.DataControlParent.FindMasterRow(out targetView, out targetVisibleIndex))
            {
                RowData data = targetView.GetRowData(targetView.DataControl.GetRowHandleByVisibleIndexCore(targetVisibleIndex));
                if (data != null)
                {
                    Func<DetailDescriptorBase, bool> evaluator = <>c.<>9__52_0;
                    if (<>c.<>9__52_0 == null)
                    {
                        Func<DetailDescriptorBase, bool> local1 = <>c.<>9__52_0;
                        evaluator = <>c.<>9__52_0 = x => x.HasBottomMargin;
                    }
                    bool flag = rowData.View.DataControl.OwnerDetailDescriptor.Return<DetailDescriptorBase, bool>(evaluator, <>c.<>9__52_1 ??= () => false);
                    bool flag2 = targetView.DataControl.VisibleRowCount == (targetVisibleIndex + 1);
                    bool flag3 = !targetView.IsRootView && (targetView.ShowFixedTotalSummary || targetView.ShowTotalSummary);
                    if (!flag2)
                    {
                        if ((targetView.GroupCount > 0) && (!flag && ((data.RowPosition == RowPosition.Bottom) || (data.RowPosition == RowPosition.Single))))
                        {
                            lineLevel++;
                        }
                    }
                    else if (!flag3 && !flag)
                    {
                        lineLevel++;
                        return this.GetLineLevel(data, lineLevel, detailCount);
                    }
                }
            }
            return lineLevel;
        }

        private void InitAdditionalElement()
        {
            this.AdditionalElement = this.View.CreateAdditionalElement();
            if (this.AdditionalElement != null)
            {
                this.ApplyRowDataToAdditionalElement();
            }
        }

        protected void InitWholeRowElement()
        {
            FrameworkElement rowElement = this.reusingStrategy.CreateRowElement();
            this.SetElementDataContext(rowElement);
            SetDataObject(rowElement, this);
            this.wholeRowElement = rowElement;
        }

        internal virtual bool IsFocusedRow() => 
            false;

        protected void NotifyPropertyChanged(string info)
        {
            base.RaisePropertyChanged(info);
        }

        protected virtual void OnViewChanged()
        {
            this.InitAdditionalElement();
        }

        protected virtual void OnVisibilityChanged(bool isVisible)
        {
        }

        private void SetElementDataContext(FrameworkElement rowElement)
        {
            if (!(rowElement is IAdditionalRowElement) || !((IAdditionalRowElement) rowElement).LockDataContext)
            {
                rowElement.DataContext = this;
            }
        }

        internal void SetVisibleIndex(int index)
        {
            if (this.visibleIndex != index)
            {
                this.visibleIndex = index;
                bool isVisible = this.GetIsVisible();
                if (this.GetIsVisible() != isVisible)
                {
                    this.OnVisibilityChanged(isVisible);
                }
            }
        }

        protected virtual void SetWholeElement(FrameworkElement rowElement)
        {
            this.wholeRowElement = rowElement;
        }

        internal void StoreAsFreeData(DevExpress.Xpf.Grid.RowsContainer dataContainer)
        {
            dataContainer.StoreFreeRowData(this.node, this);
            if (this.RowsContainer != null)
            {
                this.RowsContainer.StoreFreeData();
            }
            this.SetVisibleIndex(-1);
            this.UpdateFullState();
        }

        protected virtual void UpdateClientIndicatorState()
        {
        }

        protected internal virtual bool UpdateFixedRowPosition() => 
            false;

        protected internal virtual void UpdateFullState()
        {
        }

        internal virtual void UpdateLineLevel()
        {
        }

        internal virtual void UpdateRow()
        {
        }

        protected internal FrameworkElement WholeRowElement
        {
            get
            {
                if (this.wholeRowElement == null)
                {
                    this.InitWholeRowElement();
                }
                return this.wholeRowElement;
            }
        }

        internal abstract object MatchKey { get; }

        [Description("")]
        public DataViewBase View
        {
            get => 
                this.view;
            protected set
            {
                if (!ReferenceEquals(this.view, value))
                {
                    this.view = value;
                    this.OnViewChanged();
                    this.NotifyPropertyChanged("View");
                }
            }
        }

        [Description("")]
        public DevExpress.Xpf.Grid.RowsContainer RowsContainer
        {
            get => 
                this.rowsContainer;
            internal set
            {
                if (value != null)
                {
                    value.SetOwnerRowData(this);
                }
                if (!ReferenceEquals(this.RowsContainer, value))
                {
                    this.ClearRowsContainer();
                }
                if (!ReferenceEquals(this.rowsContainer, value))
                {
                    this.rowsContainer = value;
                    base.RaisePropertyChanged("RowsContainer");
                }
            }
        }

        public DevExpress.Xpf.Grid.IndicatorState IndicatorState
        {
            get => 
                this.indicatorState;
            internal set
            {
                if (this.indicatorState != value)
                {
                    this.indicatorState = value;
                    base.RaisePropertyChanged("IndicatorState");
                    this.UpdateClientIndicatorState();
                }
            }
        }

        public bool IsMasterRowExpanded
        {
            get => 
                this.isMasterRowExpanded;
            protected set
            {
                if (this.isMasterRowExpanded != value)
                {
                    this.isMasterRowExpanded = value;
                    base.RaisePropertyChanged("IsMasterRowExpanded");
                }
            }
        }

        public int LineLevel
        {
            get => 
                this.lineLevel;
            protected set
            {
                if (this.lineLevel != value)
                {
                    this.lineLevel = value;
                    base.RaisePropertyChanged("LineLevel");
                }
            }
        }

        public int DetailLevel
        {
            get => 
                this.detailLevel;
            protected set
            {
                if (this.detailLevel != value)
                {
                    this.detailLevel = value;
                    base.RaisePropertyChanged("DetailLevel");
                }
            }
        }

        FrameworkElement IItem.Element =>
            this.WholeRowElement;

        protected internal FrameworkElement AdditionalElement { get; private set; }

        FrameworkElement IItem.AdditionalElement =>
            this.AdditionalElement;

        double IItem.AdditionalElementWidth =>
            (this.View != null) ? this.View.AdditionalElementWidth : 0.0;

        double IItem.GridAreaWidth =>
            (this.View != null) ? this.View.ActualColumnsWidth : 0.0;

        double IItem.AdditionalElementOffset =>
            (this.View != null) ? this.View.ActualAdditionalElementOffset : 0.0;

        IItemsContainer IItem.ItemsContainer =>
            this.RowsContainer ?? EmptyItemsContainer.Instance;

        bool IItem.IsFixedItem =>
            this.node.IsFixedNode;

        bool IItem.IsRowVisible =>
            this.node.IsRowVisible;

        FixedRowPosition IItem.FixedRowPosition =>
            this.node.FixedRowPosition;

        bool IItem.IsItemsContainer =>
            this.IsItemsContainerCore;

        protected virtual bool IsItemsContainerCore =>
            false;

        int ISupportVisibleIndex.VisibleIndex =>
            this.visibleIndex;

        protected internal virtual FixedRowPosition FixedRowPositionCore =>
            FixedRowPosition.None;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RowDataBase.<>c <>9 = new RowDataBase.<>c();
            public static Func<DetailDescriptorBase, bool> <>9__52_0;
            public static Func<bool> <>9__52_1;
            public static Func<DetailDescriptorBase, bool> <>9__53_0;
            public static Func<bool> <>9__53_1;

            internal bool <GetDetailLevel>b__53_0(DetailDescriptorBase x) => 
                x.HasBottomMargin;

            internal bool <GetDetailLevel>b__53_1() => 
                false;

            internal bool <GetLineLevel>b__52_0(DetailDescriptorBase x) => 
                x.HasBottomMargin;

            internal bool <GetLineLevel>b__52_1() => 
                false;
        }

        protected class DetailRowDataReusingStrategy : RowDataBase.NotImplementedRowDataReusingStrategy
        {
            protected readonly RowDataBase rowData;
            private readonly Func<FrameworkElement> createRowElementDelegate;

            public DetailRowDataReusingStrategy(RowDataBase rowData, Func<FrameworkElement> createRowElementDelegate)
            {
                this.rowData = rowData;
                this.createRowElementDelegate = createRowElementDelegate;
            }

            internal override void AssignFrom(RowsContainer parentRowsContainer, NodeContainer parentNodeContainer, RowNode rowNode, bool forceUpdate = false)
            {
                DetailInfoWithContent.DetailRowNode node = (DetailInfoWithContent.DetailRowNode) rowNode;
                this.rowData.node = node;
                this.rowData.RowsContainer = node.childrenRowsContainer;
                RowData masterRowData = ((RowDetailInfoBase.DetailRowsContainer) parentRowsContainer).MasterRowData;
                node.AssignToRowData(this.rowData, masterRowData);
                ((DetailRowControlBase) this.rowData.WholeRowElement).MasterRowData = masterRowData;
            }

            internal override void CacheRowData()
            {
                ((DetailInfoWithContent.DetailRowNode) this.rowData.node).CurrentRowData = this.rowData;
            }

            internal override FrameworkElement CreateRowElement() => 
                this.createRowElementDelegate();
        }

        protected class NotImplementedRowDataReusingStrategy
        {
            public static readonly RowDataBase.NotImplementedRowDataReusingStrategy Instance = new RowDataBase.NotImplementedRowDataReusingStrategy();

            protected NotImplementedRowDataReusingStrategy()
            {
            }

            internal virtual void AssignFrom(RowsContainer parentRowsContainer, NodeContainer parentNodeContainer, RowNode rowNode, bool forceUpdate = false)
            {
                throw new NotImplementedException();
            }

            internal virtual void CacheRowData()
            {
                throw new NotImplementedException();
            }

            internal virtual FrameworkElement CreateRowElement()
            {
                throw new NotImplementedException();
            }
        }
    }
}

