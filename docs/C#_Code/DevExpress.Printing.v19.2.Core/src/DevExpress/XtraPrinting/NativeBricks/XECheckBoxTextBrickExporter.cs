namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    internal class XECheckBoxTextBrickExporter : CheckBoxTextBrickExporter
    {
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider)
        {
            exportProvider.SetCellText(this.CheckBoxBrick.CheckText);
        }

        protected internal override BrickViewData[] GetExportData(ExportContext exportContext, RectangleF rect, RectangleF clipRect) => 
            !(exportContext is XlExportContext) ? base.GetExportData(exportContext, rect, clipRect) : exportContext.CreateBrickViewDataArray(base.Style, rect, base.TableCell);

        internal CheckBoxTextBrick CheckBoxBrick =>
            base.Brick as CheckBoxTextBrick;
    }
}

