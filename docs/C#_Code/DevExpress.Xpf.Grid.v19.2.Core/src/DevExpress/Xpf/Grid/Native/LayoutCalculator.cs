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

    public class LayoutCalculator : ColumnsLayoutCalculator
    {
        protected LayoutAssigner layoutAssigner;
        private bool needRoundingLastColumn;

        public LayoutCalculator(GridViewInfo viewInfo) : base(viewInfo)
        {
        }

        public override void ApplyBestFit(BaseColumn resizeColumn, double newWidth, bool correctWidths)
        {
            if (resizeColumn.GetAllowResizing())
            {
                if (resizeColumn.Width.IsAbsolute)
                {
                    this.SetColumnWidth(resizeColumn, newWidth);
                }
                else
                {
                    this.ViewInfo.GridView.BeginUpdateColumnsLayout();
                    double columnWidth = resizeColumn.ColumnWidth;
                    this.SetColumnWidth(resizeColumn, newWidth);
                    IList columns = (from x in base.VisibleColumns
                        where !ReferenceEquals(x, resizeColumn)
                        select x).ToList<ColumnBase>();
                    this.ResizeRightColumns(columns, -1, resizeColumn.ColumnWidth - columnWidth, true, false, false);
                    this.ViewInfo.GridView.EndUpdateColumnsLayout();
                }
            }
        }

        protected virtual void ApplyResize(BaseColumn resizeColumn, double newWidth, bool correctWidths)
        {
            if (!correctWidths)
            {
                this.SetColumnWidth(resizeColumn, newWidth);
            }
            else
            {
                IList columns = (from x in base.VisibleColumns
                    where x.Fixed == resizeColumn.Fixed
                    select x).ToList<ColumnBase>();
                if (resizeColumn.Fixed == FixedStyle.None)
                {
                    this.ApplyResize(columns, resizeColumn, Math.Min(Math.Max(newWidth, resizeColumn.MinWidth), resizeColumn.MaxWidth), this.CalcHasLeftStar(columns, null, false), this.CalcHasLeftStar(columns, resizeColumn, true), true);
                }
                else
                {
                    newWidth = Math.Min(Math.Max(this.CorrectFixedDelta(resizeColumn, newWidth), resizeColumn.MinWidth), resizeColumn.MaxWidth);
                    double delta = newWidth - resizeColumn.ColumnWidth;
                    this.SetColumnWidth(resizeColumn, newWidth);
                    this.CorrectFixedRightColumns(columns, resizeColumn, delta);
                }
            }
        }

        protected void ApplyResize(IList columns, BaseColumn column, double newWidth, bool? forceStars = new bool?())
        {
            this.ApplyResize(columns, column, newWidth, (forceStars != null) ? forceStars.Value : this.CalcHasLeftStar(columns, null, false), (forceStars != null) ? forceStars.Value : this.CalcHasLeftStar(columns, column, true), true);
        }

        protected void ApplyResize(IList columns, BaseColumn column, double newWidth, bool hasStarColumns, bool hasLeftStar, bool checkExcessiveWidth)
        {
            this.AssignActualStars(column, columns);
            double delta = this.SetColumnWidth(columns, column, newWidth, hasStarColumns, hasLeftStar, checkExcessiveWidth);
            if (hasStarColumns)
            {
                this.ResizeRightColumns(columns, columns.IndexOf(column), delta, hasLeftStar || column.Width.IsStar, false, false);
            }
        }

        protected override void ApplyResizeCore(BaseColumn resizeColumn, double newWidth, double maxWidth, double indentWidth, bool correctWidths)
        {
            this.ApplyResize(resizeColumn, newWidth - indentWidth, correctWidths);
        }

        private void AssignActualStars(BaseColumn resizeColumn, IList columns)
        {
            GridColumnWidth width;
            double num = 0.0;
            double columnWidth = 0.0;
            BaseColumn column = null;
            foreach (BaseColumn column2 in columns)
            {
                width = column2.Width;
                if (!width.IsAbsolute)
                {
                    column ??= column2;
                    if (this.IsRealStar(column2))
                    {
                        num += column2.Width.Value;
                        columnWidth += column2.ColumnWidth;
                    }
                }
            }
            if (columnWidth == 0.0)
            {
                if (column == null)
                {
                    return;
                }
                num = column.Width.Value;
                columnWidth = column.ColumnWidth;
            }
            for (int i = columns.IndexOf(resizeColumn); i < columns.Count; i++)
            {
                BaseColumn column3 = (BaseColumn) columns[i];
                width = column3.Width;
                if (!width.IsAbsolute && !this.IsRealStar(column3))
                {
                    double columnWidth = column3.ColumnWidth;
                    column3.Width = new GridColumnWidth((columnWidth * num) / columnWidth, GridColumnUnitType.Star);
                    column3.ColumnWidth = columnWidth;
                }
            }
        }

        private void AssignActualWidth(IList columns, bool useAutoWidth)
        {
            useAutoWidth = useAutoWidth || this.GetStarColumns(columns, 0, true, null).Any<BaseColumn>();
            double rest = 0.0;
            for (int i = 0; i < columns.Count; i++)
            {
                BaseColumn column = (BaseColumn) columns[i];
                column.HeaderWidth = column.ColumnWidth + this.ViewInfo.GetHeaderIndentsWidth(column, true);
                this.layoutAssigner.SetWidth(column, useAutoWidth ? AutoWidthHelper.GetRoundedActualColumnWidth(column.HeaderWidth, ref rest, i == (columns.Count - 1)) : DXArranger.Floor(column.HeaderWidth, 0));
            }
        }

        protected virtual void CalcActualLayout(double arrangeWidth)
        {
            this.CalcColumnsLayout((IList) base.VisibleColumns, arrangeWidth, false);
            this.UpdateHasStarColumns((IList) base.TableViewBehavior.FixedNoneVisibleColumns);
        }

        protected sealed override void CalcActualLayoutCore(double arrangeWidth, LayoutAssigner layoutAssigner, bool showIndicator, bool needRoundingLastColumn, bool ignoreDetailButtons)
        {
            this.layoutAssigner = layoutAssigner;
            this.needRoundingLastColumn = needRoundingLastColumn;
            this.CalcActualLayout(arrangeWidth - this.ViewInfo.FirstColumnIndent);
        }

        protected void CalcColumnsLayout(IList columns, double arrangeWidth, bool fillPixelColumns)
        {
            arrangeWidth -= this.CalcFixedColumnsLayout(columns, arrangeWidth, !fillPixelColumns);
            arrangeWidth -= this.CalcStarColumnsLayout(columns, arrangeWidth, !fillPixelColumns);
            if (fillPixelColumns)
            {
                Action<BaseColumn, int, double> setWidth = <>c.<>9__10_0;
                if (<>c.<>9__10_0 == null)
                {
                    Action<BaseColumn, int, double> local1 = <>c.<>9__10_0;
                    setWidth = <>c.<>9__10_0 = (column, index, width) => column.ColumnWidth = width;
                }
                this.FillPixelColumns(columns, arrangeWidth, setWidth);
            }
            this.AssignActualWidth(columns, fillPixelColumns);
        }

        private double CalcColumnWidth(Func<BaseColumn, int, double> getWidth)
        {
            double num = 0.0;
            for (int i = 0; i < base.VisibleColumns.Count; i++)
            {
                num += getWidth(base.VisibleColumns[i], i);
            }
            return num;
        }

        private double CalcExcessWidth(IList columns)
        {
            double num = 0.0;
            foreach (BaseColumn column in columns)
            {
                num += column.ColumnWidth;
            }
            return Math.Max((double) 0.0, (double) (num - this.GetArrangeWidth()));
        }

        private double CalcFixedColumnsLayout(IList columns, double arrangeWidth, bool checkForcedWidth)
        {
            double num = 0.0;
            foreach (BaseColumn column in columns)
            {
                if (column.Width.IsAbsolute || (checkForcedWidth && !double.IsNaN(column.ForcedWidth)))
                {
                    column.ColumnWidth = this.GetColumnFixedWidth(column);
                    num += column.ColumnWidth;
                }
                GridColumnWidth width = column.Width;
                if (width.IsStar && (column.Fixed != FixedStyle.None))
                {
                    column.ColumnWidth = column.MinWidth;
                    num += column.ColumnWidth;
                }
            }
            return num;
        }

        protected bool CalcHasLeftStar(IList columns, BaseColumn resizeColumn, bool checkStarMinWidth)
        {
            bool flag;
            using (IEnumerator enumerator = columns.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BaseColumn current = (BaseColumn) enumerator.Current;
                        GridColumnWidth width = current.Width;
                        if (width.IsStar && (!checkStarMinWidth || (current.MinWidth != current.ColumnWidth)))
                        {
                            flag = true;
                        }
                        else
                        {
                            if (!ReferenceEquals(resizeColumn, current))
                            {
                                continue;
                            }
                            flag = false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        protected void CalcMinMaxWidth(IList columns, int index, out double minWidth, out double maxWidth, out double actualWidth)
        {
            minWidth = 0.0;
            maxWidth = 0.0;
            actualWidth = 0.0;
            for (int i = index + 1; i < columns.Count; i++)
            {
                BaseColumn column = (BaseColumn) columns[i];
                GridColumnWidth width = column.Width;
                if (width.IsStar || (!column.FixedWidth || !this.IsDefinitive(column)))
                {
                    actualWidth += column.ColumnWidth;
                    minWidth += column.MinWidth;
                    maxWidth += column.MaxWidth;
                }
            }
        }

        private double CalcStarColumnsLayout(IList columns, double arrangeWidth, bool checkForcedWidth)
        {
            double starSum = 0.0;
            List<BaseColumn> list = this.GetStarColumns(columns, 0, checkForcedWidth, delegate (BaseColumn x) {
                starSum += x.Width.Value;
            });
            List<BaseColumn> list2 = new List<BaseColumn>();
            int num = 0;
            double num2 = 0.0;
            double num3 = 0.0;
            while (num < list.Count)
            {
                BaseColumn column = list[num];
                GridColumnWidth width = column.Width;
                double d = (arrangeWidth * width.Value) / starSum;
                if (double.IsInfinity(d))
                {
                    d = 120.0;
                }
                if ((d <= column.MaxWidth) || !this.IsDefinitive(column))
                {
                    if (d < column.MinWidth)
                    {
                        list2.Add(column);
                    }
                }
                else
                {
                    foreach (BaseColumn column2 in list2)
                    {
                        width = column2.Width;
                        starSum += width.Value;
                        arrangeWidth += column2.ColumnWidth;
                        num2 -= column2.ColumnWidth;
                        list.Insert(0, column2);
                    }
                    list2.Clear();
                }
                if ((d >= column.MinWidth) && ((d <= column.MaxWidth) || !this.IsDefinitive(column)))
                {
                    num++;
                    num3 += d;
                }
                else
                {
                    d = Math.Min(Math.Max(d, column.MinWidth), column.MaxWidth);
                    starSum -= column.Width.Value;
                    arrangeWidth -= d;
                    list.Remove(column);
                    num = 0;
                    num2 += d;
                    num3 = 0.0;
                }
                column.ColumnWidth = d;
                column.ActualWidth = d;
            }
            return (num2 + num3);
        }

        protected override void CorrectFixedColumnsWidth()
        {
            double num = base.CalcHorizontalViewPort();
            Func<BaseColumn, int, double> getWidth = <>c.<>9__22_0;
            if (<>c.<>9__22_0 == null)
            {
                Func<BaseColumn, int, double> local1 = <>c.<>9__22_0;
                getWidth = <>c.<>9__22_0 = (x, index) => (x.Fixed != FixedStyle.None) ? x.ColumnWidth : 0.0;
            }
            double num2 = this.CalcColumnWidth(getWidth);
            if ((num < num2) && (num > 0.0))
            {
                this.FillFixedColumns(this.GetFixedColumns(false), num - num2);
            }
        }

        private double CorrectFixedDelta(BaseColumn resizeColumn, double newWidth)
        {
            if (resizeColumn.Fixed == FixedStyle.None)
            {
                return newWidth;
            }
            this.SetFixedColumnWidth();
            double arrangeWidth = this.GetArrangeWidth();
            newWidth = Math.Min(newWidth, (this.GetArrangeWidth() - this.CalcColumnWidth((x, index) => ((x.Fixed == FixedStyle.None) || ReferenceEquals(x, resizeColumn)) ? 0.0 : x.Width.Value)) - 1.0);
            if ((resizeColumn.Fixed != FixedStyle.Right) || this.CalcHasLeftStar((IList) base.TableViewBehavior.FixedNoneVisibleColumns, null, false))
            {
                return newWidth;
            }
            int columnIndex = base.VisibleColumns.IndexOf((ColumnBase) resizeColumn);
            double num2 = this.CalcColumnWidth((x, index) => ((x.Fixed != FixedStyle.Right) || (!x.Width.IsAbsolute || (index >= columnIndex))) ? 0.0 : x.MaxWidth);
            return Math.Max(newWidth, (arrangeWidth - this.CalcColumnWidth((x, index) => !x.Width.IsStar ? (((x.Fixed != FixedStyle.Right) || (index > columnIndex)) ? x.Width.Value : 0.0) : x.MinWidth)) - num2);
        }

        private void CorrectFixedRightColumns(IList columns, BaseColumn resizeColumn, double delta)
        {
            if ((resizeColumn.Fixed == FixedStyle.Right) && !this.CalcHasLeftStar((IList) base.TableViewBehavior.FixedNoneVisibleColumns, null, false))
            {
                double arrangeWidth = this.GetArrangeWidth();
                Func<BaseColumn, int, double> getWidth = <>c.<>9__24_0;
                if (<>c.<>9__24_0 == null)
                {
                    Func<BaseColumn, int, double> local1 = <>c.<>9__24_0;
                    getWidth = <>c.<>9__24_0 = (x, i) => x.ColumnWidth;
                }
                double num2 = this.CalcColumnWidth(getWidth);
                int index = columns.IndexOf(resizeColumn);
                this.RestorePixelColumns(columns, index, delta, true);
                if (arrangeWidth > num2)
                {
                    this.FillPixelColumns(columns, index, num2 - arrangeWidth, true);
                }
            }
        }

        protected override void FillByLastFixedColumn()
        {
            double num = ((base.CalcHorizontalViewPort() - this.ViewInfo.TableView.RightDataAreaIndent) - this.ViewInfo.TableView.RightGroupAreaIndent) + 1.0;
            Func<BaseColumn, int, double> getWidth = <>c.<>9__26_0;
            if (<>c.<>9__26_0 == null)
            {
                Func<BaseColumn, int, double> local1 = <>c.<>9__26_0;
                getWidth = <>c.<>9__26_0 = (x, index) => (x.Fixed != FixedStyle.None) ? x.ColumnWidth : 0.0;
            }
            double num2 = this.CalcColumnWidth(getWidth);
            Func<BaseColumn, int, double> func2 = <>c.<>9__26_1;
            if (<>c.<>9__26_1 == null)
            {
                Func<BaseColumn, int, double> local2 = <>c.<>9__26_1;
                func2 = <>c.<>9__26_1 = (x, index) => (x.Fixed == FixedStyle.None) ? x.ColumnWidth : 0.0;
            }
            double num3 = this.CalcColumnWidth(func2);
            if (num > (num2 + num3))
            {
                this.FillFixedColumns(this.GetFixedColumns(true), (num - num2) - num3);
            }
        }

        private void FillFixedColumns(IList columns, double delta)
        {
            double rest = 0.0;
            this.FillPixelColumns(columns, delta, delegate (BaseColumn column, int index, double newWidth) {
                double columnWidth = newWidth + this.ViewInfo.GetHeaderIndentsWidth(column, true);
                column.HeaderWidth = columnWidth;
                this.layoutAssigner.SetWidth(column, AutoWidthHelper.GetRoundedActualColumnWidth(columnWidth, ref rest, index == (columns.Count - 1)));
                ColumnBase base2 = column as ColumnBase;
                if (base2 != null)
                {
                    base2.ActualDataWidth = column.ActualHeaderWidth - this.ViewInfo.GetHeaderIndentsWidth(column, true);
                }
            });
        }

        private double FillPixelColumns(IList columns, double arrangeWidth, Action<BaseColumn, int, double> setWidth)
        {
            GridColumnWidth width;
            double num = 0.0;
            double num2 = 0.0;
            foreach (BaseColumn column in columns)
            {
                width = column.Width;
                if (width.IsAbsolute)
                {
                    num2 += column.Width.Value;
                    if (column.FixedWidth)
                    {
                        num += column.Width.Value;
                    }
                }
            }
            for (int i = 0; i < columns.Count; i++)
            {
                BaseColumn column2 = (BaseColumn) columns[i];
                if (column2.Width.IsAbsolute)
                {
                    double num4 = double.IsNaN(column2.ForcedWidth) ? column2.Width.Value : column2.ForcedWidth;
                    if ((num == 0.0) || ((num == num2) && (column2.ParentBand != null)))
                    {
                        width = column2.Width;
                        num4 = Math.Max(column2.MinWidth, num4 + ((arrangeWidth * width.Value) / num2));
                    }
                    else if (!column2.FixedWidth)
                    {
                        num4 = Math.Max(column2.MinWidth, num4 + ((arrangeWidth * column2.Width.Value) / (num2 - num)));
                    }
                    setWidth(column2, i, num4);
                }
            }
            return (arrangeWidth - num);
        }

        protected virtual double FillPixelColumns(IList initialColumns, int index, double delta, bool reverse)
        {
            double num = 0.0;
            if (reverse)
            {
                initialColumns = initialColumns.Cast<BaseColumn>().Reverse<BaseColumn>().ToList<BaseColumn>();
                index = (initialColumns.Count - 1) - index;
            }
            List<BaseColumn> list = new List<BaseColumn>();
            for (int i = index + 1; i < initialColumns.Count; i++)
            {
                BaseColumn column = (BaseColumn) initialColumns[i];
                GridColumnWidth width = column.Width;
                if (!width.IsStar && (!column.FixedWidth || !this.IsDefinitive(column)))
                {
                    list.Add(column);
                    num += column.ForcedWidth - column.MinWidth;
                }
            }
            int num2 = 0;
            double num3 = 0.0;
            while (num2 < list.Count)
            {
                BaseColumn column2 = list[num2];
                double num5 = (num != 0.0) ? ((delta * (column2.ForcedWidth - column2.MinWidth)) / num) : delta;
                double num6 = column2.ForcedWidth - num5;
                if ((num6 >= column2.MinWidth) && (num6 <= column2.MaxWidth))
                {
                    num2++;
                    num3 += num6 - column2.ForcedWidth;
                }
                else
                {
                    num6 = Math.Min(Math.Max(num6, column2.MinWidth), column2.MaxWidth);
                    delta += num6 - column2.ForcedWidth;
                    num -= column2.ForcedWidth - column2.MinWidth;
                    list.RemoveAt(num2);
                    num2 = 0;
                    num3 = 0.0;
                }
                column2.ForcedWidth = num6;
                column2.ColumnWidth = num6;
            }
            return (delta + num3);
        }

        private double FillStarColumns(IList columns, int index, double delta, bool fixStarWidth)
        {
            GridColumnWidth width;
            double starWidth = 0.0;
            double starSum = 0.0;
            List<BaseColumn> list = this.GetStarColumns(columns, index + 1, false, delegate (BaseColumn x) {
                starWidth += x.ColumnWidth;
                starSum += x.Width.Value;
            });
            int num = 0;
            double num2 = 0.0;
            while (num < list.Count)
            {
                BaseColumn column = list[num];
                width = column.Width;
                double num3 = column.ColumnWidth - ((delta * width.Value) / starSum);
                if ((num3 >= column.MinWidth) && ((num3 <= column.MaxWidth) || !this.IsDefinitive(column)))
                {
                    num++;
                    num2 += num3 - column.ColumnWidth;
                    continue;
                }
                num3 = Math.Min(Math.Max(num3, column.MinWidth), column.MaxWidth);
                delta += num3 - column.ColumnWidth;
                starSum -= column.Width.Value;
                starWidth -= column.ColumnWidth;
                column.Width = new GridColumnWidth((starSum / starWidth) * num3, GridColumnUnitType.Star);
                if (fixStarWidth)
                {
                    column.ForcedWidth = num3;
                }
                column.ColumnWidth = num3;
                list.RemoveAt(num);
                num = 0;
                num2 = 0.0;
            }
            foreach (BaseColumn column2 in list)
            {
                width = column2.Width;
                double num5 = column2.ColumnWidth - ((delta * width.Value) / starSum);
                column2.Width = new GridColumnWidth((starSum / starWidth) * num5, GridColumnUnitType.Star);
                if (fixStarWidth)
                {
                    column2.ForcedWidth = num5;
                }
                column2.ColumnWidth = num5;
            }
            return (delta + num2);
        }

        private double GetArrangeWidth() => 
            this.GetArrangeWidth(this.ViewInfo.ColumnsLayoutSize, LayoutAssigner.Default, this.ViewInfo.TableView.ActualShowIndicator, false) - this.ViewInfo.FirstColumnIndent;

        protected double GetColumnFixedWidth(BaseColumn column) => 
            Math.Min(Math.Max(this.GetWidth(column), column.MinWidth), column.MaxWidth);

        private IList GetFixedColumns(bool skipLeft)
        {
            List<BaseColumn> list = new List<BaseColumn>();
            foreach (BaseColumn column in base.VisibleColumns)
            {
                if ((column.Fixed != FixedStyle.None) && !((column.Fixed == FixedStyle.Left) & skipLeft))
                {
                    list.Add(column);
                }
            }
            return list;
        }

        private List<BaseColumn> GetStarColumns(IList columns, int index, bool checkForcedWidth, Action<BaseColumn> action = null)
        {
            List<BaseColumn> list = new List<BaseColumn>();
            for (int i = index; i < columns.Count; i++)
            {
                BaseColumn item = (BaseColumn) columns[i];
                GridColumnWidth width = item.Width;
                if (width.IsStar && ((item.Fixed == FixedStyle.None) && (!checkForcedWidth || double.IsNaN(item.ForcedWidth))))
                {
                    list.Add(item);
                    if (action != null)
                    {
                        action(item);
                    }
                }
            }
            return list;
        }

        protected virtual double GetWidth(BaseColumn column) => 
            double.IsNaN(column.ForcedWidth) ? (double.IsNaN(column.ActualWidth) ? column.Width.Value : column.ActualWidth) : column.ForcedWidth;

        protected virtual bool IsDefinitive(BaseColumn column) => 
            true;

        private bool IsRealStar(BaseColumn column) => 
            (column.ColumnWidth > column.MinWidth) && (column.ColumnWidth < column.MaxWidth);

        protected double ResizeRightColumns(IList columns, int index, double delta, bool hasStar, bool reverse, bool fixStarWidth)
        {
            delta = this.RestorePixelColumns(columns, index, delta, reverse);
            delta = this.FillStarColumns(columns, index, delta, fixStarWidth);
            if (hasStar)
            {
                delta = this.FillPixelColumns(columns, index, delta, reverse);
            }
            return delta;
        }

        protected virtual double RestorePixelColumns(IList initialColumns, int index, double delta, bool reverse)
        {
            double num = 0.0;
            if (reverse)
            {
                initialColumns = initialColumns.Cast<BaseColumn>().Reverse<BaseColumn>().ToList<BaseColumn>();
                index = (initialColumns.Count - 1) - index;
            }
            List<BaseColumn> list = new List<BaseColumn>();
            for (int i = index + 1; i < initialColumns.Count; i++)
            {
                BaseColumn column = (BaseColumn) initialColumns[i];
                if (!column.Width.IsStar && (!column.FixedWidth || !this.IsDefinitive(column)))
                {
                    list.Add(column);
                    GridColumnWidth width = column.Width;
                    num += width.Value - this.GetWidth(column);
                }
            }
            double num2 = delta;
            foreach (BaseColumn column2 in list)
            {
                double num4 = (num != 0.0) ? ((delta * (column2.Width.Value - this.GetWidth(column2))) / num) : 0.0;
                double num5 = Math.Min(Math.Max(column2.ColumnWidth - num4, column2.MinWidth), column2.MaxWidth);
                num5 = (delta >= 0.0) ? Math.Min(column2.ColumnWidth, Math.Max(num5, column2.Width.Value)) : Math.Max(column2.ColumnWidth, Math.Min(num5, column2.Width.Value));
                num2 += num5 - column2.ColumnWidth;
                column2.ForcedWidth = num5;
            }
            return num2;
        }

        protected void SetColumnWidth(BaseColumn resizeColumn, double newWidth)
        {
            newWidth = Math.Max(resizeColumn.MinWidth, newWidth);
            resizeColumn.Width = (resizeColumn.Width.IsAbsolute || (resizeColumn.Fixed != FixedStyle.None)) ? newWidth : new GridColumnWidth((resizeColumn.Width.Value / resizeColumn.ColumnWidth) * newWidth, GridColumnUnitType.Star);
            resizeColumn.ColumnWidth = newWidth;
        }

        private double SetColumnWidth(IList columns, BaseColumn resizeColumn, double newWidth, bool hasStarColumns, bool hasLeftStar, bool checkExcessiveWidth)
        {
            double num = newWidth - resizeColumn.ColumnWidth;
            if (hasStarColumns)
            {
                double num2;
                double num3;
                double num4;
                this.CalcMinMaxWidth(columns, columns.IndexOf(resizeColumn), out num2, out num3, out num4);
                num2 = num4 - num2;
                num3 = num4 - num3;
                double num5 = checkExcessiveWidth ? Math.Round(this.CalcExcessWidth(columns), 4) : 0.0;
                if ((num > 0.0) & hasLeftStar)
                {
                    num = Math.Min(num2, num);
                }
                if (num < 0.0)
                {
                    num = Math.Max(num3 - num5, num);
                }
                if (resizeColumn.Width.IsStar && (num5 > 0.0))
                {
                    newWidth = resizeColumn.ColumnWidth;
                    num = 0.0;
                }
                else
                {
                    newWidth = resizeColumn.ColumnWidth + num;
                    num += Math.Max(num5, 0.0);
                }
            }
            this.SetColumnWidth(resizeColumn, newWidth);
            return num;
        }

        private void SetFixedColumnWidth()
        {
            foreach (BaseColumn column in base.VisibleColumns)
            {
                if ((column.Fixed != FixedStyle.None) && (column.Width.IsAbsolute && double.IsNaN(column.ForcedWidth)))
                {
                    column.Width = column.ActualHeaderWidth - this.ViewInfo.GetHeaderIndentsWidth(column, true);
                    column.ColumnWidth = column.Width.Value;
                }
            }
        }

        protected void UpdateHasStarColumns(IList columns)
        {
            this.HasStarColumns = this.CalcHasLeftStar(columns, null, true);
        }

        protected bool HasStarColumns { get; private set; }

        public override bool AutoWidth =>
            this.HasStarColumns;

        protected internal override bool SupportStarColumns =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.Grid.Native.LayoutCalculator.<>c <>9 = new DevExpress.Xpf.Grid.Native.LayoutCalculator.<>c();
            public static Action<BaseColumn, int, double> <>9__10_0;
            public static Func<BaseColumn, int, double> <>9__22_0;
            public static Func<BaseColumn, int, double> <>9__24_0;
            public static Func<BaseColumn, int, double> <>9__26_0;
            public static Func<BaseColumn, int, double> <>9__26_1;

            internal void <CalcColumnsLayout>b__10_0(BaseColumn column, int index, double width)
            {
                column.ColumnWidth = width;
            }

            internal double <CorrectFixedColumnsWidth>b__22_0(BaseColumn x, int index) => 
                (x.Fixed != FixedStyle.None) ? x.ColumnWidth : 0.0;

            internal double <CorrectFixedRightColumns>b__24_0(BaseColumn x, int i) => 
                x.ColumnWidth;

            internal double <FillByLastFixedColumn>b__26_0(BaseColumn x, int index) => 
                (x.Fixed != FixedStyle.None) ? x.ColumnWidth : 0.0;

            internal double <FillByLastFixedColumn>b__26_1(BaseColumn x, int index) => 
                (x.Fixed == FixedStyle.None) ? x.ColumnWidth : 0.0;
        }
    }
}

