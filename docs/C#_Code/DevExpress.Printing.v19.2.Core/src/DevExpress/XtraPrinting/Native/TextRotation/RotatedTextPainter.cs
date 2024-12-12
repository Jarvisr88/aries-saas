namespace DevExpress.XtraPrinting.Native.TextRotation
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class RotatedTextPainter
    {
        public static void DrawRotatedString(IGraphics g, string text, Font font, Brush br, RectangleF bounds, StringFormat sf, float angle, float width, TextAlignment textAlignment);
        private static void DrawRotatedString(IGraphics g, string text, Font font, Brush brush, PointF rotationPoint, SizeF textSize, StringFormat sf, float angle, DevExpress.XtraPrinting.Native.TextRotation.TextRotation textRotation, TextAlignment textAlignment);
        private static PointF GetOffset(RectangleF rect, DevExpress.XtraPrinting.Native.TextRotation.TextRotation textRotation);
        private static PointF GetTextRotationPoint(RectangleF clientRectangle, RectangleF frameBounds, TextAlignment alignment);
        private static Size GetTextSize(IGraphics g, string text, Font font, StringFormat sf, float width);
    }
}

