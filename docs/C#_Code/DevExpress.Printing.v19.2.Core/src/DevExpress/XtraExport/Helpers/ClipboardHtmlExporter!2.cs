namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ClipboardHtmlExporter<TCol, TRow> : IClipboardExporter<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private double bandedHeaderColumnWidth;
        private DevExpress.XtraExport.Helpers.HtmlDocument document;
        private int maxColumnCount;
        private HtmlTable table;

        public ClipboardHtmlExporter()
        {
            this.bandedHeaderColumnWidth = -1.0;
            this.maxColumnCount = -1;
        }

        public void AddBandedHeader(ClipboardBandLayoutInfo info)
        {
            this.maxColumnCount = Math.Max(info.GetBandPanelRowWidth(0, false), info.GetColumnPanelRowWidth(0, -1));
            double columnWidth = 100.0 / ((double) this.maxColumnCount);
            this.bandedHeaderColumnWidth = columnWidth;
            bool[,] full = new bool[info.HeaderPanelInfo.Length, this.maxColumnCount];
            int index = 0;
            while (index < info.HeaderPanelInfo.Length)
            {
                HtmlTableRow row = this.table.CreateRow();
                int rowWidth = 0;
                List<ClipboardBandCellInfo> list = info.HeaderPanelInfo[index];
                int num4 = 0;
                while (true)
                {
                    if (num4 >= list.Count)
                    {
                        index++;
                        break;
                    }
                    ClipboardBandCellInfo cellInfo = list[num4];
                    rowWidth = this.ValidateEmptyCells(full, index, row, rowWidth, cellInfo);
                    this.CreateCell(row, cellInfo, columnWidth);
                    num4++;
                }
            }
        }

        public void AddGroupHeader(ClipboardCellInfo groupHeaderInfo, int columnCount)
        {
            HtmlTableCell cell = this.table.CreateRow().CreateCell();
            cell.ColSpan = columnCount;
            cell.Value = groupHeaderInfo.DisplayValue;
            this.ApplyCellFormatting(cell, groupHeaderInfo.Formatting);
        }

        public void AddHeaders(IEnumerable<ClipboardCellInfo> headerInfo)
        {
            HtmlTableRow row = this.table.CreateRow();
            int num = headerInfo.Count<ClipboardCellInfo>();
            double num2 = 100.0 / ((double) num);
            for (int i = 0; i < num; i++)
            {
                ClipboardCellInfo info = headerInfo.ElementAt<ClipboardCellInfo>(i);
                HtmlTableCell cell = row.CreateCell();
                if (info.AllowHtmlDraw)
                {
                    cell.FormattedValue = info.DisplayValueFormatted;
                }
                else
                {
                    cell.Value = info.DisplayValue;
                }
                cell.WidthInPercent = num2;
                this.ApplyCellFormatting(cell, info.Formatting);
            }
        }

        public void AddRow(IEnumerable<ClipboardCellInfo> rowInfo)
        {
            double num = 100.0 / ((double) rowInfo.Count<ClipboardCellInfo>());
            HtmlTableRow row = this.table.CreateRow();
            int num2 = rowInfo.Count<ClipboardCellInfo>();
            for (int i = 0; i < num2; i++)
            {
                ClipboardCellInfo info = rowInfo.ElementAt<ClipboardCellInfo>(i);
                HtmlTableCell cell = row.CreateCell();
                cell.Value = info.DisplayValue;
                if (info.IsHyperlink)
                {
                    cell.TargetUrl = cell.Value.ToString();
                }
                this.ApplyCellFormatting(cell, info.Formatting);
            }
        }

        public void AddRow(IEnumerable<ClipboardBandCellInfo>[] rowInfo)
        {
            Func<IEnumerable<ClipboardBandCellInfo>, int> selector = <>c<TCol, TRow>.<>9__18_0;
            if (<>c<TCol, TRow>.<>9__18_0 == null)
            {
                Func<IEnumerable<ClipboardBandCellInfo>, int> local1 = <>c<TCol, TRow>.<>9__18_0;
                selector = <>c<TCol, TRow>.<>9__18_0 = x => x.Count<ClipboardBandCellInfo>();
            }
            this.maxColumnCount = rowInfo.Max<IEnumerable<ClipboardBandCellInfo>>(selector);
            double bandedHeaderColumnWidth = this.bandedHeaderColumnWidth;
            bool[,] full = new bool[rowInfo.Length, this.maxColumnCount];
            int index = 0;
            while (index < rowInfo.Length)
            {
                HtmlTableRow row = this.table.CreateRow();
                int rowWidth = 0;
                IEnumerable<ClipboardBandCellInfo> source = rowInfo[index];
                int num4 = 0;
                while (true)
                {
                    if (num4 >= source.Count<ClipboardBandCellInfo>())
                    {
                        index++;
                        break;
                    }
                    ClipboardBandCellInfo cellInfo = source.ElementAt<ClipboardBandCellInfo>(num4);
                    rowWidth = this.ValidateEmptyCells(full, index, row, rowWidth, cellInfo);
                    this.CreateCell(row, cellInfo, bandedHeaderColumnWidth);
                    num4++;
                }
            }
        }

        private void ApplyAlignmentAndBackground(HtmlTableCell cell, XlCellAlignment alignment, XlFill fill)
        {
            if (alignment != null)
            {
                this.ApplyVertivalAlignment(cell, alignment.VerticalAlignment);
                this.ApplyHorizontalAlignment(cell, alignment.HorizontalAlignment);
                cell.CellIndent = alignment.Indent;
            }
            if (fill != null)
            {
                cell.BackColor = (Color) fill.BackColor;
            }
        }

        private void ApplyBorderFormatting(HtmlTableCell cell, XlBorder border)
        {
            if (border != null)
            {
                cell.LeftBorder.Color = (Color) border.LeftColor;
                cell.LeftBorder.Style = this.GetBorderStyle(border.LeftLineStyle);
                cell.TopBorder.Color = (Color) border.TopColor;
                cell.TopBorder.Style = this.GetBorderStyle(border.TopLineStyle);
                cell.RightBorder.Color = (Color) border.RightColor;
                cell.RightBorder.Style = this.GetBorderStyle(border.RightLineStyle);
                cell.BottomBorder.Color = (Color) border.BottomColor;
                cell.BottomBorder.Style = this.GetBorderStyle(border.RightLineStyle);
            }
        }

        private void ApplyCellFormatting(HtmlTableCell cell, XlCellFormatting format)
        {
            this.ApplyAlignmentAndBackground(cell, format.Alignment, format.Fill);
            this.ApplyBorderFormatting(cell, format.Border);
            this.ApplyFontFormatting(cell, format.Font);
        }

        private void ApplyFontFormatting(HtmlTableCell cell, XlFont font)
        {
            if (font != null)
            {
                cell.FontName = font.Name;
                cell.FontSize = font.Size;
                cell.ForeColor = font.Color.Rgb;
                cell.FontStyle = FontHelper.GetFontStyle(font);
            }
        }

        private void ApplyHorizontalAlignment(HtmlTableCell cell, XlHorizontalAlignment horizontalAlignment)
        {
            switch (horizontalAlignment)
            {
                case XlHorizontalAlignment.General:
                case XlHorizontalAlignment.Left:
                case XlHorizontalAlignment.Fill:
                case XlHorizontalAlignment.Distributed:
                    cell.HAlignment = HtmlCellHAlignment.Left;
                    return;

                case XlHorizontalAlignment.Center:
                case XlHorizontalAlignment.CenterContinuous:
                    cell.HAlignment = HtmlCellHAlignment.Center;
                    return;

                case XlHorizontalAlignment.Right:
                    cell.HAlignment = HtmlCellHAlignment.Right;
                    return;

                case XlHorizontalAlignment.Justify:
                    cell.HAlignment = HtmlCellHAlignment.Justify;
                    return;
            }
        }

        private void ApplyVertivalAlignment(HtmlTableCell cell, XlVerticalAlignment verticalAlignment)
        {
            switch (verticalAlignment)
            {
                case XlVerticalAlignment.Top:
                    cell.VAlignment = HtmlCellVAlignment.Top;
                    return;

                case XlVerticalAlignment.Center:
                case XlVerticalAlignment.Justify:
                case XlVerticalAlignment.Distributed:
                    cell.VAlignment = HtmlCellVAlignment.Center;
                    return;

                case XlVerticalAlignment.Bottom:
                    cell.VAlignment = HtmlCellVAlignment.Bottom;
                    return;
            }
        }

        public void BeginExport()
        {
            this.document = new DevExpress.XtraExport.Helpers.HtmlDocument();
            this.table = this.document.Body.CreateTable();
        }

        private void CreateCell(HtmlTableRow row, ClipboardBandCellInfo cellInfo, double columnWidth)
        {
            HtmlTableCell cell = row.CreateCell();
            if (cellInfo.AllowHtmlDraw)
            {
                cell.FormattedValue = cellInfo.DisplayValueFormatted;
            }
            else
            {
                cell.Value = cellInfo.DisplayValue;
            }
            if (cellInfo.IsHyperlink)
            {
                cell.TargetUrl = cell.Value.ToString();
            }
            cell.WidthInPercent = columnWidth;
            cell.ColSpan = cellInfo.Width;
            cell.RowSpan = cellInfo.Height;
            this.ApplyCellFormatting(cell, cellInfo.Formatting);
        }

        private void CreateEmptyCell(HtmlTableRow row, int emptyWidth)
        {
            HtmlTableCell cell = row.CreateCell();
            cell.ColSpan = emptyWidth;
            cell.LeftBorder.Style = HtmlCellBorderStyle.None;
            cell.TopBorder.Style = HtmlCellBorderStyle.None;
            cell.RightBorder.Style = HtmlCellBorderStyle.None;
            cell.BottomBorder.Style = HtmlCellBorderStyle.None;
        }

        public void EndExport()
        {
        }

        private HtmlCellBorderStyle GetBorderStyle(XlBorderLineStyle style)
        {
            HtmlCellBorderStyle thin = HtmlCellBorderStyle.Thin;
            Enum.TryParse<HtmlCellBorderStyle>(style.ToString(), out thin);
            return thin;
        }

        public void SetDataObject(DataObject data)
        {
            data.SetText(this.document.Compile(), TextDataFormat.Html);
        }

        private int ValidateEmptyCells(bool[,] full, int i, HtmlTableRow row, int rowWidth, ClipboardBandCellInfo cellInfo)
        {
            rowWidth += cellInfo.SpaceBefore;
            int num2 = 0;
            while (num2 < cellInfo.Height)
            {
                int num3 = 0;
                while (true)
                {
                    if (num3 >= cellInfo.Width)
                    {
                        num2++;
                        break;
                    }
                    full[i + num2, rowWidth + num3] = true;
                    num3++;
                }
            }
            rowWidth += cellInfo.Width;
            int emptyWidth = 0;
            for (int j = 0; j < rowWidth; j++)
            {
                if (!full[i, j])
                {
                    emptyWidth++;
                }
            }
            if (emptyWidth != 0)
            {
                this.CreateEmptyCell(row, emptyWidth);
                for (int k = 0; k < rowWidth; k++)
                {
                    full[i, k] = true;
                }
            }
            return rowWidth;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ClipboardHtmlExporter<TCol, TRow>.<>c <>9;
            public static Func<IEnumerable<ClipboardBandCellInfo>, int> <>9__18_0;

            static <>c()
            {
                ClipboardHtmlExporter<TCol, TRow>.<>c.<>9 = new ClipboardHtmlExporter<TCol, TRow>.<>c();
            }

            internal int <AddRow>b__18_0(IEnumerable<ClipboardBandCellInfo> x) => 
                x.Count<ClipboardBandCellInfo>();
        }
    }
}

