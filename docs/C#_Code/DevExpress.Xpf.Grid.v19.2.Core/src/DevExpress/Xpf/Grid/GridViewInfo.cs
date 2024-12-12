namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class GridViewInfo
    {
        public const double DefaultColumnWidth = 120.0;
        public const double DefaultHeaderHeight = 20.0;
        public const double DefaultRowHeight = 18.0;
        public const double FixedNoneMinWidth = 1.0;
        private DataViewBase gridView;
        private Size columnsLayoutSize;
        private double verticalScrollBarWidth;

        public GridViewInfo(DataViewBase gridView)
        {
            this.gridView = gridView;
            this.LayoutCalculatorFactory = new GridTableViewLayoutCalculatorFactory();
        }

        public void CalcColumnsLayout()
        {
            this.TableView.TableViewBehavior.ColumnsLayoutCalculator.CalcActualLayout(this.ColumnsLayoutSize);
        }

        private static double CalcMax(double arg1, double arg2) => 
            Math.Max(arg1, arg2);

        protected internal ColumnsLayoutCalculator CreateColumnsLayoutCalculator() => 
            this.CreateColumnsLayoutCalculator(this.GetAutoWidth());

        protected internal virtual ColumnsLayoutCalculator CreateColumnsLayoutCalculator(bool autoWidth) => 
            (autoWidth || (this.GridView.DataControl.BandsLayoutCore != null)) ? this.LayoutCalculatorFactory.CreateCalculator(this, autoWidth) : new LayoutCalculator(this);

        protected virtual bool GetAutoWidth() => 
            this.TableView.AutoWidth;

        public double GetColumnDataWidth(BaseColumn column)
        {
            Func<BaseColumn, double> method = <>c.<>9__41_0;
            if (<>c.<>9__41_0 == null)
            {
                Func<BaseColumn, double> local1 = <>c.<>9__41_0;
                method = <>c.<>9__41_0 = c => CalcMax(c.ActualWidth, c.MinWidth);
            }
            return this.GetColumnWidthCore(column, method);
        }

        public double GetColumnFixedWidthCore(BaseColumn column) => 
            column.MinWidth + this.GetHeaderIndentsWidth(column, true);

        public double GetColumnHeaderWidth(BaseColumn column) => 
            this.GetColumnDataWidth(column) + this.GetHeaderIndentsWidth(column, true);

        protected double GetColumnWidthCore(BaseColumn column, Func<BaseColumn, double> method)
        {
            if (column == null)
            {
                return 120.0;
            }
            double d = method(column);
            return (double.IsNaN(d) ? CalcMax(column.MinWidth, 120.0) : d);
        }

        public double GetDesiredColumnsWidth(IEnumerable columns)
        {
            double num = 0.0;
            foreach (BaseColumn column in columns)
            {
                num += this.GetColumnWidthCore(column, c => this.GetColumnHeaderWidth(c));
            }
            return num;
        }

        public double GetDesiredColumnWidth(BaseColumn column) => 
            this.GetColumnWidthCore(column, c => this.GetColumnHeaderWidth(c));

        public double GetHeaderIndentsWidth(BaseColumn column, bool calcBandSeparator = true)
        {
            double num = this.gridView.ViewBehavior.IsFirstOrTreeColumn(column, this.IsPrinting) ? this.FirstColumnIndent : 0.0;
            if ((column != null) & calcBandSeparator)
            {
                num += column.ActualBandRightSeparatorWidthCore + column.ActualBandLeftSeparatorWidthCore;
            }
            return num;
        }

        internal bool HasStarredColumns()
        {
            bool flag;
            using (IEnumerator<ColumnBase> enumerator = this.VisibleColumns.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        ColumnBase current = enumerator.Current;
                        GridColumnWidth width = current.Width;
                        if (!width.IsStar)
                        {
                            continue;
                        }
                        flag = true;
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

        public Size ColumnsLayoutSize
        {
            get => 
                this.columnsLayoutSize;
            set
            {
                if (this.columnsLayoutSize != value)
                {
                    this.columnsLayoutSize = value;
                    this.GridView.UpdateColumnsPositions();
                }
            }
        }

        public double VerticalScrollBarWidth
        {
            get => 
                this.verticalScrollBarWidth;
            set
            {
                if (this.verticalScrollBarWidth != value)
                {
                    this.verticalScrollBarWidth = value;
                    this.GridView.UpdateColumnsPositions();
                }
            }
        }

        public DataViewBase GridView =>
            this.gridView;

        public ITableView TableView =>
            (ITableView) this.GridView;

        public DataControlBase Grid =>
            this.GridView.DataControl;

        public virtual IList<ColumnBase> VisibleColumns =>
            this.GridView.VisibleColumnsCore;

        public virtual int GroupCount =>
            (this.Grid != null) ? this.Grid.ActualLevelCount : 0;

        public virtual double TotalGroupAreaIndent =>
            (this.IsPrinting ? this.TableView.ViewBase.PrintRowIndentWidth : this.TableView.LeftGroupAreaIndent) * this.GroupCount;

        public virtual double RightGroupAreaIndent =>
            (this.GroupCount != 0) ? this.TableView.RightGroupAreaIndent : 0.0;

        public GridTableViewLayoutCalculatorFactory LayoutCalculatorFactory { get; set; }

        public virtual BandsLayoutBase BandsLayout =>
            this.GridView.DataControl.BandsLayoutCore;

        internal double ActualLeftDataAreaIndent =>
            this.TableView.ShowIndicator ? 0.0 : this.TableView.LeftDataAreaIndent;

        internal double FirstColumnIndent =>
            this.ActualLeftDataAreaIndent + this.NewItemRowIndent;

        internal virtual double NewItemRowIndent =>
            this.TableView.ActualShowDetailHeader ? 0.0 : this.TotalGroupAreaIndent;

        internal virtual bool IsPrinting =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GridViewInfo.<>c <>9 = new GridViewInfo.<>c();
            public static Func<BaseColumn, double> <>9__41_0;

            internal double <GetColumnDataWidth>b__41_0(BaseColumn c) => 
                GridViewInfo.CalcMax(c.ActualWidth, c.MinWidth);
        }
    }
}

