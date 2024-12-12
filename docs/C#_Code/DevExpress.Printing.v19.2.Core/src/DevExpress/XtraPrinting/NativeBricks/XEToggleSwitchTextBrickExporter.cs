namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    internal class XEToggleSwitchTextBrickExporter : ToggleSwitchTextBrickExporter
    {
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider)
        {
            exportProvider.SetCellText(this.ToggleSwitchTextBrick.CheckText);
        }

        protected internal override BrickViewData[] GetExportData(ExportContext exportContext, RectangleF rect, RectangleF clipRect) => 
            !(exportContext is XlExportContext) ? base.GetExportData(exportContext, rect, clipRect) : exportContext.CreateBrickViewDataArray(base.Style, rect, base.TableCell);

        internal DevExpress.XtraPrinting.NativeBricks.ToggleSwitchTextBrick ToggleSwitchTextBrick =>
            base.Brick as DevExpress.XtraPrinting.NativeBricks.ToggleSwitchTextBrick;
    }
}

