namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public static class CompositeBrickExporterHelper
    {
        private static BrickIterator CreateBrickIterator(IGraphics gr, RectangleF rect, BrickBase brick);
        internal static void Draw(IGraphics gr, RectangleF rect, RectangleF parentRect, BrickBase brickBase);
        private static void DrawInnerBricks(IGraphics gr, RectangleF rect, BrickIterator bricks);
        internal static void ProcessLayoutCore(BrickBase brickBase, PageLayoutBuilder layoutBuilder, RectangleF rect, RectangleF clipRect);
    }
}

