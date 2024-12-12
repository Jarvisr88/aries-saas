namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class ClipboardRtfExporterNew<TCol, TRow> : IClipboardExporter<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private int bandedHeaderColumnWidth;
        private RtfDocument document;
        private RtfTable table;

        public ClipboardRtfExporterNew()
        {
            this.bandedHeaderColumnWidth = -1;
        }

        public void AddBandedHeader(ClipboardBandLayoutInfo info)
        {
            int columnWidth = RtfDocument.TableMaxWidth / Math.Max(info.GetBandPanelRowWidth(0, false), info.GetColumnPanelRowWidth(0, -1));
            this.bandedHeaderColumnWidth = columnWidth;
            List<RtfTableRow> rows = new List<RtfTableRow>();
            Dictionary<RtfTableRowCell, ClipboardBandCellInfo> merged = new Dictionary<RtfTableRowCell, ClipboardBandCellInfo>();
            int index = 0;
            while (index < info.HeaderPanelInfo.Length)
            {
                int tableLeftOffset = RtfDocument.TableLeftOffset;
                List<ClipboardBandCellInfo> list2 = info.HeaderPanelInfo[index];
                RtfTableRow item = this.table.CreateRow();
                rows.Add(item);
                int num4 = 0;
                while (true)
                {
                    if (num4 >= list2.Count)
                    {
                        index++;
                        break;
                    }
                    ClipboardBandCellInfo cellInfo = list2[num4];
                    tableLeftOffset = this.CreateEmptyCells(item, cellInfo.SpaceBefore, columnWidth, tableLeftOffset);
                    RtfTableRowCell key = this.CreateCell(item, cellInfo, columnWidth, ref tableLeftOffset, true);
                    if ((cellInfo.Width > 1) || (cellInfo.Height > 1))
                    {
                        merged.Add(key, cellInfo);
                    }
                    tableLeftOffset = this.CreateEmptyCells(item, cellInfo.SpaceAfter, columnWidth, tableLeftOffset);
                    num4++;
                }
            }
            this.DoMerge(rows, merged);
        }

        public void AddGroupHeader(ClipboardCellInfo groupHeaderInfo, int columnCount)
        {
            RtfTableRowCell cell = this.table.CreateRow().CreateCell();
            cell.CellRightBound = RtfDocument.TableMaxWidth;
            cell.Value = groupHeaderInfo.DisplayValue;
            this.ApplyCellFormatting(cell, groupHeaderInfo.Formatting);
        }

        public void AddHeaders(IEnumerable<ClipboardCellInfo> headerInfo)
        {
            int num = headerInfo.Count<ClipboardCellInfo>();
            int num2 = RtfDocument.TableMaxWidth / num;
            RtfTableRow row = this.table.CreateRow();
            int tableLeftOffset = RtfDocument.TableLeftOffset;
            for (int i = 0; i < num; i++)
            {
                ClipboardCellInfo info = headerInfo.ElementAt<ClipboardCellInfo>(i);
                RtfTableRowCell cell = row.CreateCell();
                cell.CellRightBound = (i == (num - 1)) ? RtfDocument.TableMaxWidth : (tableLeftOffset + num2);
                tableLeftOffset = cell.CellRightBound;
                if (info.AllowHtmlDraw)
                {
                    cell.FormattedValue = info.DisplayValueFormatted;
                }
                else
                {
                    cell.Value = !string.IsNullOrEmpty(info.DisplayValue) ? info.DisplayValue : info.Value.ToString();
                }
                this.ApplyCellFormatting(cell, info.Formatting);
            }
        }

        public void AddRow(IEnumerable<ClipboardCellInfo> rowInfo)
        {
            int num = RtfDocument.TableMaxWidth / rowInfo.Count<ClipboardCellInfo>();
            RtfTableRow row = this.table.CreateRow();
            int tableLeftOffset = RtfDocument.TableLeftOffset;
            int num3 = rowInfo.Count<ClipboardCellInfo>();
            for (int i = 0; i < num3; i++)
            {
                ClipboardCellInfo info = rowInfo.ElementAt<ClipboardCellInfo>(i);
                RtfTableRowCell cell = row.CreateCell();
                cell.CellRightBound = (i == (num3 - 1)) ? RtfDocument.TableMaxWidth : (tableLeftOffset + num);
                tableLeftOffset = cell.CellRightBound;
                if (info.IsHyperlink)
                {
                    cell.TargetUrl = info.Value.ToString();
                }
                cell.Value = info.DisplayValue;
                this.ApplyCellFormatting(cell, info.Formatting);
            }
        }

        public void AddRow(IEnumerable<ClipboardBandCellInfo>[] rowInfo)
        {
            int bandedHeaderColumnWidth = this.bandedHeaderColumnWidth;
            List<RtfTableRow> rows = new List<RtfTableRow>();
            Dictionary<RtfTableRowCell, ClipboardBandCellInfo> merged = new Dictionary<RtfTableRowCell, ClipboardBandCellInfo>();
            int index = 0;
            while (index < rowInfo.Length)
            {
                int tableLeftOffset = RtfDocument.TableLeftOffset;
                IEnumerable<ClipboardBandCellInfo> source = rowInfo[index];
                RtfTableRow item = this.table.CreateRow();
                rows.Add(item);
                int num4 = source.Count<ClipboardBandCellInfo>();
                int num5 = 0;
                while (true)
                {
                    if (num5 >= num4)
                    {
                        index++;
                        break;
                    }
                    ClipboardBandCellInfo cellInfo = source.ElementAt<ClipboardBandCellInfo>(num5);
                    tableLeftOffset = this.CreateEmptyCells(item, cellInfo.SpaceBefore, bandedHeaderColumnWidth, tableLeftOffset);
                    RtfTableRowCell key = this.CreateCell(item, cellInfo, bandedHeaderColumnWidth, ref tableLeftOffset, false);
                    if ((cellInfo.Width > 1) || (cellInfo.Height > 1))
                    {
                        merged.Add(key, cellInfo);
                    }
                    tableLeftOffset = this.CreateEmptyCells(item, cellInfo.SpaceAfter, bandedHeaderColumnWidth, tableLeftOffset);
                    num5++;
                }
            }
            this.DoMerge(rows, merged);
        }

        private void ApplyAlignmentAndBackground(RtfTableRowCell cell, XlCellAlignment alignment, XlFill fill)
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

        private void ApplyBorderFormatting(RtfTableRowCell cell, XlBorder border)
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
                cell.BottomBorder.Style = this.GetBorderStyle(border.BottomLineStyle);
            }
        }

        private void ApplyCellFormatting(RtfTableRowCell cell, XlCellFormatting format)
        {
            this.ApplyAlignmentAndBackground(cell, format.Alignment, format.Fill);
            this.ApplyBorderFormatting(cell, format.Border);
            this.ApplyFontFormatting(cell, format.Font);
        }

        private void ApplyFontFormatting(RtfTableRowCell cell, XlFont font)
        {
            if (font != null)
            {
                cell.FontName = font.Name;
                cell.FontSize = Convert.ToInt32((double) (2.0 * font.Size));
                cell.ForeColor = font.Color.Rgb;
                cell.FontStyle = FontHelper.GetFontStyle(font);
            }
        }

        private void ApplyHorizontalAlignment(RtfTableRowCell cell, XlHorizontalAlignment horizontalAlignment)
        {
            switch (horizontalAlignment)
            {
                case XlHorizontalAlignment.General:
                case XlHorizontalAlignment.Left:
                case XlHorizontalAlignment.Fill:
                case XlHorizontalAlignment.Distributed:
                    cell.HAlignment = RtfCellHAlignment.Left;
                    return;

                case XlHorizontalAlignment.Center:
                case XlHorizontalAlignment.CenterContinuous:
                    cell.HAlignment = RtfCellHAlignment.Center;
                    return;

                case XlHorizontalAlignment.Right:
                    cell.HAlignment = RtfCellHAlignment.Right;
                    return;

                case XlHorizontalAlignment.Justify:
                    cell.HAlignment = RtfCellHAlignment.Justify;
                    return;
            }
        }

        private void ApplyVertivalAlignment(RtfTableRowCell cell, XlVerticalAlignment verticalAlignment)
        {
            switch (verticalAlignment)
            {
                case XlVerticalAlignment.Top:
                    cell.VAlignment = RtfCellVAlignment.Top;
                    return;

                case XlVerticalAlignment.Center:
                case XlVerticalAlignment.Justify:
                case XlVerticalAlignment.Distributed:
                    cell.VAlignment = RtfCellVAlignment.Center;
                    return;

                case XlVerticalAlignment.Bottom:
                    cell.VAlignment = RtfCellVAlignment.Bottom;
                    return;
            }
        }

        public void BeginExport()
        {
            this.document = new RtfDocument();
            this.table = this.document.CreateTable();
        }

        private RtfTableRowCell CreateCell(RtfTableRow row, ClipboardBandCellInfo cellInfo, int columnWidth, ref int lastRightBound, bool allowHtml = false)
        {
            RtfTableRowCell cell = row.CreateCell();
            cell.CellRightBound = lastRightBound + columnWidth;
            lastRightBound = cell.CellRightBound;
            this.ApplyCellFormatting(cell, cellInfo.Formatting);
            if (allowHtml && cellInfo.AllowHtmlDraw)
            {
                cell.FormattedValue = cellInfo.DisplayValueFormatted;
            }
            else
            {
                cell.Value = cellInfo.DisplayValue;
            }
            if (cellInfo.IsHyperlink)
            {
                cell.TargetUrl = cellInfo.Value.ToString();
            }
            if (cellInfo.Width > 1)
            {
                for (int i = 1; i < cellInfo.Width; i++)
                {
                    RtfTableRowCell cell2 = row.CreateCell();
                    cell2.CellRightBound = lastRightBound + columnWidth;
                    lastRightBound = cell2.CellRightBound;
                }
            }
            return cell;
        }

        private int CreateEmptyCells(RtfTableRow row, int cellCount, int columnWidth, int lastRightBound)
        {
            for (int i = 0; i < cellCount; i++)
            {
                RtfTableRowCell cell = row.CreateCell();
                cell.CellRightBound = lastRightBound + columnWidth;
                cell.LeftBorder.Style = RtfCellBorderStyle.None;
                cell.TopBorder.Style = RtfCellBorderStyle.None;
                cell.RightBorder.Style = RtfCellBorderStyle.None;
                cell.BottomBorder.Style = RtfCellBorderStyle.None;
                lastRightBound = cell.CellRightBound;
            }
            return lastRightBound;
        }

        private void DoMerge(List<RtfTableRow> rows, Dictionary<RtfTableRowCell, ClipboardBandCellInfo> merged)
        {
            int num = 0;
            while (num < rows.Count)
            {
                RtfTableRow row = rows[num];
                int num2 = 0;
                while (true)
                {
                    if (num2 >= row.cells.Count)
                    {
                        num++;
                        break;
                    }
                    RtfTableRowCell key = row[num2];
                    if (merged.ContainsKey(key))
                    {
                        ClipboardBandCellInfo info = merged[key];
                        bool flag2 = info.Height > 1;
                        if (info.Width > 1)
                        {
                            int num3 = num;
                            while (num3 < (num + info.Height))
                            {
                                rows[num3][num2].HMergeFirstCell = true;
                                int num4 = num2 + 1;
                                while (true)
                                {
                                    if (num4 >= (num2 + info.Width))
                                    {
                                        num3++;
                                        break;
                                    }
                                    rows[num3][num4].HMergeLastCell = true;
                                    num4++;
                                }
                            }
                        }
                        if (flag2)
                        {
                            key.VMergeFirstCell = true;
                            RtfTableRowCell cell2 = null;
                            int num5 = num + 1;
                            while (true)
                            {
                                if (num5 >= (num + info.Height))
                                {
                                    cell2.BottomBorder.Style = key.BottomBorder.Style;
                                    break;
                                }
                                cell2 = rows[num5][num2];
                                cell2.VMergeLastCell = true;
                                cell2.LeftBorder.Style = key.LeftBorder.Style;
                                cell2.RightBorder.Style = key.RightBorder.Style;
                                num5++;
                            }
                        }
                    }
                    num2++;
                }
            }
        }

        public void EndExport()
        {
        }

        private RtfCellBorderStyle GetBorderStyle(XlBorderLineStyle style)
        {
            RtfCellBorderStyle thin = RtfCellBorderStyle.Thin;
            Enum.TryParse<RtfCellBorderStyle>(style.ToString(), out thin);
            return thin;
        }

        public void SetDataObject(DataObject data)
        {
            data.SetText(this.document.Compile(), TextDataFormat.Rtf);
        }
    }
}

