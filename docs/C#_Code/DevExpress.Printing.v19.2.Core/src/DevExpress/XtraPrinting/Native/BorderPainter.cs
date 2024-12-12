namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public static class BorderPainter
    {
        private static void DrawBorderDashDot(IGraphicsBase gr, RectangleF rect, Color color, BorderSide sides, float borderWidth, BorderDashStyle dashStyle);
        public static void DrawBorders(IGraphicsBase gr, float grDpi, RectangleF rect, Brush brush, BorderSide sides, BrickStyle style);
        internal static void DrawDashStyleBorders(IGraphicsBase gr, float grDpi, BorderDashStyle dashStyle, RectangleF rect, Brush brush, BorderSide sides, float borderWidth);
        private static void DrawDoubleBorder(IGraphicsBase gr, float grDpi, RectangleF rect, Brush brush, BorderSide sides, float borderWidth);
        private static void DrawSolidBorder(IGraphicsBase gr, float grDpi, RectangleF rect, Brush brush, BorderSide sides, float borderWidth);
        private static void DrawSolidBorderCore(IGraphicsBase gr, RectangleF rect, Brush brush, BorderSide sides, float borderWidth);
    }
}

