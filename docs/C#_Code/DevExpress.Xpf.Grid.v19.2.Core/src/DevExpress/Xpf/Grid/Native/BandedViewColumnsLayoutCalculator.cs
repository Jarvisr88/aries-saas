namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class BandedViewColumnsLayoutCalculator : ColumnsLayoutCalculator
    {
        public BandedViewColumnsLayoutCalculator(GridViewInfo viewInfo) : base(viewInfo)
        {
        }

        protected override void ApplyResizeCore(BaseColumn resizeColumn, double newWidth, double maxWidth, double indentWidth, bool correctWidths)
        {
            ColumnBase column = resizeColumn as ColumnBase;
            if (!correctWidths)
            {
                if (resizeColumn.Visible && this.ViewInfo.GridView.IsColumnVisibleInHeaders(resizeColumn))
                {
                    resizeColumn.ActualWidth = Math.Max((double) 0.0, (double) (this.GetColumnWidth(resizeColumn) + this.CorrectColumnDelta(column, newWidth - this.GetColumnWidth(resizeColumn), FixedStyle.None, correctWidths)));
                }
                else
                {
                    resizeColumn.ActualWidth = newWidth;
                }
            }
            else
            {
                double delta = newWidth - this.GetColumnHeaderWidth(resizeColumn);
                if (resizeColumn.IsBand)
                {
                    delta = this.ChangeBandSize(resizeColumn.ParentBandInternal, delta);
                }
                else
                {
                    delta = this.ChangeColumnSize(column, delta, resizeColumn.BandRow.Columns);
                    foreach (BandRow row in column.ParentBand.ActualRows)
                    {
                        if (!ReferenceEquals(row, resizeColumn.BandRow))
                        {
                            this.ChangeColumnSize(null, delta, row.Columns);
                        }
                    }
                }
                this.OnBandResize(resizeColumn.ParentBandInternal, delta);
            }
        }

        protected override void CalcActualLayoutCore(double arrangeWidth, LayoutAssigner layoutAssigner, bool showIndicator, bool needRoundingLastColumn, bool ignoreDetailButtons)
        {
            this.ResetActualColumnHeaders(this.BandsLayout.VisibleBands, layoutAssigner);
            base.SetActualColumnWidth(this.GetSizeableColumns(false), layoutAssigner);
            foreach (BandBase base2 in this.BandsLayout.VisibleBands)
            {
                this.RecalcBandLayout(base2, layoutAssigner);
            }
        }

        private double CalcColumnsWidth(IList list, Func<BaseColumn, int, bool> skipColumn, bool useHeaderIndent = false)
        {
            double num = 0.0;
            for (int i = 0; i < list.Count; i++)
            {
                BaseColumn column = (BaseColumn) list[i];
                if (!skipColumn(column, i))
                {
                    num += useHeaderIndent ? this.GetColumnHeaderWidth(column) : this.GetColumnWidth(column);
                }
            }
            return num;
        }

        private double CalcColumnWidth(BaseColumn column, double delta, double sizeableWidth) => 
            Math.Max(this.GetColumnWidth(column) + (((this.GetColumnWidth(column) - column.MinWidth) * delta) / sizeableWidth), column.MinWidth);

        private double CalcColumnWidth(BaseColumn column, double delta, int unfixedColumnsCount) => 
            Math.Max(this.GetColumnWidth(column) + (delta / ((double) unfixedColumnsCount)), column.MinWidth);

        private double CalcMinSizeableWidth(ColumnBase resizeColumn)
        {
            double maxValue = double.MaxValue;
            foreach (BandRow row in resizeColumn.ParentBand.ActualRows)
            {
                if (!ReferenceEquals(row, resizeColumn.BandRow))
                {
                    maxValue = Math.Min(maxValue, this.CalcSizeableWidth(-1, row.Columns, true));
                }
            }
            return maxValue;
        }

        private double CalcSizeableWidth(IList bands)
        {
            double num = 0.0;
            foreach (BandBase base2 in bands)
            {
                if (base2.VisibleBands.Count != 0)
                {
                    num += this.CalcSizeableWidth(base2.VisibleBands);
                    continue;
                }
                if (base2.ActualRows.Count != 0)
                {
                    num += this.CalcSizeableWidth(-1, this.GetBandRow(base2, false).Columns, false);
                    continue;
                }
                if (!base2.FixedWidth)
                {
                    num += this.GetColumnWidth(base2) - base2.MinWidth;
                }
            }
            return num;
        }

        protected double CalcSizeableWidth(int columnIndex, IList columns, bool isLeft)
        {
            double num = 0.0;
            for (int i = 0; i < columns.Count; i++)
            {
                if (!this.BandsLayout.SkipItem(columnIndex, i, isLeft))
                {
                    BaseColumn column = (BaseColumn) columns[i];
                    if (!column.FixedWidth)
                    {
                        num += this.GetColumnWidth(column) - column.MinWidth;
                    }
                }
            }
            return num;
        }

        private int CalcUnfixedColumnsCount(IList bands)
        {
            int num = 0;
            foreach (BandBase base2 in bands)
            {
                if (base2.VisibleBands.Count != 0)
                {
                    num += this.CalcUnfixedColumnsCount(base2.VisibleBands);
                    continue;
                }
                if (base2.ActualRows.Count != 0)
                {
                    if (!this.CanBeResized(base2))
                    {
                        continue;
                    }
                    num += this.CalcUnfixedColumnsCount(-1, this.GetBandRow(base2, false).Columns, false);
                    continue;
                }
                if (!base2.FixedWidth)
                {
                    num++;
                }
            }
            return num;
        }

        protected int CalcUnfixedColumnsCount(int columnIndex, IList columns, bool isLeft)
        {
            int num = 0;
            for (int i = 0; i < columns.Count; i++)
            {
                if (!this.BandsLayout.SkipItem(columnIndex, i, isLeft) && !((BaseColumn) columns[i]).FixedWidth)
                {
                    num++;
                }
            }
            return num;
        }

        private bool CanBeResized(BandBase band)
        {
            bool flag3;
            bool flag = false;
            if (band.VisibleBands.Count != 0)
            {
                foreach (BandBase base2 in band.VisibleBands)
                {
                    if (this.CanBeResized(base2))
                    {
                        flag = true;
                    }
                }
                return flag;
            }
            if (band.ActualRows.Count == 0)
            {
                return !band.FixedWidth;
            }
            using (List<BandRow>.Enumerator enumerator2 = band.ActualRows.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator2.MoveNext())
                    {
                        BandRow current = enumerator2.Current;
                        bool flag2 = false;
                        foreach (BaseColumn column in current.Columns)
                        {
                            if (!column.FixedWidth)
                            {
                                flag2 = true;
                            }
                        }
                        if (flag2)
                        {
                            continue;
                        }
                        flag3 = false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag3;
        }

        private double ChangeBandSize(BandBase band, double delta)
        {
            delta = this.CorrectBandDelta(band, delta);
            if (delta > 0.0)
            {
                List<BandBase> bands = new List<BandBase>();
                bands.Add(band);
                this.IncreaseBandsWidth(bands, delta);
            }
            else
            {
                List<BandBase> bands = new List<BandBase>();
                bands.Add(band);
                this.DecreaseBandsWidth(bands, delta);
            }
            return -delta;
        }

        protected double ChangeColumnSize(ColumnBase resizeColumn, double delta, IList columns)
        {
            FixedStyle left = FixedStyle.Left;
            if (resizeColumn != null)
            {
                left = this.BandsLayout.GetRootBand(resizeColumn.ParentBand).Fixed;
                delta = this.CorrectColumnDelta(resizeColumn, delta, left, true);
            }
            int index = columns.IndexOf(resizeColumn);
            this.SetColumnsWidth(index, delta, columns);
            return ((delta <= 0.0) ? this.IncreaseColumnsWidth(resizeColumn, delta, columns, index, left == FixedStyle.Right) : this.DecreaseColumnsWidth(resizeColumn, delta, columns, index, left == FixedStyle.Right));
        }

        protected virtual double CorrectBandDelta(BandBase band, double delta)
        {
            if (!this.CanBeResized(band))
            {
                return 0.0;
            }
            if (this.BandsLayout.GetRootBand(band).Fixed != FixedStyle.None)
            {
                delta = Math.Min(delta, base.TableViewBehavior.HorizontalViewportCore);
            }
            if (this.BandsLayout.GetRootBand(band).Fixed == FixedStyle.Right)
            {
                double num = 0.0;
                foreach (BandBase base2 in this.BandsLayout.GetBands(band, true, true))
                {
                    num += base2.ActualHeaderWidth;
                }
                foreach (BandBase base3 in this.BandsLayout.GetBands(band, false, true))
                {
                    num += base3.ActualHeaderWidth;
                }
                if (band.HasLeftSibling)
                {
                    num += this.ViewInfo.FirstColumnIndent;
                }
                delta = Math.Max(delta, ((this.GetArrangeWidth(this.ViewInfo.ColumnsLayoutSize, LayoutAssigner.Default, base.TableViewBehavior.TableView.ShowIndicator, false) - num) - base.TableViewBehavior.HorizontalExtent) - band.ActualHeaderWidth);
            }
            return (Math.Max(band.ActualHeaderWidth + delta, this.GetBandMinWidth(band, null)) - band.ActualHeaderWidth);
        }

        protected virtual double CorrectColumnDelta(ColumnBase column, double delta, FixedStyle fixedStyle, bool correctWidths)
        {
            if (fixedStyle != FixedStyle.None)
            {
                delta = Math.Min(delta, base.TableViewBehavior.HorizontalViewportCore + this.CalcSizeableWidth(column.BandRow.Columns.IndexOf(column), column.BandRow.Columns, fixedStyle != FixedStyle.Left));
            }
            double minWidth = column.MinWidth;
            int columnIndex = column.BandRow.Columns.IndexOf(column);
            if (column.BandRow.Columns[column.BandRow.Columns.Count - 1] == column)
            {
                minWidth = Math.Max(this.GetBandMinWidth(column.ParentBand, column.BandRow) - this.CalcColumnsWidth(column.BandRow.Columns, (col, index) => index >= columnIndex, true), minWidth) - this.ViewInfo.GetHeaderIndentsWidth(column, false);
            }
            if ((delta > 0.0) && this.HasFixedSizeRow(column))
            {
                delta = Math.Min(delta, this.CalcSizeableWidth(columnIndex, column.BandRow.Columns, false));
            }
            if ((delta < 0.0) && ((column.ParentBand.ActualRows.Count > 1) && !this.HasUnfixedColumn(column.BandRow.Columns, columnIndex + 1)))
            {
                delta = Math.Max(delta, -this.CalcMinSizeableWidth(column));
            }
            return (Math.Max(this.GetColumnWidth(column) + delta, minWidth) - this.GetColumnWidth(column));
        }

        protected override void CorrectFixedColumnsWidth()
        {
            double num = this.GetArrangeWidth(this.ViewInfo.ColumnsLayoutSize, LayoutAssigner.Default, base.TableViewBehavior.TableView.ShowIndicator, false) - this.ViewInfo.TotalGroupAreaIndent;
            Func<BaseColumn, int, bool> skipColumn = <>c.<>9__38_0;
            if (<>c.<>9__38_0 == null)
            {
                Func<BaseColumn, int, bool> local1 = <>c.<>9__38_0;
                skipColumn = <>c.<>9__38_0 = (band, index) => band.Fixed == FixedStyle.None;
            }
            double num2 = this.CalcColumnsWidth(this.BandsLayout.VisibleBands, skipColumn, false);
            Func<BaseColumn, int, bool> func2 = <>c.<>9__38_1;
            if (<>c.<>9__38_1 == null)
            {
                Func<BaseColumn, int, bool> local2 = <>c.<>9__38_1;
                func2 = <>c.<>9__38_1 = (band, index) => band.Fixed != FixedStyle.None;
            }
            double num3 = this.CalcColumnsWidth(this.BandsLayout.VisibleBands, func2, false);
            if (num < num2)
            {
                this.UpdateFixedColumnsWidth(num + (base.TableViewBehavior.HasFixedLeftElements ? this.ViewInfo.TotalGroupAreaIndent : 0.0), false);
            }
            if (num > (num2 + num3))
            {
                Func<BaseColumn, int, bool> func3 = <>c.<>9__38_2;
                if (<>c.<>9__38_2 == null)
                {
                    Func<BaseColumn, int, bool> local3 = <>c.<>9__38_2;
                    func3 = <>c.<>9__38_2 = (band, index) => band.Fixed != FixedStyle.Left;
                }
                this.UpdateFixedColumnsWidth((num - num3) - this.CalcColumnsWidth(this.BandsLayout.VisibleBands, func3, false), true);
            }
        }

        protected void DecreaseBandsWidth(IList bands, double delta)
        {
            double sizeableWidth = this.CalcSizeableWidth(bands);
            if (sizeableWidth != 0.0)
            {
                this.DecreaseBandsWidth(bands, delta, sizeableWidth);
            }
        }

        private void DecreaseBandsWidth(IList bands, double delta, double sizeableWidth)
        {
            foreach (BandBase base2 in bands)
            {
                if (base2.VisibleBands.Count != 0)
                {
                    this.DecreaseBandsWidth(base2.VisibleBands, delta, sizeableWidth);
                    continue;
                }
                if (base2.ActualRows.Count == 0)
                {
                    if (base2.FixedWidth)
                    {
                        continue;
                    }
                    base2.ActualWidth = this.CalcColumnWidth(base2, delta, sizeableWidth);
                    continue;
                }
                double num = 0.0;
                double num2 = 0.0;
                BandRow bandRow = this.GetBandRow(base2, false);
                foreach (BaseColumn column in bandRow.Columns)
                {
                    num += this.GetColumnWidth(column);
                    double columnWidth = this.GetColumnWidth(column);
                    if (!column.FixedWidth)
                    {
                        columnWidth = this.CalcColumnWidth(column, delta, sizeableWidth);
                        column.ActualWidth = columnWidth;
                    }
                    num2 += columnWidth;
                }
                foreach (BandRow row2 in base2.ActualRows)
                {
                    if (!ReferenceEquals(row2, bandRow))
                    {
                        this.ChangeColumnSize(null, num - num2, row2.Columns);
                    }
                }
            }
        }

        private double DecreaseColumnsWidth(BaseColumn resizeColumn, double delta, IList columns, int columnIndex, bool isLeft)
        {
            double num = this.CalcSizeableWidth(columnIndex, columns, isLeft);
            if (num == 0.0)
            {
                return -delta;
            }
            double num2 = delta;
            for (int i = 0; i < columns.Count; i++)
            {
                if (!this.BandsLayout.SkipItem(columnIndex, i, isLeft))
                {
                    BaseColumn column = (BaseColumn) columns[i];
                    if (!column.FixedWidth)
                    {
                        double num5 = Math.Max(this.GetColumnWidth(column) - (((this.GetColumnWidth(column) - column.MinWidth) * Math.Min(num, delta)) / num), column.MinWidth);
                        num2 -= this.GetColumnWidth(column) - num5;
                        column.ActualWidth = num5;
                    }
                }
            }
            return -num2;
        }

        protected IEnumerable<BaseColumn> GetBandColumns(BandBase band, bool useMinWidth)
        {
            if (band.VisibleBands.Count == 0)
            {
                if (band.ActualRows.Count != 0)
                {
                    return (IEnumerable<BaseColumn>) this.GetBandRow(band, useMinWidth).Columns;
                }
                return new BaseColumn[] { band };
            }
            List<BaseColumn> list = new List<BaseColumn>();
            foreach (BandBase base2 in band.VisibleBands)
            {
                list.AddRange(this.GetBandColumns(base2, useMinWidth));
            }
            return list;
        }

        internal double GetBandMinWidth(BandBase band, BandRow bandRow = null)
        {
            double num = 0.0;
            if (band.VisibleBands.Count != 0)
            {
                foreach (BandBase base2 in band.VisibleBands)
                {
                    num += this.GetBandMinWidth(base2, null);
                }
            }
            else if (band.ActualRows.Count == 0)
            {
                num += AutoWidthHelper.GetColumnFixedWidth(band, this.ViewInfo, true);
            }
            else
            {
                foreach (BandRow row in band.ActualRows)
                {
                    if (!ReferenceEquals(row, bandRow))
                    {
                        num = Math.Max(num, AutoWidthHelper.CalcColumnsFixedWidth(row.Columns, this.ViewInfo));
                    }
                }
            }
            return num;
        }

        internal BandRow GetBandRow(BandBase band, bool useMinWidth)
        {
            BandRow row = band.ActualRows[0];
            double num = 0.0;
            foreach (BandRow row2 in band.ActualRows)
            {
                double num2 = 0.0;
                foreach (ColumnBase base2 in row2.Columns)
                {
                    num2 = !useMinWidth ? (num2 + this.ViewInfo.GetColumnDataWidth(base2)) : (num2 + AutoWidthHelper.GetColumnFixedWidth(base2, this.ViewInfo, true));
                }
                if (num2 > num)
                {
                    row = row2;
                    num = num2;
                }
            }
            return row;
        }

        protected double GetColumnHeaderWidth(BaseColumn column) => 
            column.ActualHeaderWidth;

        protected double GetColumnWidth(BaseColumn column) => 
            this.GetColumnHeaderWidth(column) - this.ViewInfo.GetHeaderIndentsWidth(column, true);

        protected IList GetSizeableColumns(bool useMinWidth) => 
            this.GetSizeableColumns(this.BandsLayout.VisibleBands, useMinWidth);

        protected IList GetSizeableColumns(IList bands, bool useMinWidth)
        {
            List<BaseColumn> list = new List<BaseColumn>();
            foreach (BandBase base2 in bands)
            {
                list.AddRange(this.GetBandColumns(base2, useMinWidth));
            }
            return list;
        }

        private bool HasFixedSizeRow(ColumnBase resizeColumn)
        {
            bool flag2;
            using (List<BandRow>.Enumerator enumerator = resizeColumn.ParentBand.ActualRows.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BandRow current = enumerator.Current;
                        if (ReferenceEquals(current, resizeColumn.BandRow))
                        {
                            continue;
                        }
                        bool flag = false;
                        foreach (BaseColumn column in current.Columns)
                        {
                            if (!column.FixedWidth)
                            {
                                flag = true;
                            }
                        }
                        if (flag)
                        {
                            continue;
                        }
                        flag2 = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag2;
        }

        private bool HasUnfixedColumn(List<ColumnBase> columns, int index)
        {
            for (int i = index; i < columns.Count; i++)
            {
                if (!columns[i].FixedWidth)
                {
                    return true;
                }
            }
            return false;
        }

        protected void IncreaseBandsWidth(IList bands, double delta)
        {
            this.IncreaseBandsWidth(bands, delta, this.CalcUnfixedColumnsCount(bands));
        }

        private void IncreaseBandsWidth(IList bands, double delta, int unfixedColumnsCount)
        {
            foreach (BandBase base2 in bands)
            {
                if (base2.VisibleBands.Count != 0)
                {
                    this.IncreaseBandsWidth(base2.VisibleBands, delta, unfixedColumnsCount);
                    continue;
                }
                if (base2.ActualRows.Count == 0)
                {
                    if (base2.FixedWidth)
                    {
                        continue;
                    }
                    base2.ActualWidth = this.CalcColumnWidth(base2, delta, unfixedColumnsCount);
                    continue;
                }
                if (this.CanBeResized(base2))
                {
                    double num = 0.0;
                    double num2 = 0.0;
                    BandRow bandRow = this.GetBandRow(base2, false);
                    foreach (BaseColumn column in bandRow.Columns)
                    {
                        num += this.GetColumnWidth(column);
                        double columnWidth = this.GetColumnWidth(column);
                        if (!column.FixedWidth)
                        {
                            columnWidth = this.CalcColumnWidth(column, delta, unfixedColumnsCount);
                            column.ActualWidth = columnWidth;
                        }
                        num2 += columnWidth;
                    }
                    foreach (BandRow row2 in base2.ActualRows)
                    {
                        if (!ReferenceEquals(row2, bandRow))
                        {
                            this.ChangeColumnSize(null, num - num2, row2.Columns);
                        }
                    }
                }
            }
        }

        private double IncreaseColumnsWidth(BaseColumn resizeColumn, double delta, IList columns, int columnIndex, bool isLeft)
        {
            int num = this.CalcUnfixedColumnsCount(columnIndex, columns, isLeft);
            double num2 = delta;
            for (int i = 0; i < columns.Count; i++)
            {
                if (!this.BandsLayout.SkipItem(columnIndex, i, isLeft))
                {
                    BaseColumn column = (BaseColumn) columns[i];
                    if (!column.FixedWidth)
                    {
                        double num5 = Math.Max(this.GetColumnWidth(column) - (num2 / ((double) num)), column.MinWidth);
                        num--;
                        num2 -= this.GetColumnWidth(column) - num5;
                        column.ActualWidth = num5;
                    }
                }
            }
            return -num2;
        }

        protected virtual void OnBandResize(BandBase band, double delta)
        {
            if (this.BandsLayout.GetRootBand(band).Fixed != FixedStyle.None)
            {
                this.SetDefaultColumnSize(this.BandsLayout.GetBands(band, false, true));
            }
        }

        protected double RecalcBandLayout(BandBase band, LayoutAssigner layoutAssigner)
        {
            if (band.VisibleBands.Count != 0)
            {
                layoutAssigner.SetWidth(band, 0.0);
                foreach (BandBase base2 in band.VisibleBands)
                {
                    layoutAssigner.SetWidth(band, layoutAssigner.GetWidth(band) + this.RecalcBandLayout(base2, layoutAssigner));
                }
            }
            else if (band.ActualRows.Count != 0)
            {
                double num = 0.0;
                BandRow objB = null;
                layoutAssigner.SetWidth(band, 0.0);
                foreach (BandRow row2 in band.ActualRows)
                {
                    double d = 0.0;
                    foreach (BaseColumn column in row2.Columns)
                    {
                        d += layoutAssigner.GetWidth(column);
                    }
                    if (!double.IsNaN(d) && (num < d))
                    {
                        objB = row2;
                        num = d;
                    }
                }
                layoutAssigner.SetWidth(band, num);
                foreach (BandRow row3 in band.ActualRows)
                {
                    if (!ReferenceEquals(row3, objB))
                    {
                        AutoWidthHelper.CalcColumnLayout(row3.Columns, num, this.ViewInfo, layoutAssigner, true, false);
                    }
                }
            }
            return layoutAssigner.GetWidth(band);
        }

        protected void ResetActualColumnHeaders(IEnumerable bands, LayoutAssigner layoutAssigner)
        {
            foreach (BandBase base2 in bands)
            {
                if (base2.VisibleBands.Count != 0)
                {
                    this.ResetActualColumnHeaders(base2.VisibleBands, layoutAssigner);
                }
                foreach (BandRow row in base2.ActualRows)
                {
                    foreach (ColumnBase base3 in row.Columns)
                    {
                        layoutAssigner.SetWidth(base3, 0.0);
                    }
                }
            }
        }

        private void SetColumnsWidth(int columnIndex, double delta, IList columns)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                BaseColumn column = (BaseColumn) columns[i];
                column.ActualWidth = (i != columnIndex) ? this.GetColumnWidth(column) : Math.Max((double) 0.0, (double) (this.GetColumnWidth(column) + delta));
            }
        }

        protected void SetDefaultColumnSize(IList bands)
        {
            foreach (BandBase base2 in bands)
            {
                if (base2.VisibleBands.Count != 0)
                {
                    this.SetDefaultColumnSize(base2.VisibleBands);
                    continue;
                }
                if (base2.ActualRows.Count == 0)
                {
                    base2.ActualWidth = this.GetColumnWidth(base2);
                    continue;
                }
                foreach (BandRow row in base2.ActualRows)
                {
                    foreach (BaseColumn column in row.Columns)
                    {
                        column.ActualWidth = this.GetColumnWidth(column);
                    }
                }
            }
        }

        protected void StretchColumnsToWidth(List<BandBase> bands, double arrangeWidth, LayoutAssigner layoutAssigner, bool needRoundingLastColumn, bool allowFixedWidth)
        {
            this.ResetActualColumnHeaders(bands, layoutAssigner);
            AutoWidthHelper.CalcColumnLayout(this.GetSizeableColumns(bands, true), arrangeWidth, this.ViewInfo, layoutAssigner, needRoundingLastColumn, allowFixedWidth);
            foreach (BandBase base2 in bands)
            {
                this.RecalcBandLayout(base2, layoutAssigner);
            }
        }

        protected override void UpdateColumnsActualAllowResizing()
        {
            base.UpdateColumnsActualAllowResizing();
            Action<BandBase> action = <>c.<>9__37_0;
            if (<>c.<>9__37_0 == null)
            {
                Action<BandBase> local1 = <>c.<>9__37_0;
                action = <>c.<>9__37_0 = e => e.UpdateActualAllowResizing();
            }
            this.BandsLayout.ForeachBand(action);
        }

        private void UpdateFixedColumnsWidth(double arrangeWidth, bool skipLeft)
        {
            List<BandBase> bands = new List<BandBase>();
            foreach (BandBase base2 in this.BandsLayout.VisibleBands)
            {
                if ((base2.Fixed != FixedStyle.None) && (!skipLeft || (base2.Fixed != FixedStyle.Left)))
                {
                    bands.Add(base2);
                }
            }
            this.StretchColumnsToWidth(bands, arrangeWidth, LayoutAssigner.Default, false, false);
        }

        protected BandsLayoutBase BandsLayout =>
            this.ViewInfo.BandsLayout;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BandedViewColumnsLayoutCalculator.<>c <>9 = new BandedViewColumnsLayoutCalculator.<>c();
            public static Action<BandBase> <>9__37_0;
            public static Func<BaseColumn, int, bool> <>9__38_0;
            public static Func<BaseColumn, int, bool> <>9__38_1;
            public static Func<BaseColumn, int, bool> <>9__38_2;

            internal bool <CorrectFixedColumnsWidth>b__38_0(BaseColumn band, int index) => 
                band.Fixed == FixedStyle.None;

            internal bool <CorrectFixedColumnsWidth>b__38_1(BaseColumn band, int index) => 
                band.Fixed != FixedStyle.None;

            internal bool <CorrectFixedColumnsWidth>b__38_2(BaseColumn band, int index) => 
                band.Fixed != FixedStyle.Left;

            internal void <UpdateColumnsActualAllowResizing>b__37_0(BandBase e)
            {
                e.UpdateActualAllowResizing();
            }
        }
    }
}

