namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Drawing;

    public class CompositeBrickExporter : BrickExporter
    {
        public override void Draw(IGraphics gr, RectangleF rect, RectangleF parentRect);
        internal override void ProcessLayout(PageLayoutBuilder layoutBuilder, PointF pos, RectangleF clipRect);
    }
}

