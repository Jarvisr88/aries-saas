namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class BandsLayoutCalculator : LayoutCalculator
    {
        private Dictionary<BandBase, BandRow> bandRowCache;

        public BandsLayoutCalculator(GridViewInfo viewInfo) : base(viewInfo)
        {
        }

        protected override void ApplyResize(BaseColumn resizeColumn, double newWidth, bool correctWidths)
        {
            double delta = 0.0;
            delta = !(resizeColumn is BandBase) ? this.ResizeColumn(resizeColumn, newWidth) : this.ResizeBand((BandBase) resizeColumn, newWidth);
            this.ResizeRightBands(resizeColumn.ParentBandInternal, delta);
        }

        private void AssignActualStars(BaseColumn resizeColumn)
        {
            GridColumnWidth width;
            double num = 0.0;
            double num2 = 0.0;
            foreach (BaseColumn column in this.CalcDefinitiveColumns(this.BandsLayout.VisibleBands, false))
            {
                width = column.Width;
                if (!width.IsAbsolute)
                {
                    num += column.Width.Value;
                    num2 += column.ColumnWidth;
                }
            }
            if (num2 != 0.0)
            {
                foreach (BandRow row in resizeColumn.ParentBand.ActualRows)
                {
                    foreach (BaseColumn column2 in row.Columns)
                    {
                        width = column2.Width;
                        if (!width.IsAbsolute)
                        {
                            double columnWidth = column2.ColumnWidth;
                            column2.Width = new GridColumnWidth((num * columnWidth) / num2, GridColumnUnitType.Star);
                            column2.ColumnWidth = columnWidth;
                        }
                    }
                }
            }
        }

        protected override void CalcActualLayout(double arrangeWidth)
        {
            List<BaseColumn> columns = this.CalcDefinitiveColumns(this.BandsLayout.VisibleBands, true);
            base.CalcColumnsLayout(columns, arrangeWidth, false);
            foreach (BandBase base2 in this.BandsLayout.VisibleBands)
            {
                this.CalcBandLayout(base2);
            }
            base.UpdateHasStarColumns(columns);
        }

        private double CalcBandLayout(BandBase band)
        {
            double arrangeWidth = 0.0;
            if (band.VisibleBands.Count > 0)
            {
                foreach (BandBase base2 in band.VisibleBands)
                {
                    arrangeWidth += this.CalcBandLayout(base2);
                }
            }
            else
            {
                if (band.ActualRows.Count <= 0)
                {
                    return base.layoutAssigner.GetWidth(band);
                }
                BandRow objB = this.bandRowCache[band];
                arrangeWidth = objB.Columns.Sum<ColumnBase>(x => x.ActualHeaderWidth - this.ViewInfo.GetHeaderIndentsWidth(x, true));
                foreach (BandRow row2 in band.ActualRows)
                {
                    if (!ReferenceEquals(row2, objB))
                    {
                        base.CalcColumnsLayout(row2.Columns, arrangeWidth, true);
                    }
                }
            }
            band.ColumnWidth = arrangeWidth;
            arrangeWidth += this.ViewInfo.GetHeaderIndentsWidth(band, true);
            band.HeaderWidth = arrangeWidth;
            base.layoutAssigner.SetWidth(band, arrangeWidth);
            return arrangeWidth;
        }

        private double CalcCurrentWidth(BaseColumn resizeColumn)
        {
            IList columns = resizeColumn.BandRow.Columns;
            double num = 0.0;
            double num2 = 0.0;
            int index = columns.IndexOf(resizeColumn);
            for (int i = 0; i < columns.Count; i++)
            {
                BaseColumn column = (BaseColumn) columns[i];
                column.ForcedWidth = column.ColumnWidth;
                if (i != index)
                {
                    if (i > index)
                    {
                        num2 += column.Width.IsAbsolute ? column.Width.Value : column.MinWidth;
                    }
                    else if (i < index)
                    {
                        num += column.ColumnWidth;
                    }
                }
            }
            return ((num + resizeColumn.ColumnWidth) + num2);
        }

        private void CalcDefinitiveColumns(BandBase band, IList<BaseColumn> list)
        {
            if (band.VisibleBands.Count > 0)
            {
                foreach (BandBase base2 in band.VisibleBands)
                {
                    this.CalcDefinitiveColumns(base2, list);
                }
            }
            else if (band.ActualRows.Count <= 0)
            {
                list.Add(band);
            }
            else
            {
                BandRow definitiveRow = null;
                if (!this.bandRowCache.TryGetValue(band, out definitiveRow))
                {
                    definitiveRow = this.GetDefinitiveRow(band, null);
                    this.bandRowCache[band] = definitiveRow;
                }
                foreach (BaseColumn column in definitiveRow.Columns)
                {
                    list.Add(column);
                }
            }
        }

        private List<BaseColumn> CalcDefinitiveColumns(IList bands, bool clearCache = false)
        {
            if (ReferenceEquals(this.bandRowCache, null) | clearCache)
            {
                this.bandRowCache = new Dictionary<BandBase, BandRow>();
            }
            List<BaseColumn> list = new List<BaseColumn>();
            foreach (BandBase base2 in bands)
            {
                this.CalcDefinitiveColumns(base2, list);
            }
            return list;
        }

        private double CalcDefinitiveDelta(BaseColumn resizeColumn, double delta)
        {
            BandRow definitiveRow = this.GetDefinitiveRow(resizeColumn.ParentBand, resizeColumn.BandRow);
            double num = 0.0;
            double num2 = 0.0;
            if (definitiveRow != null)
            {
                foreach (BaseColumn column in definitiveRow.Columns)
                {
                    num += column.Width.IsAbsolute ? column.Width.Value : column.MinWidth;
                    GridColumnWidth width = column.Width;
                    num2 += width.IsAbsolute ? column.ColumnWidth : column.MinWidth;
                }
            }
            double num3 = this.CalcCurrentWidth(resizeColumn);
            return ((delta <= 0.0) ? Math.Min((double) (delta - Math.Min((double) (num - num3), (double) 0.0)), (double) 0.0) : Math.Min(Math.Max((double) 0.0, (double) (num2 - num3)), delta));
        }

        private void ChangeBandWidth(IList bands, double delta)
        {
            IList columns = this.CalcDefinitiveColumns(bands, false);
            base.ResizeRightColumns(columns, -1, -delta, true, false, false);
            this.FillBandsToDefinitiveRow(bands);
        }

        private void ChangeBandWidth(BandBase band, double delta, BandRow skipRow, bool fixStarWidth)
        {
            foreach (BandRow row in band.ActualRows)
            {
                if (!ReferenceEquals(row, skipRow))
                {
                    base.ResizeRightColumns(row.Columns, -1, -delta, true, false, fixStarWidth);
                }
            }
        }

        private double CorrectBandDelta(BaseColumn column, double delta) => 
            (column != column.BandRow.Columns[column.BandRow.Columns.Count - 1]) ? delta : (Math.Max(this.GetBandMinWidth(column.ParentBand), column.ParentBand.ColumnWidth + delta) - column.ParentBand.ColumnWidth);

        private double CorrectColumnDelta(BaseColumn column, double delta) => 
            ((column.ColumnWidth + delta) >= column.MinWidth) ? ((((column.ColumnWidth + delta) <= column.MaxWidth) || !this.IsDefinitive(column)) ? delta : (column.MaxWidth - column.ColumnWidth)) : (column.MinWidth - column.ColumnWidth);

        private IList CreateDefinitiveRow(BandBase band, IList columns)
        {
            List<BaseColumn> list = new List<BaseColumn>();
            foreach (BaseColumn column in this.CalcDefinitiveColumns(this.BandsLayout.GetBands(band, true, false), false))
            {
                list.Add(column);
            }
            foreach (BaseColumn column2 in columns)
            {
                list.Add(column2);
            }
            foreach (BaseColumn column3 in this.CalcDefinitiveColumns(this.BandsLayout.GetBands(band, false, false), false))
            {
                list.Add(column3);
            }
            return list;
        }

        private void FillBandsToDefinitiveRow(IList bands)
        {
            foreach (BandBase base2 in bands)
            {
                if (base2.VisibleBands.Count > 0)
                {
                    this.FillBandsToDefinitiveRow(base2.VisibleBands);
                    continue;
                }
                if (base2.ActualRows.Count > 0)
                {
                    BandRow objB = this.bandRowCache[base2];
                    Func<ColumnBase, double> selector = <>c.<>9__14_0;
                    if (<>c.<>9__14_0 == null)
                    {
                        Func<ColumnBase, double> local1 = <>c.<>9__14_0;
                        selector = <>c.<>9__14_0 = x => x.ColumnWidth;
                    }
                    double num = objB.Columns.Sum<ColumnBase>(selector);
                    foreach (BandRow row2 in base2.ActualRows)
                    {
                        if (!ReferenceEquals(row2, objB))
                        {
                            Func<ColumnBase, double> func1 = <>c.<>9__14_1;
                            if (<>c.<>9__14_1 == null)
                            {
                                Func<ColumnBase, double> local2 = <>c.<>9__14_1;
                                func1 = <>c.<>9__14_1 = x => x.ColumnWidth;
                            }
                            double num2 = row2.Columns.Sum<ColumnBase>(func1);
                            base.ResizeRightColumns(row2.Columns, -1, num2 - num, true, false, true);
                        }
                    }
                }
            }
        }

        private double GetBandMinWidth(BandBase band)
        {
            double num = 0.0;
            foreach (BandRow row in band.ActualRows)
            {
                double num2 = 0.0;
                foreach (BaseColumn column in row.Columns)
                {
                    num2 += column.MinWidth;
                }
                num = Math.Max(num, num2);
            }
            return num;
        }

        private BandRow GetDefinitiveRow(BandBase band, BandRow skipRow)
        {
            BandRow row = null;
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            foreach (BandRow row2 in band.ActualRows)
            {
                if (!ReferenceEquals(row2, skipRow))
                {
                    double num5 = 0.0;
                    double num6 = 0.0;
                    double num7 = 0.0;
                    double num8 = 0.0;
                    foreach (BaseColumn column in row2.Columns)
                    {
                        num6 += column.Width.IsAbsolute ? column.Width.Value : column.MinWidth;
                        num8 += column.MinWidth;
                        GridColumnWidth width = column.Width;
                        if (width.IsStar)
                        {
                            width = column.Width;
                            num5 += width.Value;
                            num7 += column.MinWidth;
                        }
                    }
                    if ((num6 >= num2) && ((num6 != num2) || ((num8 >= num4) && ((num8 != num4) || ((num7 <= num3) && ((num7 != num3) || (num5 > num)))))))
                    {
                        num2 = num6;
                        num = num5;
                        row = row2;
                        num3 = num7;
                        num4 = num8;
                    }
                }
            }
            return row;
        }

        protected override double GetWidth(BaseColumn column) => 
            double.IsNaN(column.ForcedWidth) ? column.Width.Value : column.ForcedWidth;

        protected override bool IsDefinitive(BaseColumn column) => 
            this.bandRowCache[column.ParentBand] == column.BandRow;

        private double ResizeBand(BandBase band, double newWidth)
        {
            double num;
            double num2;
            double columnWidth;
            BandBase[] bands = new BandBase[] { band };
            IList columns = this.CalcDefinitiveColumns(bands, false);
            base.CalcMinMaxWidth(columns, -1, out num, out num2, out columnWidth);
            newWidth = Math.Min(Math.Max(num, newWidth), num2);
            columnWidth = band.ColumnWidth;
            IList list2 = this.CreateDefinitiveRow(band, columns);
            BaseColumn[] columnArray1 = new BaseColumn[] { band };
            base.ApplyResize(this.CreateDefinitiveRow(band, columnArray1), band, newWidth, base.CalcHasLeftStar(list2, null, false), base.CalcHasLeftStar(list2, null, true), true);
            if ((band.VisibleBands.Count > 0) || (band.ActualRows.Count > 0))
            {
                BandBase[] baseArray2 = new BandBase[] { band };
                this.ChangeBandWidth(baseArray2, band.ColumnWidth - columnWidth);
            }
            return (band.ColumnWidth - columnWidth);
        }

        private double ResizeColumn(BaseColumn resizeColumn, double newWidth)
        {
            bool? nullable;
            bool? nullable2;
            double delta = this.CorrectColumnDelta(resizeColumn, newWidth - resizeColumn.ColumnWidth);
            delta = this.CorrectBandDelta(resizeColumn, delta);
            IList columns = resizeColumn.BandRow.Columns;
            Func<ColumnBase, double> selector = <>c.<>9__15_0;
            if (<>c.<>9__15_0 == null)
            {
                Func<ColumnBase, double> local1 = <>c.<>9__15_0;
                selector = <>c.<>9__15_0 = x => x.ColumnWidth;
            }
            double num2 = resizeColumn.BandRow.Columns.Sum<ColumnBase>(selector);
            double num3 = this.CalcDefinitiveDelta(resizeColumn, delta);
            this.AssignActualStars(resizeColumn);
            bool flag = base.CalcHasLeftStar(this.CalcDefinitiveColumns(this.BandsLayout.VisibleBands, false), null, false);
            if (delta > 0.0)
            {
                bool? nullable1;
                if (flag || (resizeColumn.ParentBand.ActualRows.Count > 1))
                {
                    nullable1 = true;
                }
                else
                {
                    nullable = null;
                    nullable1 = nullable;
                }
                this.ApplyResize(resizeColumn.BandRow.Columns, resizeColumn, resizeColumn.ColumnWidth + num3, nullable1);
            }
            IList list2 = this.CreateDefinitiveRow(resizeColumn.ParentBand, resizeColumn.BandRow.Columns);
            if (!flag)
            {
                nullable2 = false;
            }
            else
            {
                nullable = null;
                nullable2 = nullable;
            }
            this.ApplyResize(list2, resizeColumn, (resizeColumn.ColumnWidth + delta) - num3, nullable2);
            if (delta < 0.0)
            {
                Func<ColumnBase, double> func2 = <>c.<>9__15_1;
                if (<>c.<>9__15_1 == null)
                {
                    Func<ColumnBase, double> local2 = <>c.<>9__15_1;
                    func2 = <>c.<>9__15_1 = x => x.ColumnWidth;
                }
                double num4 = resizeColumn.BandRow.Columns.Sum<ColumnBase>(func2);
                base.SetColumnWidth(resizeColumn, resizeColumn.ColumnWidth + num3);
                BandBase[] bands = new BandBase[] { resizeColumn.ParentBand };
                this.CalcDefinitiveColumns(bands, true);
                bool hasStar = (resizeColumn.ParentBand.ActualRows.Count > 1) || base.CalcHasLeftStar(columns, null, false);
                base.ResizeRightColumns(columns, columns.IndexOf(resizeColumn), num3, hasStar, false, false);
                BandsLayoutCalculator calculator1 = this;
                if (<>c.<>9__15_2 == null)
                {
                    calculator1 = (BandsLayoutCalculator) (<>c.<>9__15_2 = x => x.ColumnWidth);
                }
                <>c.<>9__15_2.ResizeRightColumns(resizeColumn.BandRow.Columns, -1, this.CalcDefinitiveColumns(this.BandsLayout.GetBands(resizeColumn.ParentBand, false, false), false).Sum<ColumnBase>(((Func<ColumnBase, double>) calculator1)) - num4, hasStar, false, false);
            }
            Func<ColumnBase, double> func3 = <>c.<>9__15_3;
            if (<>c.<>9__15_3 == null)
            {
                Func<ColumnBase, double> local4 = <>c.<>9__15_3;
                func3 = <>c.<>9__15_3 = x => x.ColumnWidth;
            }
            this.ChangeBandWidth(resizeColumn.ParentBand, resizeColumn.BandRow.Columns.Sum<ColumnBase>(func3) - num2, resizeColumn.BandRow, true);
            Func<ColumnBase, double> func4 = <>c.<>9__15_4;
            if (<>c.<>9__15_4 == null)
            {
                Func<ColumnBase, double> local5 = <>c.<>9__15_4;
                func4 = <>c.<>9__15_4 = x => x.ColumnWidth;
            }
            return (resizeColumn.BandRow.Columns.Sum<ColumnBase>(func4) - num2);
        }

        private void ResizeRightBands(BandBase band, double delta)
        {
            if (base.HasStarColumns)
            {
                this.FillBandsToDefinitiveRow(this.BandsLayout.GetBands(band, false, false));
            }
        }

        private BandsLayoutBase BandsLayout =>
            this.ViewInfo.BandsLayout;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BandsLayoutCalculator.<>c <>9 = new BandsLayoutCalculator.<>c();
            public static Func<ColumnBase, double> <>9__14_0;
            public static Func<ColumnBase, double> <>9__14_1;
            public static Func<ColumnBase, double> <>9__15_0;
            public static Func<ColumnBase, double> <>9__15_1;
            public static Func<ColumnBase, double> <>9__15_2;
            public static Func<ColumnBase, double> <>9__15_3;
            public static Func<ColumnBase, double> <>9__15_4;

            internal double <FillBandsToDefinitiveRow>b__14_0(ColumnBase x) => 
                x.ColumnWidth;

            internal double <FillBandsToDefinitiveRow>b__14_1(ColumnBase x) => 
                x.ColumnWidth;

            internal double <ResizeColumn>b__15_0(ColumnBase x) => 
                x.ColumnWidth;

            internal double <ResizeColumn>b__15_1(ColumnBase x) => 
                x.ColumnWidth;

            internal double <ResizeColumn>b__15_2(ColumnBase x) => 
                x.ColumnWidth;

            internal double <ResizeColumn>b__15_3(ColumnBase x) => 
                x.ColumnWidth;

            internal double <ResizeColumn>b__15_4(ColumnBase x) => 
                x.ColumnWidth;
        }
    }
}

