namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    internal static class GraphicsUnitConverter2
    {
        public static PointF DocToPixel(PointF value) => 
            GraphicsUnitConverter.Convert(value, (float) 300f, (float) 96f);

        public static RectangleF DocToPixel(RectangleF value) => 
            GraphicsUnitConverter.Convert(value, (float) 300f, (float) 96f);

        public static SizeF DocToPixel(SizeF value) => 
            GraphicsUnitConverter.Convert(value, (float) 300f, (float) 96f);

        public static float DocToPixel(float value) => 
            GraphicsUnitConverter.Convert(value, (float) 300f, (float) 96f);

        public static PointF PixelToDoc(PointF value) => 
            GraphicsUnitConverter.Convert(value, (float) 96f, (float) 300f);

        public static RectangleF PixelToDoc(RectangleF value) => 
            GraphicsUnitConverter.Convert(value, (float) 96f, (float) 300f);

        public static float PixelToDoc(float value) => 
            GraphicsUnitConverter.Convert(value, (float) 96f, (float) 300f);
    }
}

