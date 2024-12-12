namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.HtmlExport;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class HtmlDivBuilder : HtmlBuilderBase
    {
        private LayoutControlCollection layoutControls;

        public override DXHtmlTable BuildTable(LayoutControlCollection layoutControls, bool correctImportBrickBounds, HtmlExportContext htmlExportContext)
        {
            this.layoutControls = layoutControls;
            return this.Build(htmlExportContext);
        }

        protected override DXHtmlTable CreateHtmlTable()
        {
            DXHtmlTable table = new DXHtmlTable {
                Style = { 
                    ["border-width"] = "0px",
                    ["empty-cells"] = "show"
                }
            };
            table.Style.Add(DXHtmlTextWriterStyle.Position, "relative");
            table.CellPadding = 0;
            table.CellSpacing = 0;
            table.Border = 0;
            table.Rows.Add(new DXHtmlTableRow());
            table.Rows[0].Cells.Add(new DXHtmlTableCell());
            table.Rows[0].Cells[0].Style.Add(DXHtmlTextWriterStyle.Position, "absolute");
            return table;
        }

        protected override void SetupSpans(DXHtmlTable table)
        {
            DXHtmlTableCell cell = table.Rows[0].Cells[0];
            Rectangle empty = Rectangle.Empty;
            foreach (ILayoutControl control in this.layoutControls)
            {
                if (base.fHtmlExportContext.CancelPending)
                {
                    break;
                }
                empty = Rectangle.Union(empty, control.Bounds);
                BrickViewData data = (BrickViewData) control;
                DXHtmlDivision division = new DXHtmlDivision();
                division.Style.Add(DXHtmlTextWriterStyle.MarginTop, HtmlConvert.ToHtml(data.Bounds.Top));
                division.Style.Add(base.fHtmlExportContext.RightToLeftLayout ? DXHtmlTextWriterStyle.MarginRight : DXHtmlTextWriterStyle.MarginLeft, HtmlConvert.ToHtml(data.Bounds.Left));
                division.Style.Add(DXHtmlTextWriterStyle.Position, "absolute");
                base.FillCellContent(data, division);
                cell.Controls.Add(division);
                if (base.fHtmlExportContext.MainExportMode == HtmlExportMode.SingleFile)
                {
                    ProgressReflector progressReflector = base.fHtmlExportContext.ProgressReflector;
                    progressReflector.RangeValue++;
                }
            }
            if (empty.X < 0)
            {
                cell.Style["padding-left"] = HtmlConvert.ToHtml(-empty.X);
            }
            if (empty.Y < 0)
            {
                cell.Style["padding-top"] = HtmlConvert.ToHtml(-empty.Y);
            }
            table.Style["width"] = HtmlConvert.ToHtml(empty.Width);
            table.Style["height"] = HtmlConvert.ToHtml(empty.Height);
        }

        protected override int CountObjects =>
            this.layoutControls.Count;
    }
}

