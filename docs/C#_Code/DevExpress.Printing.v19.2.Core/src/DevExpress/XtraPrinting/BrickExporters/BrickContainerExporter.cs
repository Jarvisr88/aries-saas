namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Drawing;

    public class BrickContainerExporter : BrickContainerBaseExporter
    {
        public override void Draw(IGraphics gr, RectangleF rect, RectangleF parentRect);
        private static float GetOuterBorderWidth(BrickStyle style);
        private static RectangleF InflateRectByBorderWidth(RectangleF clipRect, RectangleF brickRect, BrickStyle style);
        internal override void ProcessLayout(PageLayoutBuilder layoutBuilder, PointF pos, RectangleF clipRect);

        private DevExpress.XtraPrinting.BrickContainer BrickContainer { get; }
    }
}

