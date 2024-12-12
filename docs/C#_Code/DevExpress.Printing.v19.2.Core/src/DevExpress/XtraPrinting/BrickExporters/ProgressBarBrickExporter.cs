namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class ProgressBarBrickExporter : VisualBrickExporter
    {
        protected override void DrawBackground(IGraphics gr, RectangleF rect);
        protected override void DrawClientContent(IGraphics gr, RectangleF clientRect);
        private void DrawText(IGraphics gr, RectangleF clientRectangle, StringFormat sf, Brush brush);
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);
        protected internal override BrickViewData[] GetExportData(ExportContext exportContext, RectangleF rect, RectangleF clipRect);
        private BrickViewData[] GetExportDataCore(ExportContext exportContext, RectangleF rect);
        private object GetTextValueCore(TextExportMode textExportMode);
        protected internal override BrickViewData[] GetXlData(ExportContext xlExportContext, RectangleF rect, RectangleF clipRect);

        private DevExpress.XtraPrinting.ProgressBarBrick ProgressBarBrick { get; }
    }
}

