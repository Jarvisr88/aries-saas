namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class XETextPanelBrickExporter : PanelBrickExporter
    {
        protected internal override void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText)
        {
            TextBrick brick = this.FindTextBrick();
            if (brick != null)
            {
                ((BrickExporter) GetExporter(exportProvider.ExportContext, brick)).FillTextTableCell(exportProvider, shouldSplitText);
            }
            else
            {
                exportProvider.SetCellText(this.Brick.Value);
            }
        }

        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider)
        {
            TextBrick brick = this.FindTextBrick();
            exportProvider.SetCellText((brick != null) ? brick.Text : this.Brick.Value);
        }

        private TextBrick FindTextBrick()
        {
            TextBrick brick3;
            using (IEnumerator<DevExpress.XtraPrinting.Brick> enumerator = this.Brick.Bricks.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DevExpress.XtraPrinting.Brick current = enumerator.Current;
                        TextBrick brick2 = current as TextBrick;
                        if (brick2 == null)
                        {
                            continue;
                        }
                        brick3 = brick2;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return brick3;
        }

        protected internal override BrickViewData[] GetExportData(ExportContext exportContext, RectangleF rect, RectangleF clipRect) => 
            base.VisualBrick.IsVisible ? (((exportContext is XlExportContext) || (exportContext is TextExportContext)) ? exportContext.CreateBrickViewDataArray(base.Style, rect, base.TableCell) : base.GetExportData(exportContext, rect, clipRect)) : new BrickViewData[0];

        internal PanelBrick Brick =>
            base.Brick as PanelBrick;
    }
}

