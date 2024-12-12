namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class TrackBarBrickExporter : VisualBrickExporter
    {
        protected override void DrawObject(IGraphics gr, RectangleF rect);
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);
        protected internal override BrickViewData[] GetExportData(ExportContext exportContext, RectangleF rect, RectangleF clipRect);
        private BrickViewData[] GetExportDataCore(ExportContext exportContext, RectangleF rect);
        protected internal override BrickViewData[] GetHtmlData(ExportContext htmlExportContext, RectangleF rect, RectangleF clipRect);
        private BrickViewData[] GetHtmlExportDataCore(ExportContext exportContext, RectangleF rect);
        private object GetTextValueCore(TextExportMode textExportMode);
        protected internal override BrickViewData[] GetXlData(ExportContext xlsExportContext, RectangleF rect, RectangleF clipRect);

        private DevExpress.XtraPrinting.TrackBarBrick TrackBarBrick { get; }
    }
}

