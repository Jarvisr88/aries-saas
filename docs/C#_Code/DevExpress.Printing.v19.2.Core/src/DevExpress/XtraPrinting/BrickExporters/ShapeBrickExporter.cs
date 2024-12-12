namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class ShapeBrickExporter : VisualBrickExporter
    {
        protected override Image CreateTableCellImage(ITableExportProvider exportProvider, RectangleF rect);
        protected override void DrawClientContent(IGraphics gr, RectangleF clientRect);
        protected override Image DrawContentToImage(ExportContext exportContext, RectangleF rect);
        protected internal override void FillDocxTableCellInternal(IDocxExportProvider exportProvider);
        protected override void FillHtmlTableCellCore(IHtmlExportProvider exportProvider);
        protected override void FillRtfTableCellCore(IRtfExportProvider exportProvider);
        private void FillTableCellWithImage(ITableExportProvider exportProvider);
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);
        protected internal override BrickViewData[] GetExportData(ExportContext exportContext, RectangleF rect, RectangleF clipRect);

        private DevExpress.XtraPrinting.ShapeBrick ShapeBrick { get; }
    }
}

