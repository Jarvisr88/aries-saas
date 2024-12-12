namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Input;

    public abstract class MultiSelectionStrategyBase : SelectionStrategyBase
    {
        private Dictionary<DataViewBase, Tuple<int, int>> cashOldView;
        private Dictionary<DataViewBase, Tuple<int, int>> cashNewView;

        public MultiSelectionStrategyBase(DataViewBase view) : base(view)
        {
        }

        private int GetIncrementCountRows(int currentIndex, int endIndex, DataControlBase dataControl)
        {
            int num = 0;
            DataControlBase rootDataControl = dataControl.GetRootDataControl();
            DataViewBase dataView = dataControl.DataView;
            for (int i = currentIndex + 1; i <= endIndex; i++)
            {
                KeyValuePair<DataViewBase, int> pair = rootDataControl.FindViewAndVisibleIndexByCommonVisibleIndex(i);
                if ((pair.Key == null) || (pair.Key != dataView))
                {
                    return num;
                }
                num++;
            }
            return num;
        }

        public override IList<Tuple<DataControlBase, int>> GetSelectedRowsInfo()
        {
            if (base.view.IsRootView || !base.view.DataControl.IsOriginationDataControl())
            {
                int[] selectedRows = this.GetSelectedRows();
                return (((selectedRows == null) || (selectedRows.Length == 0)) ? base.GetSelectedRowsInfo() : (from x in selectedRows select new Tuple<DataControlBase, int>(base.view.DataControl, x)).ToList<Tuple<DataControlBase, int>>());
            }
            List<Tuple<DataControlBase, int>> list = new List<Tuple<DataControlBase, int>>();
            foreach (DataControlBase dataControl in base.view.DataControl.DetailClones)
            {
                int[] selectedRowHandles = dataControl.GetSelectedRowHandles();
                if ((list != null) && (selectedRowHandles.Length != 0))
                {
                    list.AddRange((from x in selectedRowHandles select new Tuple<DataControlBase, int>(dataControl, x)).ToList<Tuple<DataControlBase, int>>());
                }
            }
            return list;
        }

        public override void OnDataControlInitialized()
        {
            if (base.DataControl.SelectedItems != null)
            {
                this.ProcessSelectedItemsChanged();
            }
            else
            {
                if (base.DataControl.SelectedItem == null)
                {
                    base.OnDataControlInitialized();
                    this.OnDataSourceReset();
                    return;
                }
                this.ProcessSelectedItemChanged();
            }
            base.OnDataControlInitialized();
        }

        public override void OnItemChanged(ListChangedEventArgs e)
        {
            if ((e.ListChangedType == ListChangedType.ItemChanged) && base.CanUpdateSelectedItems)
            {
                int newIndex = e.NewIndex;
                if (this.IsRowSelected(newIndex) && !base.SelectedItems.Contains(base.DataControl.GetRow(newIndex)))
                {
                    this.UnselectRow(newIndex);
                }
            }
        }

        public override void OnSelectedItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            if (base.CanGetSelectedItems)
            {
                base.SelectionLocker.DoLockedActionIfNotLocked(delegate {
                    if ((e.Action == NotifyCollectionChangedAction.Remove) || (e.Action == NotifyCollectionChangedAction.Replace))
                    {
                        foreach (object obj2 in e.OldItems)
                        {
                            this.UnselectItem(obj2);
                        }
                    }
                    if ((e.Action == NotifyCollectionChangedAction.Add) || (e.Action == NotifyCollectionChangedAction.Replace))
                    {
                        foreach (object obj3 in e.NewItems)
                        {
                            this.SelectItem(obj3);
                        }
                    }
                    if (e.Action == NotifyCollectionChangedAction.Reset)
                    {
                        this.SelectItems(this.SelectedItems);
                    }
                    this.UpdateSelectedItem();
                });
            }
        }

        public override void ProcessSelectedItemsChanged()
        {
            this.OnSelectedItemsChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void SelectDetailRangeByCommonVisibleIndex(int minIndex, int maxIndex, double minX, double maxX, DataControlBase rootDataControl, bool useSelectionCount, bool selectOtherStrategy)
        {
            bool hasDetailViews = ((ITableView) rootDataControl.DataView).HasDetailViews;
            this.cashOldView = new Dictionary<DataViewBase, Tuple<int, int>>();
            this.cashNewView = new Dictionary<DataViewBase, Tuple<int, int>>();
            if (this.OldMasterRectangle != null)
            {
                this.UnselectDetailstOldRectange(this.OldMasterRectangle.StartVisibleIndex, minIndex, minIndex, maxIndex, rootDataControl, useSelectionCount, selectOtherStrategy);
                this.UnselectDetailstOldRectange(maxIndex + 1, this.OldMasterRectangle.EndVisibleIndex + 1, minIndex, maxIndex, rootDataControl, useSelectionCount, selectOtherStrategy);
            }
            if (hasDetailViews)
            {
                this.SelectDetailRangeByCommonVisibleIndexBase(minIndex, maxIndex, minX, maxX, rootDataControl, useSelectionCount, selectOtherStrategy);
            }
            else
            {
                this.SelectRangeByCommonVisibleIndex(minIndex, maxIndex, minX, maxX, rootDataControl, useSelectionCount, selectOtherStrategy);
            }
            this.cashOldView = null;
            this.cashNewView = null;
        }

        private void SelectDetailRangeByCommonVisibleIndexBase(int minIndex, int maxIndex, double minX, double maxX, DataControlBase rootDataControl, bool useSelectionCount, bool selectOtherStrategy)
        {
            int commonVisibleIndex = minIndex;
            while (commonVisibleIndex <= maxIndex)
            {
                KeyValuePair<DataViewBase, int> pair = rootDataControl.FindViewAndVisibleIndexByCommonVisibleIndex(commonVisibleIndex);
                if (pair.Key == null)
                {
                    commonVisibleIndex++;
                    continue;
                }
                DataControlBase dataControl = pair.Key.DataControl;
                int rowHandleByVisibleIndexCore = dataControl.GetRowHandleByVisibleIndexCore(pair.Value);
                dataControl.DataView.SelectionStrategy.BeginSelection();
                try
                {
                    if (dataControl.IsGroupRowHandleCore(rowHandleByVisibleIndexCore))
                    {
                        if (dataControl.DataView.SelectionStrategy.IsCellSelection())
                        {
                            dataControl.DataView.SelectionStrategy.SelectCellCore(rowHandleByVisibleIndexCore, dataControl.DataView.VisibleColumnsCore[0], useSelectionCount);
                        }
                        else if (selectOtherStrategy)
                        {
                            dataControl.DataView.SelectionStrategy.SelectRow(rowHandleByVisibleIndexCore);
                        }
                    }
                    else if (!dataControl.DataView.SelectionStrategy.IsCellSelection())
                    {
                        if (selectOtherStrategy)
                        {
                            dataControl.DataView.SelectionStrategy.SelectRow(rowHandleByVisibleIndexCore);
                        }
                    }
                    else
                    {
                        int actualVisibleIndex;
                        int num4;
                        int num5;
                        int num6;
                        double? nullable3;
                        DataViewBase key = pair.Key.OriginationView ?? pair.Key;
                        if (this.cashNewView.ContainsKey(key))
                        {
                            Tuple<int, int> tuple = this.cashNewView[key];
                            actualVisibleIndex = tuple.Item1;
                            num4 = tuple.Item2;
                        }
                        else
                        {
                            nullable3 = null;
                            actualVisibleIndex = ((TableViewBehavior) dataControl.DataView.ViewBehavior).GetColumnByCoordinateWithOffset(minX, nullable3).ActualVisibleIndex;
                            nullable3 = null;
                            num4 = ((TableViewBehavior) dataControl.DataView.ViewBehavior).GetColumnByCoordinateWithOffset(maxX, nullable3).ActualVisibleIndex;
                            this.cashNewView.Add(key, new Tuple<int, int>(actualVisibleIndex, num4));
                        }
                        int? nullable = null;
                        int? nullable2 = null;
                        if (this.OldMasterRectangle == null)
                        {
                            num5 = actualVisibleIndex;
                            num6 = num4;
                        }
                        else
                        {
                            if (this.cashOldView.ContainsKey(key))
                            {
                                Tuple<int, int> tuple2 = this.cashOldView[key];
                                nullable = new int?(tuple2.Item1);
                                nullable2 = new int?(tuple2.Item2);
                            }
                            else
                            {
                                nullable3 = null;
                                nullable = new int?(((TableViewBehavior) dataControl.DataView.ViewBehavior).GetColumnByCoordinateWithOffset(this.OldMasterRectangle.StartX, nullable3).ActualVisibleIndex);
                                nullable3 = null;
                                nullable2 = new int?(((TableViewBehavior) dataControl.DataView.ViewBehavior).GetColumnByCoordinateWithOffset(this.OldMasterRectangle.EndX, nullable3).ActualVisibleIndex);
                                this.cashOldView.Add(key, new Tuple<int, int>(nullable.Value, nullable2.Value));
                            }
                            num5 = Math.Min(actualVisibleIndex, nullable.Value);
                            num6 = Math.Max(num4, nullable2.Value);
                        }
                        SelectionStrategyBase selectionStrategy = dataControl.DataView.SelectionStrategy;
                        int num7 = this.GetIncrementCountRows(commonVisibleIndex, maxIndex, dataControl);
                        int num8 = commonVisibleIndex;
                        while (true)
                        {
                            if (num8 > (commonVisibleIndex + num7))
                            {
                                commonVisibleIndex += num7;
                                break;
                            }
                            KeyValuePair<DataViewBase, int> pair2 = rootDataControl.FindViewAndVisibleIndexByCommonVisibleIndex(num8);
                            if (pair2.Key != null)
                            {
                                int rowHandle = dataControl.GetRowHandleByVisibleIndexCore(pair2.Value);
                                if ((rowHandle != -2147483645) && (rowHandle != -2147483647))
                                {
                                    if (dataControl.IsGroupRowHandleCore(rowHandle))
                                    {
                                        if (dataControl.DataView.SelectionStrategy.IsCellSelection())
                                        {
                                            dataControl.DataView.SelectionStrategy.SelectCellCore(rowHandle, dataControl.DataView.VisibleColumnsCore[0], useSelectionCount);
                                        }
                                        else if (selectOtherStrategy)
                                        {
                                            dataControl.DataView.SelectionStrategy.SelectRow(rowHandle);
                                        }
                                    }
                                    else
                                    {
                                        for (int i = num5; i <= num6; i++)
                                        {
                                            if ((i >= 0) && (i <= (dataControl.DataView.VisibleColumnsCore.Count - 1)))
                                            {
                                                ColumnBase column = dataControl.DataView.VisibleColumnsCore[i];
                                                if ((nullable != null) && ((num8 >= this.OldMasterRectangle.StartVisibleIndex) && ((num8 <= this.OldMasterRectangle.EndVisibleIndex) && ((column.ActualVisibleIndex >= nullable.Value) && (column.ActualVisibleIndex <= nullable2.Value)))))
                                                {
                                                    selectionStrategy.UnselectCellCore(rowHandle, column, useSelectionCount);
                                                }
                                                if ((column.ActualVisibleIndex >= actualVisibleIndex) && (column.ActualVisibleIndex <= num4))
                                                {
                                                    selectionStrategy.SelectCellCore(rowHandle, column, useSelectionCount);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            num8++;
                        }
                    }
                }
                finally
                {
                    dataControl.DataView.SelectionStrategy.EndSelection();
                }
                commonVisibleIndex++;
            }
        }

        internal override void SelectMasterDetailRangeCell(int startCommonVisibleIndex, int endCommonVisibleIndex, double startX, double endX, DataControlBase rootDataControl, bool useSelectionCount, bool selectOtherStrategy, bool startInDataArea = false)
        {
            this.SelectMasterDetailRangeCellCore(startCommonVisibleIndex, endCommonVisibleIndex, startX, endX, rootDataControl, useSelectionCount, selectOtherStrategy, startInDataArea);
        }

        protected void SelectMasterDetailRangeCellCore(int startCommonVisibleIndex, int endCommonVisibleIndex, double startX, double endX, DataControlBase rootDataControl, bool useSelectionCount, bool selectOtherStrategy, bool startInDataArea)
        {
            if ((startCommonVisibleIndex < 0) || (endCommonVisibleIndex < 0))
            {
                if (!startInDataArea || ((endCommonVisibleIndex >= 0) || (startCommonVisibleIndex <= 0)))
                {
                    return;
                }
                endCommonVisibleIndex = startCommonVisibleIndex;
                startCommonVisibleIndex++;
            }
            int minIndex = Math.Min(startCommonVisibleIndex, endCommonVisibleIndex);
            int maxIndex = Math.Max(startCommonVisibleIndex, endCommonVisibleIndex);
            double minX = Math.Min(startX, endX);
            double maxX = Math.Max(startX, endX);
            this.SelectDetailRangeByCommonVisibleIndex(minIndex, maxIndex, minX, maxX, rootDataControl, useSelectionCount, selectOtherStrategy);
            SelectionStrategyBase.RectangleMaster master1 = new SelectionStrategyBase.RectangleMaster();
            master1.StartVisibleIndex = minIndex;
            master1.EndVisibleIndex = maxIndex;
            master1.StartX = minX;
            master1.EndX = maxX;
            this.OldMasterRectangle = master1;
        }

        private void SelectRangeByCommonVisibleIndex(int minIndex, int maxIndex, double minX, double maxX, DataControlBase rootDataControl, bool useSelectionCount, bool selectOtherStrategy)
        {
            KeyValuePair<DataViewBase, int> pair = rootDataControl.FindViewAndVisibleIndexByCommonVisibleIndex(minIndex);
            KeyValuePair<DataViewBase, int> pair2 = rootDataControl.FindViewAndVisibleIndexByCommonVisibleIndex(maxIndex);
            int rowHandleByVisibleIndexCore = rootDataControl.GetRowHandleByVisibleIndexCore(pair.Value);
            int rowHandle = rootDataControl.GetRowHandleByVisibleIndexCore(pair2.Value);
            if (!rootDataControl.IsValidRowHandleCore(rowHandle))
            {
                pair2 = rootDataControl.FindViewAndVisibleIndexByCommonVisibleIndex(maxIndex - 1);
                rowHandle = rootDataControl.GetRowHandleByVisibleIndexCore(pair2.Value);
            }
            if ((pair.Key != null) && ((pair.Key.DataControl != null) && ((pair2.Key != null) && ((pair2.Key.DataControl != null) && (rootDataControl.IsValidRowHandleCore(rowHandleByVisibleIndexCore) && rootDataControl.IsValidRowHandleCore(rowHandle))))))
            {
                int commonVisibleIndex = pair.Key.DataControl.GetCommonVisibleIndex(rowHandleByVisibleIndexCore);
                int num4 = pair2.Key.DataControl.GetCommonVisibleIndex(rowHandle);
                rootDataControl.DataView.SelectionStrategy.BeginSelection();
                for (int i = commonVisibleIndex; i <= num4; i++)
                {
                    KeyValuePair<DataViewBase, int> pair3 = rootDataControl.FindViewAndVisibleIndexByCommonVisibleIndex(i);
                    if (pair3.Key != null)
                    {
                        int visibleIndex = pair3.Value;
                        int num7 = rootDataControl.GetRowHandleByVisibleIndexCore(visibleIndex);
                        if (rootDataControl.IsGroupRowHandleCore(num7))
                        {
                            if (rootDataControl.DataView.SelectionStrategy.IsCellSelection())
                            {
                                rootDataControl.DataView.SelectionStrategy.SelectCellCore(num7, rootDataControl.DataView.VisibleColumnsCore[0], useSelectionCount);
                            }
                            else if (selectOtherStrategy)
                            {
                                rootDataControl.DataView.SelectionStrategy.SelectRow(num7);
                            }
                        }
                        else
                        {
                            if (rootDataControl.DataView.SelectionStrategy.IsCellSelection())
                            {
                                int actualVisibleIndex;
                                int num9;
                                int num10;
                                int num11;
                                double? nullable3;
                                if (this.cashNewView.ContainsKey(rootDataControl.DataView))
                                {
                                    Tuple<int, int> tuple = this.cashNewView[rootDataControl.DataView];
                                    actualVisibleIndex = tuple.Item1;
                                    num9 = tuple.Item2;
                                }
                                else
                                {
                                    nullable3 = null;
                                    actualVisibleIndex = ((TableViewBehavior) rootDataControl.DataView.ViewBehavior).GetColumnByCoordinateWithOffset(minX, nullable3).ActualVisibleIndex;
                                    nullable3 = null;
                                    num9 = ((TableViewBehavior) rootDataControl.DataView.ViewBehavior).GetColumnByCoordinateWithOffset(maxX, nullable3).ActualVisibleIndex;
                                    this.cashNewView.Add(rootDataControl.DataView, new Tuple<int, int>(actualVisibleIndex, num9));
                                }
                                int? nullable = null;
                                int? nullable2 = null;
                                if (this.OldMasterRectangle == null)
                                {
                                    num10 = actualVisibleIndex;
                                    num11 = num9;
                                }
                                else
                                {
                                    if (this.cashOldView.ContainsKey(rootDataControl.DataView))
                                    {
                                        Tuple<int, int> tuple2 = this.cashOldView[rootDataControl.DataView];
                                        nullable = new int?(tuple2.Item1);
                                        nullable2 = new int?(tuple2.Item2);
                                    }
                                    else
                                    {
                                        nullable3 = null;
                                        nullable = new int?(((TableViewBehavior) rootDataControl.DataView.ViewBehavior).GetColumnByCoordinateWithOffset(this.OldMasterRectangle.StartX, nullable3).ActualVisibleIndex);
                                        nullable3 = null;
                                        nullable2 = new int?(((TableViewBehavior) rootDataControl.DataView.ViewBehavior).GetColumnByCoordinateWithOffset(this.OldMasterRectangle.EndX, nullable3).ActualVisibleIndex);
                                        this.cashOldView.Add(rootDataControl.DataView, new Tuple<int, int>(nullable.Value, nullable2.Value));
                                    }
                                    num10 = Math.Min(actualVisibleIndex, nullable.Value);
                                    num11 = Math.Max(num9, nullable2.Value);
                                }
                                SelectionStrategyBase selectionStrategy = rootDataControl.DataView.SelectionStrategy;
                                for (int j = i; j <= maxIndex; j++)
                                {
                                    KeyValuePair<DataViewBase, int> pair4 = rootDataControl.FindViewAndVisibleIndexByCommonVisibleIndex(j);
                                    if (pair4.Key != null)
                                    {
                                        int num13 = pair4.Value;
                                        int num14 = rootDataControl.GetRowHandleByVisibleIndexCore(num13);
                                        if ((num14 != -2147483645) && (num14 != -2147483647))
                                        {
                                            if (rootDataControl.IsGroupRowHandleCore(num14))
                                            {
                                                if (rootDataControl.DataView.SelectionStrategy.IsCellSelection())
                                                {
                                                    rootDataControl.DataView.SelectionStrategy.SelectCellCore(num14, rootDataControl.DataView.VisibleColumnsCore[0], useSelectionCount);
                                                }
                                                else if (selectOtherStrategy)
                                                {
                                                    rootDataControl.DataView.SelectionStrategy.SelectRow(num14);
                                                }
                                            }
                                            else
                                            {
                                                for (int k = num10; k <= num11; k++)
                                                {
                                                    if ((k >= 0) && (k <= (rootDataControl.DataView.VisibleColumnsCore.Count - 1)))
                                                    {
                                                        ColumnBase column = rootDataControl.DataView.VisibleColumnsCore[k];
                                                        if ((nullable != null) && ((j >= this.OldMasterRectangle.StartVisibleIndex) && ((j <= this.OldMasterRectangle.EndVisibleIndex) && ((column.ActualVisibleIndex >= nullable.Value) && (column.ActualVisibleIndex <= nullable2.Value)))))
                                                        {
                                                            selectionStrategy.UnselectCellCore(num14, column, useSelectionCount);
                                                        }
                                                        if ((column.ActualVisibleIndex >= actualVisibleIndex) && (column.ActualVisibleIndex <= num9))
                                                        {
                                                            selectionStrategy.SelectCellCore(num14, column, useSelectionCount);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                            if (selectOtherStrategy)
                            {
                                rootDataControl.DataView.SelectionStrategy.SelectRow(num7);
                            }
                        }
                    }
                }
                rootDataControl.DataView.SelectionStrategy.EndSelection();
            }
        }

        public sealed override bool ShouldInvertSelectionOnPreviewKeyDown(KeyEventArgs e) => 
            (e.Key == Key.Space) && ((base.view.ActiveEditor == null) && (!base.IsControlPressed ? (!base.view.IsKeyboardFocusInSearchPanel() && (!base.view.IsEditFormVisible && this.ShouldInvertSelectionOnSpace())) : true));

        protected abstract bool ShouldInvertSelectionOnSpace();
        internal override void StartMouseSelection()
        {
            this.OldMasterRectangle = null;
        }

        private void UnselectDetail(DataControlBase dataControl, int rowHandle, int startColIndex, int endColIndex, bool useSelectionCount, bool selectOtherStrategy)
        {
            dataControl.DataView.SelectionStrategy.BeginSelection();
            try
            {
                if (selectOtherStrategy && !dataControl.DataView.SelectionStrategy.IsCellSelection())
                {
                    dataControl.DataView.SelectionStrategy.UnselectRow(rowHandle);
                }
                else if (dataControl.DataView.SelectionStrategy.IsCellSelection())
                {
                    if (dataControl.IsGroupRowHandleCore(rowHandle))
                    {
                        dataControl.DataView.SelectionStrategy.UnselectRow(rowHandle);
                    }
                    else
                    {
                        dataControl.DataView.ViewBehavior.IterateCells(rowHandle, startColIndex, rowHandle, endColIndex, (rHandle, column) => dataControl.DataView.SelectionStrategy.UnselectCellCore(rHandle, column, useSelectionCount));
                    }
                }
            }
            finally
            {
                dataControl.DataView.SelectionStrategy.EndSelection();
            }
        }

        private void UnselectDetailstOldRectange(int startIterate, int endIterate, int minIndex, int maxIndex, DataControlBase rootDataControl, bool useSelectionCount, bool selectOtherStrategy)
        {
            for (int i = startIterate; i < endIterate; i++)
            {
                KeyValuePair<DataViewBase, int> pair = rootDataControl.FindViewAndVisibleIndexByCommonVisibleIndex(i);
                if (pair.Key != null)
                {
                    int actualVisibleIndex;
                    int num5;
                    int visibleIndex = pair.Value;
                    DataViewBase key = pair.Key.OriginationView ?? pair.Key;
                    DataControlBase dataControl = pair.Key.DataControl;
                    int rowHandleByVisibleIndexCore = dataControl.GetRowHandleByVisibleIndexCore(visibleIndex);
                    if (this.cashOldView.ContainsKey(key))
                    {
                        Tuple<int, int> tuple = this.cashOldView[key];
                        actualVisibleIndex = tuple.Item1;
                        num5 = tuple.Item2;
                    }
                    else
                    {
                        double? coord = null;
                        actualVisibleIndex = ((TableViewBehavior) dataControl.DataView.ViewBehavior).GetColumnByCoordinateWithOffset(this.OldMasterRectangle.StartX, coord).ActualVisibleIndex;
                        coord = null;
                        num5 = ((TableViewBehavior) dataControl.DataView.ViewBehavior).GetColumnByCoordinateWithOffset(this.OldMasterRectangle.EndX, coord).ActualVisibleIndex;
                        this.cashOldView.Add(key, new Tuple<int, int>(actualVisibleIndex, num5));
                    }
                    if ((i < minIndex) || (i > maxIndex))
                    {
                        this.UnselectDetail(dataControl, rowHandleByVisibleIndexCore, actualVisibleIndex, num5, useSelectionCount, selectOtherStrategy);
                    }
                }
            }
        }

        protected SelectionStrategyBase.RectangleMaster OldMasterRectangle
        {
            get => 
                base.view.RootView.SelectionStrategy.oldMasterRectangleCore;
            set => 
                base.view.RootView.SelectionStrategy.oldMasterRectangleCore = value;
        }
    }
}

