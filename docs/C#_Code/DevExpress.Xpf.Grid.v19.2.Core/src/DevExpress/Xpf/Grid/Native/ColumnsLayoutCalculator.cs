namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class ColumnsLayoutCalculator
    {
        private GridViewInfo viewInfo;
        private double[] extWidth;

        public ColumnsLayoutCalculator(GridViewInfo viewInfo)
        {
            this.viewInfo = viewInfo;
        }

        public virtual void ApplyBestFit(BaseColumn resizeColumn, double newWidth, bool correctWidths)
        {
            this.ApplyResize(resizeColumn, newWidth, double.MaxValue, 0.0, correctWidths);
        }

        public void ApplyResize(BaseColumn resizeColumn, double newWidth, double maxWidth)
        {
            int visibleIndex = this.GetVisibleIndex(resizeColumn);
            this.ApplyResize(resizeColumn, newWidth, maxWidth, this.ViewInfo.GetHeaderIndentsWidth(resizeColumn, true));
        }

        public void ApplyResize(BaseColumn resizeColumn, double newWidth, double maxWidth, double indentWidth)
        {
            this.ApplyResize(resizeColumn, newWidth, maxWidth, indentWidth, true);
        }

        public virtual void ApplyResize(BaseColumn resizeColumn, double newWidth, double maxWidth, double indentWidth, bool correctWidths)
        {
            if (double.IsNaN(newWidth))
            {
                this.SetDefaultWidths();
            }
            else if (resizeColumn.GetAllowResizing())
            {
                Action<DataViewBase> updateMethod = <>c.<>9__39_0;
                if (<>c.<>9__39_0 == null)
                {
                    Action<DataViewBase> local1 = <>c.<>9__39_0;
                    updateMethod = <>c.<>9__39_0 = view => view.BeginUpdateColumnsLayout();
                }
                this.ViewInfo.GridView.UpdateAllDependentViews(updateMethod);
                this.ApplyResizeCore(resizeColumn, newWidth, maxWidth, indentWidth, correctWidths);
                Action<DataViewBase> action2 = <>c.<>9__39_1;
                if (<>c.<>9__39_1 == null)
                {
                    Action<DataViewBase> local2 = <>c.<>9__39_1;
                    action2 = <>c.<>9__39_1 = view => view.EndUpdateColumnsLayout();
                }
                this.ViewInfo.GridView.UpdateAllDependentViews(action2);
            }
        }

        protected virtual void ApplyResizeCore(BaseColumn resizeColumn, double newWidth, double maxWidth, double indentWidth, bool correctWidths)
        {
            newWidth = this.CorrectNewWidth(resizeColumn, newWidth);
            resizeColumn.ActualWidth = Math.Max((double) 0.0, (double) (newWidth - indentWidth));
        }

        public void CalcActualLayout(Size arrangeBounds)
        {
            this.CalcActualLayout(arrangeBounds, LayoutAssigner.Default, this.TableViewBehavior.TableView.ActualShowIndicator, false, false);
        }

        public void CalcActualLayout(Size arrangeBounds, LayoutAssigner layoutAssigner, bool showIndicator, bool needRoundingLastColumn, bool ignoreDetailButtons)
        {
            if (!this.ViewInfo.GridView.IsLockUpdateColumnsLayout)
            {
                double arrangeWidth = this.GetArrangeWidth(arrangeBounds, layoutAssigner, showIndicator, ignoreDetailButtons);
                this.CalcActualLayoutCore(arrangeWidth, layoutAssigner, showIndicator, needRoundingLastColumn, ignoreDetailButtons);
                layoutAssigner.CreateLayout(this);
                this.UpdateAdditionalRowDataWidth();
            }
        }

        protected virtual void CalcActualLayoutCore(double arrangeWidth, LayoutAssigner layoutAssigner, bool showIndicator, bool needRoundingLastColumn, bool ignoreDetailButtons)
        {
            this.SetActualColumnWidth(this.VisibleColumns, layoutAssigner);
        }

        private double CalcAutoWidthHorizontalViewPort()
        {
            Func<ColumnBase, bool> predicate = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Func<ColumnBase, bool> local1 = <>c.<>9__19_0;
                predicate = <>c.<>9__19_0 = c => c.Fixed == FixedStyle.Left;
            }
            bool flag = this.VisibleColumns.Where<ColumnBase>(predicate).ToList<ColumnBase>().Count > 0;
            Func<ColumnBase, bool> func2 = <>c.<>9__19_1;
            if (<>c.<>9__19_1 == null)
            {
                Func<ColumnBase, bool> local2 = <>c.<>9__19_1;
                func2 = <>c.<>9__19_1 = c => c.Fixed == FixedStyle.Right;
            }
            bool flag2 = this.VisibleColumns.Where<ColumnBase>(func2).ToList<ColumnBase>().Count > 0;
            bool flag3 = flag | flag2;
            double num = 0.0;
            if (this.viewInfo.TableView.ActualShowIndicator)
            {
                num += this.ViewInfo.TableView.IndicatorHeaderWidth;
            }
            if ((!this.viewInfo.TableView.ActualShowIndicator && !flag) & flag2)
            {
                num += this.ViewInfo.TableView.LeftDataAreaIndent;
            }
            if ((!this.viewInfo.TableView.ActualShowDetailButtons && !flag) & flag2)
            {
                num += this.ViewInfo.TotalGroupAreaIndent;
            }
            if (this.viewInfo.TableView.ActualShowDetailButtons & flag3)
            {
                num += this.ViewInfo.TotalGroupAreaIndent;
            }
            if (this.viewInfo.TableView.ActualShowDetailButtons)
            {
                num += this.ViewInfo.TableView.ActualExpandDetailButtonWidth;
            }
            return this.CorrectValueOnFixedLineWidth((this.AvailableWidth - num) - 1.0);
        }

        public virtual double CalcColumnMaxWidth(ColumnBase column) => 
            double.MaxValue;

        protected double CalcHorizontalViewPort()
        {
            double num = this.ViewInfo.TableView.LeftDataAreaIndent + this.ViewInfo.TotalGroupAreaIndent;
            if (this.viewInfo.TableView.ActualShowIndicator)
            {
                num += this.ViewInfo.TableView.IndicatorWidth;
            }
            if (this.viewInfo.TableView.ActualShowDetailButtons)
            {
                num += this.ViewInfo.TableView.ActualExpandDetailButtonWidth;
            }
            return this.CorrectValueOnFixedLineWidth((this.AvailableWidth - num) - 1.0);
        }

        private double CalcTotalFixedWidth()
        {
            double num = 0.0;
            for (int i = 0; i < this.VisibleColumns.Count; i++)
            {
                if (this.VisibleColumns[i].Fixed != FixedStyle.None)
                {
                    num += this.VisibleColumns[i].ActualHeaderWidth - this.ViewInfo.GetHeaderIndentsWidth(this.VisibleColumns[i], true);
                }
            }
            return num;
        }

        protected double CorrectDetailExpandButtonAndVerticalScrollBarWidth(double value)
        {
            double num = this.ViewInfo.TableView.TableViewBehavior.GetTotalLeftIndent(false, false) + this.ViewInfo.TableView.TableViewBehavior.GetTotalRightIndent();
            return Math.Max((double) 0.0, (double) (value - num));
        }

        protected virtual void CorrectFixedColumnsWidth()
        {
            double num2 = this.CalcHorizontalViewPort();
            if ((num2 < this.CalcTotalFixedWidth()) && (num2 > 0.0))
            {
                Func<ColumnBase, bool> predicate = <>c.<>9__23_0;
                if (<>c.<>9__23_0 == null)
                {
                    Func<ColumnBase, bool> local1 = <>c.<>9__23_0;
                    predicate = <>c.<>9__23_0 = c => c.Fixed != FixedStyle.None;
                }
                AutoWidthHelper.CalcColumnLayout(this.VisibleColumns.Where<ColumnBase>(predicate).ToList<ColumnBase>(), this.CalcAutoWidthHorizontalViewPort(), this.viewInfo, LayoutAssigner.Default, false, true);
            }
        }

        private double CorrectNewWidth(BaseColumn column, double width)
        {
            if (!this.TableViewBehavior.BestFitLocker.IsLocked && (column.Fixed != FixedStyle.None))
            {
                double num = this.CalcTotalFixedWidth();
                double num2 = this.CalcHorizontalViewPort();
                double num3 = (num + width) - column.ActualHeaderWidth;
                int num4 = 0;
                while (true)
                {
                    if (num4 >= this.VisibleColumns.Count)
                    {
                        if ((num3 > num2) && (num2 > 0.0))
                        {
                            width = (num2 - num) + column.ActualHeaderWidth;
                        }
                        break;
                    }
                    if (this.VisibleColumns[num4].Fixed != FixedStyle.None)
                    {
                        this.VisibleColumns[num4].ActualWidth = this.VisibleColumns[num4].ActualHeaderWidth - this.ViewInfo.GetHeaderIndentsWidth(this.VisibleColumns[num4], true);
                    }
                    num4++;
                }
            }
            return width;
        }

        private double CorrectValueOnFixedLineWidth(double value)
        {
            if (this.TableViewBehavior.HasFixedLeftElements)
            {
                value -= DXArranger.Round(this.ViewInfo.TableView.FixedLineWidth, 0);
            }
            if (this.TableViewBehavior.HasFixedRightElements)
            {
                value -= DXArranger.Round(this.ViewInfo.TableView.FixedLineWidth, 0);
            }
            return value;
        }

        protected internal virtual void CreateLayout()
        {
            this.CorrectFixedColumnsWidth();
            double totalFixedNoneSize = 0.0;
            double totalFixedLeftSize = 0.0;
            double totalFixedRightSize = 0.0;
            this.UpdateColumnDataWidths(out totalFixedNoneSize, out totalFixedLeftSize, out totalFixedRightSize);
            double num4 = ((this.ViewInfo.GridView.RowMarginControlDisplayMode == RowMarginControlDisplayMode.InCellsControl) ? 0.0 : this.ViewInfo.TotalGroupAreaIndent) + totalFixedLeftSize;
            double num5 = 0.0;
            double num6 = 0.0;
            if (this.ViewInfo.TableView.ActualAllowTreeIndentScrolling)
            {
                num6 = num4;
            }
            else
            {
                num5 = num4;
            }
            double num7 = this.ViewInfo.RightGroupAreaIndent + totalFixedRightSize;
            double num8 = Math.Max(0.0, this.CorrectValueOnFixedLineWidth((this.AvailableWidth - num5) - num7));
            if (this.ViewInfo.TableView.ActualShowIndicator)
            {
                num8 -= this.ViewInfo.TableView.IndicatorWidth;
            }
            num8 = Math.Max(num8, 1.0);
            num8 = this.CorrectDetailExpandButtonAndVerticalScrollBarWidth(num8);
            if (double.IsInfinity(num8))
            {
                num8 = totalFixedNoneSize;
            }
            this.TableViewBehavior.HorizontalExtent = totalFixedNoneSize + num6;
            this.TableViewBehavior.LeftIndent = num4;
            this.TableViewBehavior.RightIndent = num7;
            this.TableViewBehavior.TableView.SetHorizontalViewport(num8);
            this.ViewInfo.GridView.AdditionalElementWidth = Math.Max((double) 0.0, (double) ((this.ViewInfo.ColumnsLayoutSize.Width - this.ViewInfo.GridView.ActualColumnsWidth) - this.ViewInfo.GridView.AdditionalElementOffset));
            this.ViewInfo.TableView.IndicatorHeaderWidth = this.ViewInfo.TableView.IndicatorWidth + this.ViewInfo.TableView.LeftDataAreaIndent;
            double expandDetailButtonWidth = (this.ViewInfo.TableView.ActualShowDetailButtons ? this.ViewInfo.TableView.ActualExpandDetailButtonWidth : 0.0) + this.TableViewBehavior.ActualFixRowButtonWidth;
            if (expandDetailButtonWidth > 0.0)
            {
                expandDetailButtonWidth += this.ViewInfo.TotalGroupAreaIndent;
            }
            this.ViewInfo.TableView.SetActualExpandDetailHeaderWidth(expandDetailButtonWidth);
            double num10 = Math.Min(num8, totalFixedNoneSize + num6);
            if (totalFixedLeftSize == 0.0)
            {
                num10 += num5;
                if (this.ViewInfo.TableView.ActualShowDetailHeader)
                {
                    num10 -= this.ViewInfo.TotalGroupAreaIndent;
                }
                if (!this.ViewInfo.TableView.ActualShowIndicator)
                {
                    num10 += this.ViewInfo.TableView.LeftDataAreaIndent;
                }
            }
            if (totalFixedRightSize == 0.0)
            {
                num10 += this.TableViewBehavior.RightIndent;
            }
            this.ViewInfo.TableView.FixedNoneContentWidth = num10;
            this.ViewInfo.TableView.FixedLeftContentWidth = Math.Max(0.0, totalFixedLeftSize);
            this.ViewInfo.TableView.FixedRightContentWidth = Math.Max(0.0, totalFixedRightSize);
            if (this.ViewInfo.TableView.ViewBase.GetIsCompactMode())
            {
                UpdateRowDataDelegate updateMethod = <>c.<>9__25_0;
                if (<>c.<>9__25_0 == null)
                {
                    UpdateRowDataDelegate local1 = <>c.<>9__25_0;
                    updateMethod = <>c.<>9__25_0 = x => x.UpdateClientFixedNoneContentWidth();
                }
                this.ViewInfo.TableView.ViewBase.UpdateRowData(updateMethod, true, true);
            }
            this.ViewInfo.TableView.TotalGroupAreaIndent = (this.ViewInfo.GridView.RowMarginControlDisplayMode == RowMarginControlDisplayMode.InCellsControl) ? 0.0 : this.ViewInfo.TotalGroupAreaIndent;
            this.ViewInfo.TableView.TotalSummaryFixedNoneContentWidth = this.GetTotalSummaryFixedNoneContentWidth();
            this.ViewInfo.TableView.VerticalScrollBarWidth = this.ViewInfo.VerticalScrollBarWidth;
            this.UpdateColumnsActualAllowResizing();
            this.FillByLastFixedColumn();
        }

        protected virtual void FillByLastFixedColumn()
        {
            this.TableViewBehavior.FillByLastFixedColumn(this.GetArrangeWidth(this.ViewInfo.ColumnsLayoutSize, LayoutAssigner.Default, this.TableViewBehavior.TableView.ShowIndicator, false));
        }

        protected virtual double GetArrangeWidth(Size arrangeBounds, LayoutAssigner layoutAssigner, bool showIndicator, bool ignoreDetailButtons)
        {
            double num = Math.Min(arrangeBounds.Width - this.ViewInfo.RightGroupAreaIndent, this.ViewInfo.GridView.MaxAvailableWidth);
            if (showIndicator)
            {
                num -= this.ViewInfo.TableView.ActualIndicatorWidth;
            }
            if (this.TableViewBehavior.HasFixedLeftElements && layoutAssigner.UseFixedColumnIndents)
            {
                num -= this.ViewInfo.TableView.FixedLineWidth;
            }
            if (this.TableViewBehavior.HasFixedRightElements && layoutAssigner.UseFixedColumnIndents)
            {
                num -= this.ViewInfo.TableView.FixedLineWidth;
            }
            if (layoutAssigner.UseDetailButtonsIndents)
            {
                num = this.CorrectDetailExpandButtonAndVerticalScrollBarWidth(num);
            }
            if (this.ViewInfo.TableView.ActualShowDetailHeader && ((num >= this.ViewInfo.TotalGroupAreaIndent) && !ignoreDetailButtons))
            {
                num -= this.ViewInfo.TotalGroupAreaIndent;
            }
            if (!this.ViewInfo.TableView.ShowIndicator && layoutAssigner.UseDataAreaIndent)
            {
                num += this.ViewInfo.TableView.LeftDataAreaIndent;
            }
            return num;
        }

        private double GetHeaderIndentsWidth(ColumnBase column) => 
            (this.ViewInfo.GridView.RowMarginControlDisplayMode != RowMarginControlDisplayMode.InCellsControl) ? this.ViewInfo.GetHeaderIndentsWidth(column, true) : (!this.ViewInfo.TableView.TableViewBehavior.IsFirstColumn(column, this.ViewInfo.IsPrinting) ? 0.0 : this.ViewInfo.ActualLeftDataAreaIndent);

        private double GetTotalSummaryFixedNoneContentWidth()
        {
            double fixedNoneContentWidth = this.ViewInfo.TableView.FixedNoneContentWidth;
            if ((this.ViewInfo.TableView.FixedLeftContentWidth != 0.0) && (this.ViewInfo.TableView.FixedRightContentWidth != 0.0))
            {
                return fixedNoneContentWidth;
            }
            if (this.ViewInfo.TableView.ViewBase.IsRootView && this.ViewInfo.TableView.ActualShowIndicator)
            {
                fixedNoneContentWidth += this.ViewInfo.TableView.ActualIndicatorWidth;
            }
            fixedNoneContentWidth += this.ViewInfo.TableView.ActualExpandDetailHeaderWidth;
            return ((this.ViewInfo.TableView.FixedRightContentWidth == 0.0) ? (fixedNoneContentWidth + this.ViewInfo.VerticalScrollBarWidth) : fixedNoneContentWidth);
        }

        protected int GetVisibleIndex(BaseColumn column) => 
            BaseColumn.GetActualVisibleIndex(column);

        internal void ResetBestFitBandWidth(BandBase band)
        {
            if (band != null)
            {
                band.ActualWidth = double.NaN;
                foreach (BandBase base2 in band.BandsCore)
                {
                    this.ResetBestFitBandWidth(base2);
                }
            }
        }

        protected void SetActualColumnWidth(IEnumerable columns, LayoutAssigner layoutAssigner)
        {
            double rest = 0.0;
            foreach (BaseColumn column in columns)
            {
                layoutAssigner.SetWidth(column, AutoWidthHelper.GetRoundedActualColumnWidth(this.ViewInfo.GetColumnHeaderWidth(column), ref rest, false));
            }
        }

        protected virtual void SetDefaultWidths()
        {
            foreach (ColumnBase base2 in this.VisibleColumns)
            {
                GridColumnWidth width = base2.Width;
                base2.ActualWidth = width.Value;
            }
            this.ViewInfo.GridView.UpdateColumnsPositions();
        }

        protected virtual void UpdateAdditionalRowDataWidth()
        {
            for (int i = 0; i < this.VisibleColumns.Count; i++)
            {
                this.VisibleColumns[i].ActualAdditionalRowDataWidth = this.VisibleColumns[i].ActualHeaderWidth - ((this.ViewInfo.TableView.ShowIndicator || (i != 0)) ? 0.0 : this.ViewInfo.TableView.LeftDataAreaIndent);
            }
        }

        private void UpdateColumnDataWidths(out double totalFixedNoneSize, out double totalFixedLeftSize, out double totalFixedRightSize)
        {
            totalFixedNoneSize = 0.0;
            totalFixedLeftSize = 0.0;
            totalFixedRightSize = 0.0;
            for (int i = 0; i < this.VisibleColumns.Count; i++)
            {
                this.VisibleColumns[i].ActualDataWidth = this.VisibleColumns[i].ActualHeaderWidth - this.GetHeaderIndentsWidth(this.VisibleColumns[i]);
            }
            this.TableViewBehavior.UpdateColumnDataWidths(out totalFixedNoneSize, out totalFixedLeftSize, out totalFixedRightSize);
        }

        protected virtual void UpdateColumnsActualAllowResizing()
        {
            for (int i = 0; i < this.VisibleColumns.Count; i++)
            {
                this.VisibleColumns[i].UpdateActualAllowResizing();
            }
        }

        protected internal virtual void UpdateHasLeftRightSibling(IList<ColumnBase> columns)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].HasRightSibling = i < (columns.Count - 1);
                columns[i].HasLeftSibling = i != 0;
            }
        }

        internal void VisibleColumnsChanged()
        {
            if ((this.VisibleColumns != null) && ((this.extWidth != null) && (this.VisibleColumns.Count != this.extWidth.Length)))
            {
                this.extWidth = null;
            }
        }

        protected double[] ExtWidth
        {
            get
            {
                if ((this.extWidth == null) && (this.VisibleColumns.Count > 0))
                {
                    this.extWidth = new double[this.VisibleColumns.Count];
                }
                return this.extWidth;
            }
        }

        protected virtual GridViewInfo ViewInfo =>
            this.viewInfo;

        protected DevExpress.Xpf.Grid.Native.TableViewBehavior TableViewBehavior =>
            this.ViewInfo.TableView.TableViewBehavior;

        protected IList<ColumnBase> VisibleColumns =>
            this.ViewInfo.VisibleColumns;

        public virtual bool AutoWidth =>
            false;

        private double AvailableWidth =>
            Math.Min(this.ViewInfo.ColumnsLayoutSize.Width, this.ViewInfo.GridView.MaxAvailableWidth);

        protected internal virtual bool SupportStarColumns =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ColumnsLayoutCalculator.<>c <>9 = new ColumnsLayoutCalculator.<>c();
            public static Func<ColumnBase, bool> <>9__19_0;
            public static Func<ColumnBase, bool> <>9__19_1;
            public static Func<ColumnBase, bool> <>9__23_0;
            public static UpdateRowDataDelegate <>9__25_0;
            public static Action<DataViewBase> <>9__39_0;
            public static Action<DataViewBase> <>9__39_1;

            internal void <ApplyResize>b__39_0(DataViewBase view)
            {
                view.BeginUpdateColumnsLayout();
            }

            internal void <ApplyResize>b__39_1(DataViewBase view)
            {
                view.EndUpdateColumnsLayout();
            }

            internal bool <CalcAutoWidthHorizontalViewPort>b__19_0(ColumnBase c) => 
                c.Fixed == FixedStyle.Left;

            internal bool <CalcAutoWidthHorizontalViewPort>b__19_1(ColumnBase c) => 
                c.Fixed == FixedStyle.Right;

            internal bool <CorrectFixedColumnsWidth>b__23_0(ColumnBase c) => 
                c.Fixed != FixedStyle.None;

            internal void <CreateLayout>b__25_0(RowData x)
            {
                x.UpdateClientFixedNoneContentWidth();
            }
        }
    }
}

