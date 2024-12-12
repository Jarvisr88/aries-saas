namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.NativeBricks;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class TableBrickExporter : PanelBrickExporter
    {
        protected override void DrawClientContent(IGraphics gr, RectangleF clientRect);
        protected override List<Rectangle> GenerateEmptyAreas(RectangleDivider divider, bool allowEmptyAreas);
        private List<Rectangle> GetAdditionInnerAreas(RowBrick rowBrick, RectangleF rowRect);
        protected internal override BrickViewData[] GetExportData(ExportContext exportContext, RectangleF rect, RectangleF clipRect);
        protected override Rectangle[] GetInnerAreas(BrickViewData[] innerData, ExportContext exportContext, Brick brick, RectangleF rect);
        protected override BrickViewData GetOuterPanel(ExportContext exportContext, RectangleF boundsF);
    }
}

