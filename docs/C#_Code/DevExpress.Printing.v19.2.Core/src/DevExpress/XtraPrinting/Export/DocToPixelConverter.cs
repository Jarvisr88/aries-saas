namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class DocToPixelConverter : GraphicsUnitConverter
    {
        public static Point Convert(PointF val) => 
            Point.Round(Convert(val, (float) 300f, GraphicsDpi.Pixel));

        public static Rectangle Convert(RectangleF val) => 
            Round(Convert(val, (float) 300f, GraphicsDpi.Pixel));

        public static Size Convert(SizeF val) => 
            Size.Round(Convert(val, (float) 300f, GraphicsDpi.Pixel));

        public static int Convert(float val) => 
            System.Convert.ToInt32(Convert(val, (float) 300f, GraphicsDpi.Pixel));
    }
}

