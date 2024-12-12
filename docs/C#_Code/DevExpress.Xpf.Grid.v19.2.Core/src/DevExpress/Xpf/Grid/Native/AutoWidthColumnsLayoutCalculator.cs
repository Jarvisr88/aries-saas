namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;

    public class AutoWidthColumnsLayoutCalculator : ColumnsLayoutCalculator
    {
        public AutoWidthColumnsLayoutCalculator(GridViewInfo viewInfo) : base(viewInfo)
        {
        }

        protected override void ApplyResizeCore(BaseColumn resizeColumn, double newWidth, double maxWidth, double indentWidth, bool correctWidths)
        {
            this.ExtractWidth();
            newWidth = Math.Max(resizeColumn.MinWidth, newWidth - indentWidth);
            newWidth = Math.Min(maxWidth, newWidth);
            bool flag = resizeColumn.Visible && this.ViewInfo.GridView.IsColumnVisibleInHeaders(resizeColumn);
            if (!(correctWidths & flag))
            {
                resizeColumn.ActualWidth = newWidth;
            }
            else
            {
                int visibleIndex = base.GetVisibleIndex(resizeColumn);
                this.UpdateWidths(visibleIndex, newWidth - base.ExtWidth[visibleIndex]);
                base.ExtWidth[visibleIndex] = newWidth;
                this.UpdateColumnsWidthFromExtWidth();
            }
            if (!base.TableViewBehavior.BestFitLocker.IsLocked)
            {
                this.UpdateColumnsActualAllowResizing();
            }
        }

        protected override void CalcActualLayoutCore(double arrangeWidth, LayoutAssigner layoutAssigner, bool showIndicator, bool needRoundingLastColumn, bool ignoreDetailButtons)
        {
            AutoWidthHelper.CalcColumnLayout(base.VisibleColumns as IList, arrangeWidth, this.ViewInfo, layoutAssigner, needRoundingLastColumn, true);
        }

        public override double CalcColumnMaxWidth(ColumnBase column)
        {
            this.ExtractWidth();
            return this.CalcMaxWidth(base.GetVisibleIndex(column));
        }

        protected virtual double CalcColumnsExtWidth(int startColumn) => 
            this.CalcColumnsExtWidth(startColumn, base.ExtWidth.Length - startColumn, false);

        protected virtual double CalcColumnsExtWidth(int startColumn, bool skipFixedWidth) => 
            this.CalcColumnsExtWidth(startColumn, base.ExtWidth.Length - startColumn, skipFixedWidth);

        protected virtual double CalcColumnsExtWidth(int startColumn, int count, bool skipFixedWidth)
        {
            double num = 0.0;
            for (int i = startColumn; i < (startColumn + count); i++)
            {
                if (!skipFixedWidth || !base.VisibleColumns[i].FixedWidth)
                {
                    num += base.ExtWidth[i];
                }
            }
            return num;
        }

        protected virtual double CalcColumnsMinWidth(int startColumn) => 
            this.CalcColumnsMinWidth(startColumn, base.VisibleColumns.Count - startColumn);

        protected virtual double CalcColumnsMinWidth(int startColumn, int count)
        {
            double num = 0.0;
            for (int i = startColumn; i < (startColumn + count); i++)
            {
                num += base.VisibleColumns[i].FixedWidth ? base.ExtWidth[i] : base.VisibleColumns[i].MinWidth;
            }
            return num;
        }

        protected virtual double CalcMaxWidth(int resizeColumn)
        {
            double num = (this.CalcColumnsExtWidth(0) - this.CalcColumnsExtWidth(0, resizeColumn, false)) - this.CalcColumnsMinWidth(resizeColumn + 1);
            if ((Math.Abs((double) (num - base.ExtWidth[resizeColumn])) < 1.0) && ((resizeColumn < (base.VisibleColumns.Count - 1)) && base.VisibleColumns[resizeColumn + 1].GetAllowResizing()))
            {
                num += base.ExtWidth[resizeColumn + 1] - base.VisibleColumns[resizeColumn + 1].MinWidth;
            }
            return num;
        }

        protected virtual int CalcNonFixedWidthColumnCount(int startIndex, int columnCount)
        {
            int num = 0;
            for (int i = startIndex; i < columnCount; i++)
            {
                if (!base.VisibleColumns[i].FixedWidth)
                {
                    num++;
                }
            }
            return num;
        }

        protected virtual double CalcTotalColumnsSizeableWidth(int startIndex, int columnCount)
        {
            double num = 0.0;
            for (int i = startIndex; i < columnCount; i++)
            {
                num += this.GetColumnSizeableWidth(i);
            }
            return num;
        }

        protected virtual unsafe double DecreaseWidth(int columnIndex, double delta)
        {
            double columnSizeableWidth = this.GetColumnSizeableWidth(columnIndex);
            if (columnSizeableWidth != 0.0)
            {
                delta = Math.Min(columnSizeableWidth, delta);
                double* numPtr1 = &(base.ExtWidth[columnIndex]);
                numPtr1[0] -= delta;
            }
            return delta;
        }

        protected virtual void DecreaseWidths(int decStartIndex, double totalDecValue, int columnCount)
        {
            if (decStartIndex < columnCount)
            {
                double num = this.CalcTotalColumnsSizeableWidth(decStartIndex, columnCount);
                if ((num < totalDecValue) && base.VisibleColumns[decStartIndex].GetAllowResizing())
                {
                    base.ExtWidth[decStartIndex] = Math.Max(base.VisibleColumns[decStartIndex].MinWidth, base.ExtWidth[decStartIndex] - totalDecValue);
                }
                for (int i = decStartIndex; i < columnCount; i++)
                {
                    this.DecreaseWidth(i, (this.GetColumnSizeableWidth(i) / num) * totalDecValue);
                }
            }
        }

        protected virtual void ExtractWidth()
        {
            for (int i = 0; i < base.VisibleColumns.Count; i++)
            {
                base.ExtWidth[i] = base.VisibleColumns[i].HeaderWidth - this.ViewInfo.GetHeaderIndentsWidth(base.VisibleColumns[i], true);
            }
        }

        protected virtual double GetColumnSizeableWidth(int columnIndex) => 
            !base.VisibleColumns[columnIndex].FixedWidth ? Math.Max((double) 0.0, (double) (base.ExtWidth[columnIndex] - base.VisibleColumns[columnIndex].MinWidth)) : 0.0;

        protected virtual double GetDeltaWidth(int incStartIndex, double totalIncValue, int columnCount)
        {
            int num = this.CalcNonFixedWidthColumnCount(incStartIndex, columnCount);
            return ((num != 0) ? (totalIncValue / ((double) num)) : 0.0);
        }

        protected virtual unsafe void IncreaseWidths(int incStartIndex, double totalIncValue, int columnCount)
        {
            if (incStartIndex < columnCount)
            {
                double num = this.GetDeltaWidth(incStartIndex, totalIncValue, columnCount);
                double num2 = 0.0;
                for (int i = incStartIndex; i < columnCount; i++)
                {
                    if (!base.VisibleColumns[i].FixedWidth)
                    {
                        num2 += num;
                        double* numPtr1 = &(base.ExtWidth[i]);
                        numPtr1[0] += num;
                    }
                }
                if ((num2 == 0.0) && base.VisibleColumns[incStartIndex].GetAllowResizing())
                {
                    double* numPtr2 = &(base.ExtWidth[incStartIndex]);
                    numPtr2[0] += totalIncValue;
                }
            }
        }

        protected virtual void UpdateColumnsWidthFromExtWidth()
        {
            for (int i = 0; i < base.VisibleColumns.Count; i++)
            {
                base.VisibleColumns[i].ActualWidth = base.ExtWidth[i];
            }
        }

        protected virtual void UpdateWidths(int columnsIndex, double delta)
        {
            if (columnsIndex == (base.VisibleColumns.Count - 1))
            {
                if (delta < 0.0)
                {
                    this.IncreaseWidths(0, -delta, base.VisibleColumns.Count - 1);
                }
                else
                {
                    this.DecreaseWidths(0, delta, base.VisibleColumns.Count - 1);
                }
            }
            else
            {
                int incStartIndex = columnsIndex + 1;
                if (delta < 0.0)
                {
                    this.IncreaseWidths(incStartIndex, -delta, base.VisibleColumns.Count);
                }
                else
                {
                    this.DecreaseWidths(incStartIndex, delta, base.VisibleColumns.Count);
                }
            }
        }

        public override bool AutoWidth =>
            true;
    }
}

