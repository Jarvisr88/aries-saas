namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Hierarchy;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Threading;

    public class MasterRowsContainer : DevExpress.Xpf.Grid.DetailRowsContainer, IRootItemsContainer, IDetailRootItemsContainer, IItemsContainer
    {
        private DataViewBase focusedView;
        private List<RowData> rowsToUpdate;
        private int oldStartVisibleIndex;
        private bool isDirect;
        private List<int> rowData1Indices;
        private List<int> rowData2Indices;

        public event HierarchyChangedEventHandler HierarchyChanged;

        public MasterRowsContainer(DataTreeBuilder treeBuilder, int level) : base(treeBuilder, level)
        {
            this.rowsToUpdate = new List<RowData>();
            this.rowData1Indices = new List<int>();
            this.rowData2Indices = new List<int>();
            this.focusedView = treeBuilder.View;
        }

        private static void CollectParentIndices(RowData rowData, List<int> indices)
        {
            indices.Clear();
            rowData.View.DataControl.EnumerateThisAndParentDataControls((dataControl, visibleIndex) => indices.Add(visibleIndex), rowData.ControllerVisibleIndex);
        }

        private int CompareRowData(RowData rowData1, RowData rowData2)
        {
            int num;
            if (ReferenceEquals(rowData1.View, rowData2.View))
            {
                num = Comparer<int>.Default.Compare(rowData1.ControllerVisibleIndex, rowData2.ControllerVisibleIndex);
                if (num == 0)
                {
                    if (rowData1.MatchKey is GroupSummaryRowKey)
                    {
                        num = -1;
                    }
                    else if (rowData2.MatchKey is GroupSummaryRowKey)
                    {
                        num = 1;
                    }
                }
            }
            else
            {
                CollectParentIndices(rowData1, this.rowData1Indices);
                CollectParentIndices(rowData2, this.rowData2Indices);
                num = 0;
                int num2 = Math.Min(this.rowData1Indices.Count, this.rowData2Indices.Count);
                int num3 = 1;
                while (true)
                {
                    if (num3 <= num2)
                    {
                        num = Comparer<int>.Default.Compare(this.rowData1Indices[this.rowData1Indices.Count - num3], this.rowData2Indices[this.rowData2Indices.Count - num3]);
                        if (num == 0)
                        {
                            num3++;
                            continue;
                        }
                    }
                    num ??= Comparer<int>.Default.Compare(this.rowData1Indices.Count, this.rowData2Indices.Count);
                    break;
                }
            }
            return (this.isDirect ? num : -num);
        }

        private void InvokeUpdatePostponedDataItem()
        {
            if (this.rowsToUpdate.Count > 0)
            {
                base.View.Dispatcher.BeginInvoke(new Action(this.UpdatePostponedDataItem), DispatcherPriority.Background, new object[0]);
            }
        }

        private void ProcessInvisibleItems()
        {
            foreach (RowData data in this.rowsToUpdate)
            {
                data.UpdateIsDirty();
            }
        }

        public void RaiseHierarchyChanged(HierarchyChangedEventArgs eventArgs)
        {
            if (this.HierarchyChanged != null)
            {
                this.HierarchyChanged(this, eventArgs);
            }
        }

        internal void UpdatePostponedData(bool updateStartIndex, bool updateImmediately)
        {
            if (((base.View.ViewBehavior.AllowCascadeUpdate || base.View.GetAllowGroupSummaryCascadeUpdate) && (base.View.DataPresenter != null)) && (!base.View.DataPresenter.AdjustmentInProgress && !base.View.PostponedNavigationInProgress))
            {
                this.ProcessInvisibleItems();
                this.rowsToUpdate.Clear();
                VirtualItemsEnumerator enumerator = base.View.CreateAllRowsEnumerator();
                while (enumerator.MoveNext())
                {
                    if (!(enumerator.CurrentData is RowData))
                    {
                        continue;
                    }
                    this.rowsToUpdate.Add((RowData) enumerator.CurrentData);
                }
                this.ProcessInvisibleItems();
                int scrollOffset = base.View.DataPresenter.ScrollOffset;
                if (updateStartIndex && (scrollOffset != this.oldStartVisibleIndex))
                {
                    this.isDirect = this.oldStartVisibleIndex < scrollOffset;
                    this.oldStartVisibleIndex = scrollOffset;
                }
                if (!updateImmediately)
                {
                    this.InvokeUpdatePostponedDataItem();
                }
                else if (!base.View.DataPresenter.CanScrollWithAnimation)
                {
                    this.UpdatePostponedDataItem();
                }
            }
        }

        private void UpdatePostponedDataItem()
        {
            this.rowsToUpdate.Sort(new Comparison<RowData>(this.CompareRowData));
            while (this.rowsToUpdate.Count != 0)
            {
                RowData data = this.rowsToUpdate[0];
                this.rowsToUpdate.RemoveAt(0);
                if (data.IsRowInView() && !data.IsReady)
                {
                    data.RefreshData();
                    this.InvokeUpdatePostponedDataItem();
                    return;
                }
            }
        }

        protected DataControlBase MasterDataControl =>
            base.TreeBuilder.View.DataControl;

        public DataViewBase FocusedView
        {
            get => 
                this.focusedView;
            internal set
            {
                if (!ReferenceEquals(this.focusedView, value))
                {
                    DataViewBase focusedView = this.focusedView;
                    this.focusedView = value;
                    focusedView.ProcessFocusedViewChange();
                    this.focusedView.ProcessFocusedViewChange();
                    this.focusedView.RootView.RaiseFocusedViewChanged(focusedView, this.focusedView);
                }
            }
        }

        double IRootItemsContainer.ScrollItemOffset =>
            base.View.DataPresenter.ScrollItemOffset;

        IItem IRootItemsContainer.ScrollItem =>
            base.View.DataPresenter.GetRowDataToScroll();

        internal List<RowData> RowsToUpdate =>
            this.rowsToUpdate;
    }
}

