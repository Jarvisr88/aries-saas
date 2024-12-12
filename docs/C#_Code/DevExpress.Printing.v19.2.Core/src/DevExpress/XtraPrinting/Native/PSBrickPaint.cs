namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class PSBrickPaint
    {
        public static void DrawWarningRect(IGraphics gr, RectangleF r);
        public static void DrawWarningRect(IGraphics gr, RectangleF r, bool drawBorders);
        public static Image GetBrickImage(IBrick brick, RectangleF rect, Color backColor, PrintingSystemBase ps);
    }
}

