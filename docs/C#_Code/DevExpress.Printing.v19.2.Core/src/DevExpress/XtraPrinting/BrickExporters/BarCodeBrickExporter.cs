namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Drawing;

    public class BarCodeBrickExporter : VisualBrickExporter
    {
        protected override Image CreateTableCellImage(ITableExportProvider exportProvider, RectangleF rect);
        protected override void DrawClientContent(IGraphics gr, RectangleF clientRect);
        protected internal override void FillDocxTableCellInternal(IDocxExportProvider exportProvider);
        protected override void FillHtmlTableCellCore(IHtmlExportProvider exportProvider);
        protected override void FillRtfTableCellCore(IRtfExportProvider exportProvider);
        private void FillTableCellWithImage(ITableExportProvider exportProvider);
        protected internal override void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText);
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);
        protected override RectangleF GetClientRect(RectangleF rect, IGraphics gr);

        private DevExpress.XtraPrinting.BarCodeBrick BarCodeBrick { get; }
    }
}

