namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.CharacterComb;
    using System;
    using System.Drawing;

    public class CharacterCombBrickExporter : TextBrickExporter
    {
        protected override void DrawBackground(IGraphics gr, RectangleF rect);
        protected override void DrawClientContent(IGraphics gr, RectangleF clientRect);
        protected override RectangleF GetClientRect(RectangleF rect, IGraphics gr);
        protected override RectangleF GetClipRect(RectangleF rect, IGraphics gr);
        private BrickViewData[] GetCommonExportData(ExportContext exportContext, RectangleF rect);
        protected internal override BrickViewData[] GetDocxData(ExportContext docxExportContext, RectangleF rect, RectangleF clipRect);
        protected internal override BrickViewData[] GetHtmlData(ExportContext htmlExportContext, RectangleF bounds, RectangleF clipBounds);
        private RectangleF GetHtmlTableBrickRect(RectangleF elementRect, CharacterCombInfo cellInfo, int rowIndex, int colIndex, int rowCount, int colCount);
        private BrickViewData[] GetHtmlTableData(ExportContext exportContext, RectangleF rect);
        private BrickStyle GetHtmlTableStyle(ExportContext exportContext, CharacterCombInfo cellInfo, int rowIndex, int colIndex, int rowCount, int colCount);
        protected internal override BrickViewData[] GetRtfData(ExportContext rtfExportContext, RectangleF rect, RectangleF clipRect);
        private BrickStyle GetStyle(ExportContext exportContext, CharacterCombInfo cellInfo, int rowIndex, int colIndex, int rowCount, int colCount);
        private RectangleF GetTableBrickRect(RectangleF elementRect, CharacterCombInfo cellInfo, int rowIndex, int colIndex, int rowCount, int colCount);
        private BrickViewData[] GetTableExportData(ExportContext exportContext, RectangleF rect);
        protected internal override BrickViewData[] GetTextData(ExportContext exportContext, RectangleF rect);
        protected internal override BrickViewData[] GetXlData(ExportContext xlsExportContext, RectangleF rect, RectangleF clipRect);
        private bool HasBorderWithoutOpposite(BorderSide borderSide);

        private DevExpress.XtraPrinting.CharacterCombBrick CharacterCombBrick { get; }
    }
}

