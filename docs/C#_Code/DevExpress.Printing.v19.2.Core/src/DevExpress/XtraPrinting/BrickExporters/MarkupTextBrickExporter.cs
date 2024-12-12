namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.MarkupText;
    using System;
    using System.Drawing;

    public class MarkupTextBrickExporter : TextBrickExporter
    {
        protected override void DrawText(IGraphics gr, RectangleF clientRectangle, StringFormat sf, Brush brush);
        protected internal override void FillDocxTableCellInternal(IDocxExportProvider exportProvider);
        protected override void FillHtmlTableCellCore(IHtmlExportProvider exportProvider);
        protected override void FillRtfTableCellCore(IRtfExportProvider exportProvider);
        private void FillTableCellWithImage(ITableExportProvider exportProvider);
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);
        protected override RectangleF GetClientRect(RectangleF rect, IGraphics gr);
        protected override RectangleF GetClipRect(RectangleF rect, IGraphics gr);
        protected internal override BrickViewData[] GetTextData(ExportContext exportContext, RectangleF rect);

        private DevExpress.XtraPrinting.MarkupTextBrick MarkupTextBrick { get; }

        private PrintingStringInfo StringInfo { get; }

        protected override bool IsClippedPageDataSupported { get; }
    }
}

