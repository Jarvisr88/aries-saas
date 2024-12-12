namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class PSUnitConverter : GraphicsUnitConverter
    {
        public static PointF DocToPixel(PointF val, float zoom) => 
            MathMethods.Scale(DocToPixel(val), zoom);

        public static RectangleF DocToPixel(RectangleF val, float zoom) => 
            MathMethods.Scale(DocToPixel(val), zoom);

        public static SizeF DocToPixel(SizeF val, float zoom) => 
            MathMethods.Scale(DocToPixel(val), zoom);

        public static float DocToPixel(float val, float zoom) => 
            DocToPixel(val) * zoom;

        public static unsafe PointF DocToPixel(PointF val, float zoom, PointF scrollPos)
        {
            PointF* tfPtr1 = &val;
            tfPtr1.X -= scrollPos.X;
            PointF* tfPtr2 = &val;
            tfPtr2.Y -= scrollPos.Y;
            return DocToPixel(val, zoom);
        }

        public static RectangleF DocToPixel(RectangleF val, float zoom, PointF scrollPos)
        {
            val.Offset(-scrollPos.X, -scrollPos.Y);
            return DocToPixel(val, zoom);
        }

        public static PointF PixelToDoc(PointF val, float zoom) => 
            MathMethods.Scale(PixelToDoc(val), (float) (1f / zoom));

        public static RectangleF PixelToDoc(RectangleF val, float zoom) => 
            MathMethods.Scale(PixelToDoc(val), (float) (1f / zoom));

        public static SizeF PixelToDoc(SizeF val, float zoom) => 
            MathMethods.Scale(PixelToDoc(val), (float) (1f / zoom));

        public static float PixelToDoc(float val, float zoom) => 
            PixelToDoc(val) / zoom;

        public static PointF PixelToDoc(PointF val, float zoom, PointF scrollPos) => 
            PSNativeMethods.TranslatePointF(PixelToDoc(val, zoom), scrollPos);

        public static RectangleF PixelToDoc(RectangleF val, float zoom, PointF scrollPos)
        {
            RectangleF ef = PixelToDoc(val, zoom);
            ef.Offset(scrollPos.X, scrollPos.Y);
            return ef;
        }
    }
}

