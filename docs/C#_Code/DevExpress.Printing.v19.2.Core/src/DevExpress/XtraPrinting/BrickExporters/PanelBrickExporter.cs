namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.NativeBricks;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class PanelBrickExporter : VisualBrickExporter
    {
        protected override void DrawClientContent(IGraphics gr, RectangleF clientRect);
        protected virtual void DrawInnerBrick(IGraphics gr, RectangleF clientRect, Brick brick, RectangleF brickRect);
        protected internal override void FillHtmlTableCellInternal(IHtmlExportProvider exportProvider);
        protected internal override void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText);
        protected virtual List<Rectangle> GenerateEmptyAreas(RectangleDivider divider, bool allowEmptyAreas);
        private List<Rectangle> GetAdditionalTableInnerAreas(ExportContext exportContext, TableBrick tableBrick, RectangleF rect);
        protected override RectangleF GetClientRect(RectangleF rect, IGraphics gr);
        protected internal override BrickViewData[] GetExportData(ExportContext exportContext, RectangleF rect, RectangleF clipRect);
        protected virtual Rectangle[] GetInnerAreas(BrickViewData[] innerData, ExportContext exportContext, Brick brick, RectangleF rect);
        protected virtual BrickViewData GetOuterPanel(ExportContext exportContext, RectangleF boundsF);
        private void GetPageFormattedTextViewData(ExportContext exportContext, Rectangle bounds, List<BrickViewData> dataContainer);
        private void GetUsualViewData(ExportContext exportContext, Rectangle bounds, RectangleDivider divider, List<BrickViewData> dataContainer);
        protected BrickViewData[] GetViewData(RectangleF boundsF, RectangleF clipBoundsF, ExportContext exportContext);
        protected virtual RectangleF GetViewDataBounds(ExportContext exportContext, Brick brick);
        private static bool HasBrickWithTheSameBounds(List<BrickViewData> dataContainer, Rectangle bounds);
        private bool IsFormattedTextFrameExport(ExportContext exportContext);
        private void PatchTransparentBackColor(BrickViewData[] exportData, ExportContext exportContext);
        private void ProcessViewData(RectangleF boundsF, RectangleF clipBoundsF, ExportContext exportContext, Action<BrickViewData[], Rectangle[]> action);
        protected virtual void ValidateInnerData(BrickViewData[] innerData, ExportContext exportContext);

        private DevExpress.XtraPrinting.PanelBrick PanelBrick { get; }
    }
}

