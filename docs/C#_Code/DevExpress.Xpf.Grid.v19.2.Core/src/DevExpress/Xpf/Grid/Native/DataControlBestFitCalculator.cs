namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Core;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;

    public sealed class DataControlBestFitCalculator : BestFitCalculatorBase
    {
        private DataViewBase view;

        public DataControlBestFitCalculator(DataViewBase view)
        {
            this.view = view;
        }

        public double CalcBandBestFitWidth(BandBase band, bool indentsWidth = true)
        {
            if (band == null)
            {
                return 0.0;
            }
            double minWidth = 0.0;
            if ((band.BandsCore.Count != 0) || (band.ColumnsCore.Count != 0))
            {
                if (band.BandsCore.Count != 0)
                {
                    double result = 0.0;
                    Dictionary<BandBase, double> widthsBandCache = new Dictionary<BandBase, double>();
                    for (int i = 0; i < band.BandsCore.Count; i++)
                    {
                        indentsWidth = false;
                        BandBase base2 = band.BandsCore[i] as BandBase;
                        if (base2.AllowResizing.GetValue(this.TableView.AllowResizing) && this.TableView.AllowBestFit)
                        {
                            double num4 = this.CalcBandBestFitWidth(base2, i == 0);
                            widthsBandCache[base2] = num4;
                            result += num4;
                        }
                    }
                    this.TableView.TableViewBehavior.BestFitLocker.DoLockedAction(delegate {
                        this.View.BeginUpdateColumnsLayout();
                        foreach (BandBase base2 in widthsBandCache.Keys)
                        {
                            this.TableView.TableViewBehavior.ColumnsLayoutCalculator.ApplyResize(base2, widthsBandCache[base2], double.MaxValue, 0.0, true);
                        }
                        this.View.EndUpdateColumnsLayout();
                    });
                    if ((band.AllowBestFit != DefaultBoolean.False) && this.ShouldCalcBestFitBandHeader())
                    {
                        this.CalcHeaderBestFit(band, ref result);
                        result = Math.Ceiling(result);
                    }
                    return result;
                }
                if ((band.ActualRows == null) || (band.ActualRows.Count == 0))
                {
                    return band.MinWidth;
                }
                Dictionary<ColumnBase, double> widthsCache = new Dictionary<ColumnBase, double>();
                int num5 = 0;
                while (true)
                {
                    if (num5 >= band.ActualRows.Count)
                    {
                        this.TableView.TableViewBehavior.BestFitLocker.DoLockedAction(delegate {
                            this.View.BeginUpdateColumnsLayout();
                            foreach (ColumnBase base2 in widthsCache.Keys)
                            {
                                this.TableView.TableViewBehavior.ColumnsLayoutCalculator.ApplyResize(base2, widthsCache[base2], double.MaxValue, 0.0, false);
                            }
                            this.View.EndUpdateColumnsLayout();
                        });
                        break;
                    }
                    double num6 = 0.0;
                    foreach (ColumnBase base3 in band.ActualRows[num5].Columns)
                    {
                        if (this.TableView.TableViewBehavior.CanBestFitColumn(base3))
                        {
                            double num7 = this.CalcColumnBestFitWidthCore((BaseColumn) base3);
                            widthsCache[base3] = num7;
                            num6 += num7;
                        }
                    }
                    minWidth = Math.Max(minWidth, num6);
                    num5++;
                }
            }
            if (band.AllowBestFit == DefaultBoolean.False)
            {
                if (minWidth == 0.0)
                {
                    return double.NaN;
                }
            }
            else
            {
                if (this.ShouldCalcBestFitBandHeader())
                {
                    this.CalcHeaderBestFit(band, ref minWidth);
                }
                if (minWidth == 0.0)
                {
                    minWidth = band.MinWidth;
                }
                minWidth += band.ActualBandLeftSeparatorWidthCore + band.ActualBandRightSeparatorWidthCore;
                if (indentsWidth)
                {
                    minWidth += this.TableView.TableViewBehavior.TreeHeaderIndentsWidth(band);
                }
            }
            return Math.Ceiling(minWidth);
        }

        protected sealed override RowsRange CalcBestFitRowsRange(int rowCount) => 
            new RowsRange(Math.Min(Math.Max(0, this.TableView.TableViewBehavior.GetTopRow(this.View.PageVisibleTopRowIndex + this.View.PageOffset)), (this.View.IsPagingMode ? (this.View.LastVisibleIndexOnPage + 1) : this.DataProviderBase.DataRowCount) - rowCount), rowCount);

        private RowsRange CalcBestFitVisibleRowsRange(int visibleRowsCount) => 
            new RowsRange(Math.Min(Math.Max(0, this.View.PageVisibleTopRowIndex), this.DataProviderBase.VisibleCount - visibleRowsCount), visibleRowsCount);

        public sealed override double CalcColumnBestFitWidth(IBestFitColumn column) => 
            this.CalcColumnBestFitWidthCore(column as BaseColumn);

        protected sealed override double CalcColumnBestFitWidthCore(IBestFitColumn column)
        {
            double result = 0.0;
            if (column is BaseColumn)
            {
                result = ((BaseColumn) column).MinWidth;
            }
            if (this.View.ShowColumnHeaders && this.ShouldCalcBestFitArea(column, BestFitArea.Header))
            {
                this.CalcHeaderBestFit(column as BaseColumn, ref result);
            }
            if (this.ShouldCalcBestFitArea(column, BestFitArea.Rows))
            {
                base.CalcDataBestFit(column, ref result);
            }
            if (this.ShouldCalcBestFitArea(column, BestFitArea.GroupSummary) && this.ValidGroupSummary(column))
            {
                this.CalcGroupColumnSummaryBestFit(column, ref result);
            }
            if (this.ShouldCalcBestFitArea(column, BestFitArea.GroupFooter) && this.View.ShowGroupSummaryFooter)
            {
                this.CalcGroupFooterBestFit(column, ref result);
            }
            if (this.View.ShowTotalSummary && this.ShouldCalcBestFitArea(column, BestFitArea.TotalSummary))
            {
                this.CalcTotalSummaryBestFit(column, ref result);
            }
            return result;
        }

        internal double CalcColumnBestFitWidthCore(BaseColumn column)
        {
            double num;
            if (column.FixedWidth)
            {
                return column.ActualWidth;
            }
            if (!double.IsNaN(column.BestFitWidth))
            {
                return column.BestFitWidth;
            }
            if (this.View.RootDataPresenter == null)
            {
                return double.NaN;
            }
            try
            {
                LayoutUpdatedHelper.GlobalLocker.Lock();
                num = !(column is IBestFitColumn) ? (!column.IsBand ? double.NaN : this.CalcBandBestFitWidth(column as BandBase, true)) : base.CalcColumnBestFitWidth(column as IBestFitColumn);
            }
            finally
            {
                LayoutUpdatedHelper.GlobalLocker.Unlock();
            }
            return num;
        }

        protected override void CalcDistinctValuesBestFit(FrameworkElement bestFitControl, IBestFitColumn column, ref double result)
        {
            ((BestFitControlBase) bestFitControl).Update(-2147483648);
            base.CalcDistinctValuesBestFit(bestFitControl, column, ref result);
        }

        private void CalcGroupColumnSummaryBestFit(IBestFitColumn column, ref double result)
        {
            FrameworkElement bestFitElement = this.TableView.TableViewBehavior.CreateBestFitGroupControl((ColumnBase) column);
            this.SetBestFitElement(bestFitElement);
            this.GetGroupColumnBestFitDelegate(this.GetBestFitMode(column), column)(bestFitElement, column, ref result);
        }

        private void CalcGroupFooterBestFit(IBestFitColumn column, ref double result)
        {
            FrameworkElement bestFitElement = this.TableView.TableViewBehavior.CreateBestFitGroupFooterSummaryControl((ColumnBase) column);
            this.SetBestFitElement(bestFitElement);
            this.GetGroupFooterBestFitDelegate(this.GetBestFitMode(column), column)(bestFitElement, column, ref result);
        }

        private void CalcHeaderBestFit(BaseColumn column, ref double result)
        {
            GridColumnHeaderBase element = this.TableView.TableViewBehavior.CreateGridColumnHeader();
            BaseGridHeader.SetGridColumn(element, column);
            element.DataContext = column;
            element.ColumnPosition = column.ColumnPosition;
            this.SetBestFitElement(element);
            element.SetIsBestFitElement();
            base.UpdateBestFitResult(element, ref result, this.TableView.TableViewBehavior.ViewInfo.GetHeaderIndentsWidth(column, true));
        }

        private void CalcTotalSummaryBestFit(IBestFitColumn column, ref double result)
        {
            FrameworkElement bestFitElement = this.TableView.TableViewBehavior.CreateGridTotalSummaryControl();
            bestFitElement.DataContext = this.View.HeadersData.GetCellDataByColumn((ColumnBase) column, true, false);
            this.SetBestFitElement(bestFitElement);
            base.UpdateBestFitResult(bestFitElement, ref result);
        }

        protected sealed override BestFitCalculatorBase.RowsBestFitCalculatorBase CreateBestFitCalculator(IEnumerable<int> rows) => 
            new RowsBestFitCalculator(this, rows);

        protected sealed override FrameworkElement CreateBestFitControl(IBestFitColumn column) => 
            this.TableView.TableViewBehavior.CreateBestFitControl((ColumnBase) column);

        private BestFitArea GetBestFitArea(GridColumnBase column) => 
            (column.BestFitArea == BestFitArea.None) ? this.TableView.BestFitArea : column.BestFitArea;

        protected sealed override int GetBestFitMaxRowCount(IBestFitColumn column)
        {
            int itemsOnPage = (column.BestFitMaxRowCount == -1) ? this.TableView.BestFitMaxRowCount : column.BestFitMaxRowCount;
            if ((itemsOnPage == -1) && this.View.IsPagingMode)
            {
                itemsOnPage = this.View.ItemsOnPage;
            }
            return itemsOnPage;
        }

        protected sealed override BestFitMode GetBestFitMode(IBestFitColumn column)
        {
            BestFitMode mode = (column.BestFitMode == BestFitMode.Default) ? this.TableView.BestFitMode : column.BestFitMode;
            return ((!this.View.IsPagingMode || ((mode != BestFitMode.Default) && ((mode != BestFitMode.Smart) && (mode != BestFitMode.DistinctValues)))) ? mode : BestFitMode.AllRows);
        }

        protected override BestFitCalculatorBase.CalcBestFitDelegate GetCalcBestFitDelegate(BestFitMode bestFitMode, IBestFitColumn column)
        {
            if (!this.UseVisibleIndices(bestFitMode))
            {
                return base.GetCalcBestFitDelegate(bestFitMode, column);
            }
            BestFitCalculatorBase.RowsBestFitCalculatorBase base1 = this.CreateBestFitCalculator(this.CalcBestFitVisibleRowsRange(this.VisibleRowCount));
            return new BestFitCalculatorBase.CalcBestFitDelegate(base1.CalcRowsBestFit);
        }

        private BestFitCalculatorBase.CalcBestFitDelegate GetGroupColumnBestFitDelegate(BestFitMode bestFitMode, IBestFitColumn column)
        {
            if ((bestFitMode == BestFitMode.Default) || (bestFitMode == BestFitMode.Smart))
            {
                bestFitMode = base.GetSmartBestFitMode(column);
            }
            if (bestFitMode == BestFitMode.AllRows)
            {
                GroupSummaryBestFitCalculator calculator1 = new GroupSummaryBestFitCalculator(this, new RowsRange(0, this.DataProviderBase.VisibleCount + this.View.CalcGroupSummaryVisibleRowCount()));
                return new BestFitCalculatorBase.CalcBestFitDelegate(calculator1.CalcRowsBestFit);
            }
            GroupSummaryBestFitCalculator calculator2 = new GroupSummaryBestFitCalculator(this, new RowsRange(this.View.PageVisibleTopRowIndex, this.VisibleRowCount));
            return new BestFitCalculatorBase.CalcBestFitDelegate(calculator2.CalcRowsBestFit);
        }

        private BestFitCalculatorBase.CalcBestFitDelegate GetGroupFooterBestFitDelegate(BestFitMode bestFitMode, IBestFitColumn column)
        {
            if ((bestFitMode == BestFitMode.Default) || (bestFitMode == BestFitMode.Smart))
            {
                bestFitMode = base.GetSmartBestFitMode(column);
            }
            if (bestFitMode == BestFitMode.AllRows)
            {
                GroupFooterBestFitCalculator calculator1 = new GroupFooterBestFitCalculator(this, new RowsRange(0, this.DataProviderBase.VisibleCount + this.View.CalcGroupSummaryVisibleRowCount()));
                return new BestFitCalculatorBase.CalcBestFitDelegate(calculator1.CalcRowsBestFit);
            }
            GroupFooterBestFitCalculator calculator2 = new GroupFooterBestFitCalculator(this, new RowsRange(this.View.PageVisibleTopRowIndex, this.VisibleRowCount));
            return new BestFitCalculatorBase.CalcBestFitDelegate(calculator2.CalcRowsBestFit);
        }

        protected sealed override int GetRowCount(IBestFitColumn column) => 
            !this.View.IsPagingMode ? this.DataProviderBase.DataRowCount : this.View.ItemsOnPage;

        protected sealed override BestFitCalculatorBase.CalcBestFitDelegate GetSmartModeCalcBestFitDelegate(IBestFitColumn column)
        {
            CustomBestFitEventArgsBase base2 = this.TableView.TableViewBehavior.RaiseCustomBestFit((ColumnBase) column, base.GetSmartBestFitMode(column));
            if (base2.BestFitRows == null)
            {
                return this.GetCalcBestFitDelegate(base2.BestFitMode, column);
            }
            BestFitCalculatorBase.RowsBestFitCalculatorBase base1 = this.CreateBestFitCalculator(base2.BestFitRows);
            return new BestFitCalculatorBase.CalcBestFitDelegate(base1.CalcRowsBestFit);
        }

        protected sealed override object[] GetUniqueValues(IBestFitColumn column) => 
            this.DataProviderBase.GetUniqueColumnValues((ColumnBase) column, false, true, false, null);

        protected sealed override void SetBestFitElement(FrameworkElement bestFitElement)
        {
            this.TableView.TableViewBehavior.BestFitControlDecorator.Child = bestFitElement;
        }

        private bool ShouldCalcBestFitArea(IBestFitColumn column, BestFitArea testArea) => 
            (this.GetBestFitArea((GridColumnBase) column) & testArea) > BestFitArea.None;

        private bool ShouldCalcBestFitBandHeader() => 
            (this.TableView != null) && (this.TableView.ShowBandsPanel && ((this.TableView.BestFitArea & BestFitArea.Header) > BestFitArea.None));

        protected sealed override void UpdateBestFitControl(FrameworkElement bestFitControl, IBestFitColumn column, object cellValue)
        {
            ((BestFitControlBase) bestFitControl).UpdateValue(cellValue);
        }

        private bool UseVisibleIndices(BestFitMode bestFitMode) => 
            (bestFitMode == BestFitMode.VisibleRows) && this.View.CanUseVisibleIndicesForBestFit;

        private bool ValidGroupSummary(IBestFitColumn column)
        {
            ColumnBase base2 = column as ColumnBase;
            return ((this.View != null) && ((base2 != null) && this.View.ShouldCalcBestFitGroupSummary(base2)));
        }

        protected sealed override bool IsServerMode =>
            this.view.DataControl.IsServerMode || this.View.DataControl.DataProviderBase.IsVirtualSource;

        private DataViewBase View =>
            this.view;

        private ITableView TableView =>
            (ITableView) this.View;

        private DevExpress.Xpf.Data.DataProviderBase DataProviderBase =>
            this.View.DataProviderBase;

        protected sealed override int VisibleRowCount
        {
            get
            {
                int num = Math.Min(this.View.PageVisibleDataRowCount, this.View.RootDataPresenter.ViewPort);
                return (!this.View.IsPagingMode ? ((num > 10) ? num : 30) : num);
            }
        }

        private class GroupFooterBestFitCalculator : BestFitCalculatorBase.RowsBestFitCalculatorBase
        {
            public GroupFooterBestFitCalculator(BestFitCalculatorBase owner, IEnumerable<int> rows) : base(owner, rows)
            {
            }

            protected override bool IsValidRowHandle(int scrollIndex) => 
                this.View.DataProviderBase.GetVisibleIndexByScrollIndex(scrollIndex) is GroupSummaryRowKey;

            protected override void UpdateBestFitControl(FrameworkElement bestFitControl, IBestFitColumn column, int scrollIndex)
            {
                GroupSummaryRowKey visibleIndexByScrollIndex = (GroupSummaryRowKey) this.View.DataProviderBase.GetVisibleIndexByScrollIndex(scrollIndex);
                this.TableView.TableViewBehavior.UpdateBestFitGroupFooterSummaryControl((BestFitControlBase) bestFitControl, visibleIndexByScrollIndex.RowHandle.Value, column);
            }

            protected DataControlBestFitCalculator Owner =>
                (DataControlBestFitCalculator) base.Owner;

            protected DataViewBase View =>
                this.Owner.View;

            protected ITableView TableView =>
                this.View as ITableView;
        }

        private class GroupSummaryBestFitCalculator : DataControlBestFitCalculator.GroupFooterBestFitCalculator
        {
            private bool isValidCore;

            public GroupSummaryBestFitCalculator(BestFitCalculatorBase owner, IEnumerable<int> rows) : base(owner, rows)
            {
                this.isValidCore = ((base.View != null) && ((base.View.DataProviderBase != null) && (base.View.DataControl != null))) && (base.View.DataProviderBase.GroupedColumnCount > 0);
            }

            protected override bool IsValidRowHandle(int scrollIndex) => 
                this.isValidCore && base.View.DataControl.IsGroupRowHandleCore(base.View.DataControl.GetRowHandleByVisibleIndexCore(scrollIndex));

            protected override void UpdateBestFitControl(FrameworkElement bestFitControl, IBestFitColumn column, int scrollIndex)
            {
                int rowHandleByVisibleIndexCore = base.View.DataControl.GetRowHandleByVisibleIndexCore(scrollIndex);
                base.TableView.TableViewBehavior.SetGroupElementsForBestFit(bestFitControl, column, rowHandleByVisibleIndexCore);
            }
        }

        private class RowsBestFitCalculator : BestFitCalculatorBase.RowsBestFitCalculatorBase
        {
            public RowsBestFitCalculator(BestFitCalculatorBase owner, IEnumerable<int> rows) : base(owner, rows)
            {
            }

            public override void CalcRowsBestFit(FrameworkElement bestFitControl, IBestFitColumn column, ref double result)
            {
                if (!this.Owner.UseVisibleIndices(this.Owner.GetBestFitMode(column)))
                {
                    base.CalcRowsBestFit(bestFitControl, column, ref result);
                }
                else
                {
                    foreach (int num in base.Rows)
                    {
                        int rowHandleByVisibleIndexCore = this.View.DataControl.GetRowHandleByVisibleIndexCore(num);
                        if (this.IsValidRowHandleCore(rowHandleByVisibleIndexCore))
                        {
                            this.UpdateBestFitControl(bestFitControl, column, rowHandleByVisibleIndexCore);
                            this.Owner.UpdateBestFitResult(bestFitControl, ref result);
                        }
                    }
                }
            }

            protected override bool IsFocusedCell(IBestFitColumn column, int rowHandle) => 
                (this.View.FocusedRowHandle == rowHandle) && ReferenceEquals(this.View.DataControl.CurrentColumn, column);

            protected override bool IsValidRowHandle(int rowHandle) => 
                !this.View.CanUseVisibleIndicesForBestFit ? this.IsValidRowHandleCore(rowHandle) : (this.View.ShouldBestFitCollapsedRows ? !this.Owner.DataProviderBase.IsFilteredByRowHandle(rowHandle) : this.Owner.DataProviderBase.IsRowVisible(rowHandle));

            private bool IsValidRowHandleCore(int rowHandle) => 
                this.Owner.DataProviderBase.IsValidRowHandle(rowHandle) && !this.Owner.DataProviderBase.IsGroupRowHandle(rowHandle);

            protected override void UpdateBestFitControl(FrameworkElement bestFitControl, IBestFitColumn column, int rowHandle)
            {
                ((BestFitControlBase) bestFitControl).Update(rowHandle);
                ((BestFitControlBase) bestFitControl).UpdateIsFocusedCell(this.IsFocusedCell(column, rowHandle));
            }

            protected DataControlBestFitCalculator Owner =>
                (DataControlBestFitCalculator) base.Owner;

            protected DataViewBase View =>
                this.Owner.View;
        }
    }
}

