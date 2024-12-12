namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BandedViewAutoWidthColumnsLayoutCalculator : BandedViewColumnsLayoutCalculator
    {
        public BandedViewAutoWidthColumnsLayoutCalculator(GridViewInfo viewInfo) : base(viewInfo)
        {
        }

        protected override void CalcActualLayoutCore(double arrangeWidth, LayoutAssigner layoutAssigner, bool showIndicator, bool needRoundingLastColumn, bool ignoreDetailButtons)
        {
            base.StretchColumnsToWidth(base.BandsLayout.VisibleBands, arrangeWidth, layoutAssigner, needRoundingLastColumn, true);
        }

        protected override double CorrectBandDelta(BandBase band, double delta)
        {
            delta = base.CorrectBandDelta(band, delta);
            return (Math.Min(band.ActualHeaderWidth + delta, this.GetBandMaxWidth(band)) - band.ActualHeaderWidth);
        }

        protected override double CorrectColumnDelta(ColumnBase column, double delta, FixedStyle fixedStyle, bool correctWidths)
        {
            delta = base.CorrectColumnDelta(column, delta, fixedStyle, correctWidths);
            return ((!correctWidths || this.HasSizeableRightColumns(column)) ? (Math.Min(base.GetColumnHeaderWidth(column) + delta, this.GetColumnMaxWidth(column)) - base.GetColumnHeaderWidth(column)) : 0.0);
        }

        private double GetBandMaxWidth(BandBase band)
        {
            double bandsMinWidth = this.GetBandsMinWidth(band, false);
            foreach (BandBase base2 in base.BandsLayout.GetBands(band, true, false))
            {
                bandsMinWidth += base2.ActualHeaderWidth;
            }
            return (this.GetArrangeWidth(this.ViewInfo.ColumnsLayoutSize, LayoutAssigner.Default, base.TableViewBehavior.TableView.ShowIndicator, false) - bandsMinWidth);
        }

        private double GetBandsMinWidth(BandBase band, bool isLeft)
        {
            double num = 0.0;
            foreach (BandBase base2 in base.BandsLayout.GetBands(band, isLeft, false))
            {
                num += base.GetBandMinWidth(base2, null);
            }
            return num;
        }

        private double GetColumnMaxWidth(ColumnBase column)
        {
            double bandsMinWidth = this.GetBandsMinWidth(column.ParentBand, false);
            for (int i = column.BandRow.Columns.IndexOf(column) + 1; i < column.BandRow.Columns.Count; i++)
            {
                bandsMinWidth += AutoWidthHelper.GetColumnFixedWidth(column.BandRow.Columns[i], this.ViewInfo, true);
            }
            foreach (BandBase base2 in base.BandsLayout.GetBands(column.ParentBand, true, false))
            {
                bandsMinWidth += base2.ActualHeaderWidth;
            }
            for (int j = column.BandRow.Columns.IndexOf(column) - 1; j >= 0; j--)
            {
                bandsMinWidth += base.GetColumnHeaderWidth(column.BandRow.Columns[j]);
            }
            return (this.GetArrangeWidth(this.ViewInfo.ColumnsLayoutSize, LayoutAssigner.Default, base.TableViewBehavior.TableView.ShowIndicator, false) - bandsMinWidth);
        }

        private bool HasSizeableRightColumns(ColumnBase column)
        {
            bool flag;
            int num = column.BandRow.Columns.IndexOf(column) + 1;
            while (true)
            {
                if (num < column.BandRow.Columns.Count)
                {
                    if (!column.BandRow.Columns[num].FixedWidth)
                    {
                        return true;
                    }
                    num++;
                    continue;
                }
                using (IEnumerator enumerator = base.BandsLayout.GetBands(column.ParentBand, false, false).GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            BandBase current = (BandBase) enumerator.Current;
                            using (IEnumerator<BaseColumn> enumerator2 = base.GetBandColumns(current, true).GetEnumerator())
                            {
                                while (true)
                                {
                                    if (!enumerator2.MoveNext())
                                    {
                                        break;
                                    }
                                    BaseColumn column2 = enumerator2.Current;
                                    if (!column2.FixedWidth)
                                    {
                                        return true;
                                    }
                                }
                            }
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    }
                }
                break;
            }
            return flag;
        }

        protected override void OnBandResize(BandBase band, double delta)
        {
            base.SetDefaultColumnSize(base.BandsLayout.GetBands(band, true, false));
            IList bands = base.BandsLayout.GetBands(band, false, false);
            if (delta > 0.0)
            {
                base.IncreaseBandsWidth(bands, delta);
            }
            else
            {
                base.DecreaseBandsWidth(bands, delta);
            }
        }

        public override bool AutoWidth =>
            true;
    }
}

