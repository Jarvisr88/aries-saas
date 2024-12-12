namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.HtmlExport;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.Native;
    using System;

    public class HtmlTableBuilder : HtmlBuilderBase
    {
        private MegaTable megaTable;

        private void AddSpannedCells(HtmlBuilderBase.Table cellTable, DXHtmlTable table, int row, int column)
        {
            DXHtmlTableCell cell = table.Rows[row].Cells[column];
            int num = (cell.ColSpan > 0) ? cell.ColSpan : 1;
            int num2 = (cell.RowSpan > 0) ? cell.RowSpan : 1;
            int num3 = row;
            while (num3 < (row + num2))
            {
                int num4 = column;
                while (true)
                {
                    if (num4 >= (column + num))
                    {
                        num3++;
                        break;
                    }
                    if ((num3 != row) || (num4 != column))
                    {
                        cell = table.Rows[num3].Cells[num4];
                        if ((cell.ColSpan <= 1) && ((cell.RowSpan <= 1) && this.IsEmptyCell(cell)))
                        {
                            cellTable[num3].Add(cell);
                        }
                    }
                    num4++;
                }
            }
        }

        protected override DXHtmlTable Build(HtmlExportContext htmlExportContext)
        {
            DXHtmlTable table = base.Build(htmlExportContext);
            this.RemoveSpannedCells(table);
            this.InitFirstRowWidths(table);
            this.InitFirstColumnHeights(table);
            return table;
        }

        public override DXHtmlTable BuildTable(LayoutControlCollection layoutControls, bool correctImportBrickBounds, HtmlExportContext htmlExportContext)
        {
            MegaTable table = new MegaTable(layoutControls, true, correctImportBrickBounds, -1);
            if (table.IsEmpty)
            {
                return null;
            }
            this.megaTable = table;
            return this.Build(htmlExportContext);
        }

        protected override DXHtmlTable CreateHtmlTable()
        {
            DXHtmlTable table = new DXHtmlTable {
                Style = { 
                    ["border-width"] = "0px",
                    ["empty-cells"] = "show",
                    ["width"] = HtmlConvert.ToHtml(this.megaTable.Width),
                    ["height"] = HtmlConvert.ToHtml(this.megaTable.Height)
                }
            };
            table.Style.Add(DXHtmlTextWriterStyle.Position, "relative");
            table.CellPadding = 0;
            table.CellSpacing = 0;
            table.Border = 0;
            int num = 0;
            while ((num < this.megaTable.RowCount) && !base.fHtmlExportContext.CancelPending)
            {
                DXHtmlTableRow row = this.CreateHtmlTableRow();
                table.Rows.Add(row);
                int num2 = 0;
                while (true)
                {
                    if (num2 >= this.megaTable.ColumnCount)
                    {
                        num++;
                        break;
                    }
                    table.Rows[num].Cells.Add(new DXHtmlTableCell());
                    num2++;
                }
            }
            return table;
        }

        private DXHtmlTableRow CreateHtmlTableRow()
        {
            DXHtmlTableRow row = new DXHtmlTableRow();
            row.Style.Add(DXHtmlTextWriterStyle.VerticalAlign, "top");
            return row;
        }

        private void InitFirstColumnHeights(DXHtmlTable table)
        {
            if (this.megaTable.ZeroColumnNeeded)
            {
                this.InsertFirstZeroColumn(table);
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                HtmlHelper.SetStyleHeight(table.Rows[i].Cells[0].Style, this.megaTable.RowHeights[i]);
            }
        }

        private void InitFirstRowWidths(DXHtmlTable table)
        {
            if (this.megaTable.ZeroRowNeeded)
            {
                this.InsertFirstZeroRow(table);
            }
            DXHtmlTableRow row = table.Rows[0];
            for (int i = 0; i < row.Cells.Count; i++)
            {
                HtmlHelper.SetStyleWidth(row.Cells[i].Style, this.megaTable.ColWidths[i]);
            }
        }

        private void InsertFirstZeroColumn(DXHtmlTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DXHtmlTableCell cell = new DXHtmlTableCell();
                HtmlHelper.SetStyleWidth(cell.Style, 0);
                table.Rows[i].Cells.Insert(0, cell);
            }
            this.megaTable.ColWidths.Insert(0, 0);
        }

        private void InsertFirstZeroRow(DXHtmlTable table)
        {
            DXHtmlTableRow row = new DXHtmlTableRow();
            for (int i = 0; i < this.megaTable.ColumnCount; i++)
            {
                DXHtmlTableCell cell = new DXHtmlTableCell();
                HtmlHelper.SetStyleHeight(cell.Style, 0);
                row.Cells.Add(cell);
            }
            table.Rows.Insert(0, row);
            this.megaTable.RowHeights.Insert(0, 0);
        }

        private bool IsEmptyCell(DXHtmlTableCell cell) => 
            (cell.Controls.Count != 0) ? ((cell.Controls.Count == 1) && ((cell.Controls[0] is DXHtmlLiteralControl) && (((DXHtmlLiteralControl) cell.Controls[0]).Text == base.emptyCellContent))) : true;

        private void RemoveSpannedCells(DXHtmlTable table)
        {
            HtmlBuilderBase.Table cellTable = new HtmlBuilderBase.Table(table.Rows.Count);
            int row = 0;
            while (row < table.Rows.Count)
            {
                int column = 0;
                while (true)
                {
                    if (column >= table.Rows[row].Cells.Count)
                    {
                        row++;
                        break;
                    }
                    if ((table.Rows[row].Cells[column].ColSpan > 1) || (table.Rows[row].Cells[column].RowSpan > 1))
                    {
                        this.AddSpannedCells(cellTable, table, row, column);
                    }
                    column++;
                }
            }
            for (int i = 0; i < cellTable.RowCount; i++)
            {
                foreach (DXHtmlTableCell cell in cellTable[i])
                {
                    table.Rows[i].Cells.Remove(cell);
                }
            }
        }

        protected override void SetupSpans(DXHtmlTable table)
        {
            ObjectInfo[] objects = this.megaTable.Objects;
            int index = 0;
            while (true)
            {
                if (index < objects.Length)
                {
                    ObjectInfo info = objects[index];
                    if (!base.fHtmlExportContext.CancelPending)
                    {
                        BrickViewData data = (BrickViewData) info.Object;
                        DXHtmlTableCell control = table.Rows[info.RowIndex].Cells[info.ColIndex];
                        base.FillCellContent(data, control);
                        if (control.Controls.Count == 0)
                        {
                            control.Controls.Add(base.CreateEmptyCellControl());
                        }
                        if (info.ColSpan > 1)
                        {
                            control.ColSpan = info.ColSpan;
                        }
                        if (info.RowSpan > 1)
                        {
                            control.RowSpan = info.RowSpan;
                        }
                        if (base.fHtmlExportContext.MainExportMode == HtmlExportMode.SingleFile)
                        {
                            ProgressReflector progressReflector = base.fHtmlExportContext.ProgressReflector;
                            progressReflector.RangeValue++;
                        }
                        index++;
                        continue;
                    }
                }
                return;
            }
        }

        protected override int CountObjects =>
            this.megaTable.Objects.Length;
    }
}

