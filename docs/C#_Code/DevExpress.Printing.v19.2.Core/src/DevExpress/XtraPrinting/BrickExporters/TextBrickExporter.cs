namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class TextBrickExporter : VisualBrickExporter
    {
        protected virtual ITableCell CreateClippedDataTableCell(string text);
        protected override void DrawClientContent(IGraphics gr, RectangleF clientRect);
        protected virtual void DrawText(IGraphics gr, RectangleF clientRectangle, StringFormat sf, Brush brush);
        protected internal override void FillDocxTableCellInternal(IDocxExportProvider exportProvider);
        protected override void FillHtmlTableCellCore(IHtmlExportProvider exportProvider);
        protected override void FillRtfTableCellCore(IRtfExportProvider exportProvider);
        protected internal override void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText);
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);
        private static RectangleF GetActualBrickRectangle(RectangleF pixRect, BrickStyle style);
        private BrickViewData[] GetClippedPageExportData(ExportContext exportContext, RectangleF rect, RectangleF clipRect);
        protected internal override BrickViewData[] GetDocxData(ExportContext docxExportContext, RectangleF rect, RectangleF clipRect);
        private TextExportMode GetRealTextExportMode(IXlExportProvider exportProvider);
        protected internal override BrickViewData[] GetRtfData(ExportContext rtfExportContext, RectangleF rect, RectangleF clipRect);
        private object GetTextValue(TextExportMode textExportMode);
        private static object GetValidValue(object value, string text);
        protected internal override BrickViewData[] GetXlData(ExportContext xlsExportContext, RectangleF rect, RectangleF clipRect);
        private bool IsRectEqualToClipRect(RectangleF rect, RectangleF clipRect);
        protected virtual bool IsValidBounds(RectangleF bounds);
        protected virtual void SetStringFormatTabStops(BrickStyle style, Measurer measurer);
        internal static TextExportMode ToTextExportMode(DefaultBoolean value, TextExportMode defaultTextExportMode);

        protected DevExpress.XtraPrinting.TextBrick TextBrick { get; }

        private object TextValue { get; }

        protected virtual bool IsClippedPageDataSupported { get; }
    }
}

