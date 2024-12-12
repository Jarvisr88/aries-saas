namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class RowBrickExporter : PanelBrickExporter
    {
        protected override void DrawInnerBrick(IGraphics gr, RectangleF clientRect, Brick brick, RectangleF brickRect);
        protected override Rectangle[] GetInnerAreas(BrickViewData[] innerData, ExportContext exportContext, Brick brick, RectangleF rect);
        protected override BrickViewData GetOuterPanel(ExportContext exportContext, RectangleF boundsF);
    }
}

