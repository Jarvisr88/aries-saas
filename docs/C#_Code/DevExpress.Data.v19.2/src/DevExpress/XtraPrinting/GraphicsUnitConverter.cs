namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;

    public class GraphicsUnitConverter
    {
        private static int Conv(int val, double scale) => 
            System.Convert.ToInt32((double) (val * scale));

        private static double ConvD(double val, double scale) => 
            val * scale;

        public static MarginsFloat Convert(MarginsFloat val, float fromDpi, float toDpi)
        {
            if (fromDpi == toDpi)
            {
                return val;
            }
            double scale = GetScale(fromDpi, toDpi);
            return new MarginsFloat { 
                Left = ConvF(val.Left, scale),
                Right = ConvF(val.Right, scale),
                Top = ConvF(val.Top, scale),
                Bottom = ConvF(val.Bottom, scale)
            };
        }

        public static MarginsF Convert(MarginsF val, GraphicsUnit fromUnit, GraphicsUnit toUnit) => 
            Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));

        public static MarginsF Convert(MarginsF val, float fromDpi, float toDpi)
        {
            if (fromDpi == toDpi)
            {
                return val;
            }
            double scale = GetScale(fromDpi, toDpi);
            return new MarginsF { 
                Left = ConvF(val.Left, scale),
                Right = ConvF(val.Right, scale),
                Top = ConvF(val.Top, scale),
                Bottom = ConvF(val.Bottom, scale)
            };
        }

        public static RectangleDF Convert(RectangleDF val, GraphicsUnit fromUnit, GraphicsUnit toUnit) => 
            Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));

        public static RectangleDF Convert(RectangleDF val, float fromDpi, float toDpi)
        {
            if (fromDpi == toDpi)
            {
                return val;
            }
            double scale = GetScale(fromDpi, toDpi);
            return RectangleDF.FromLTRB(ConvD(val.Left, scale), ConvD(val.Top, scale), ConvD(val.Right, scale), ConvD(val.Bottom, scale));
        }

        public static Point Convert(Point val, GraphicsUnit fromUnit, GraphicsUnit toUnit) => 
            Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));

        public static Point Convert(Point val, float fromDpi, float toDpi)
        {
            if (fromDpi == toDpi)
            {
                return val;
            }
            double scale = GetScale(fromDpi, toDpi);
            return new Point(Conv(val.X, scale), Conv(val.Y, scale));
        }

        public static PointF Convert(PointF val, GraphicsUnit fromUnit, GraphicsUnit toUnit) => 
            Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));

        public static PointF Convert(PointF val, float fromDpi, float toDpi)
        {
            if (fromDpi == toDpi)
            {
                return val;
            }
            double scale = GetScale(fromDpi, toDpi);
            return new PointF(ConvF(val.X, scale), ConvF(val.Y, scale));
        }

        public static Margins Convert(Margins val, float fromDpi, float toDpi)
        {
            if (fromDpi == toDpi)
            {
                return val;
            }
            double scale = GetScale(fromDpi, toDpi);
            return new Margins(Conv(val.Left, scale), Conv(val.Right, scale), Conv(val.Top, scale), Conv(val.Bottom, scale));
        }

        public static Rectangle Convert(Rectangle val, GraphicsUnit fromUnit, GraphicsUnit toUnit) => 
            Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));

        public static Rectangle Convert(Rectangle val, float fromDpi, float toDpi) => 
            (fromDpi != toDpi) ? Round(Convert((RectangleF) val, fromDpi, toDpi)) : val;

        public static RectangleF Convert(RectangleF val, GraphicsUnit fromUnit, GraphicsUnit toUnit) => 
            Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));

        public static RectangleF Convert(RectangleF val, float fromDpi, float toDpi)
        {
            if (fromDpi == toDpi)
            {
                return val;
            }
            double scale = GetScale(fromDpi, toDpi);
            return RectangleF.FromLTRB(ConvF(val.Left, scale), ConvF(val.Top, scale), ConvF(val.Right, scale), ConvF(val.Bottom, scale));
        }

        public static Size Convert(Size val, GraphicsUnit fromUnit, GraphicsUnit toUnit) => 
            Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));

        public static Size Convert(Size val, float fromDpi, float toDpi)
        {
            if (fromDpi == toDpi)
            {
                return val;
            }
            double scale = GetScale(fromDpi, toDpi);
            return new Size(Conv(val.Width, scale), Conv(val.Height, scale));
        }

        public static SizeF Convert(SizeF val, GraphicsUnit fromUnit, GraphicsUnit toUnit) => 
            Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));

        public static SizeF Convert(SizeF val, float fromDpi, float toDpi)
        {
            if (fromDpi == toDpi)
            {
                return val;
            }
            double scale = GetScale(fromDpi, toDpi);
            return new SizeF(ConvF(val.Width, scale), ConvF(val.Height, scale));
        }

        public static int Convert(int val, float fromDpi, float toDpi)
        {
            if (fromDpi == toDpi)
            {
                return val;
            }
            double scale = GetScale(fromDpi, toDpi);
            return Conv(val, scale);
        }

        public static float Convert(float val, GraphicsUnit fromUnit, GraphicsUnit toUnit) => 
            Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));

        public static float Convert(float val, float fromDpi, float toDpi)
        {
            if (fromDpi == toDpi)
            {
                return val;
            }
            double scale = GetScale(fromDpi, toDpi);
            return ConvF(val, scale);
        }

        private static float ConvF(float val, double scale) => 
            ((float) (val * scale)) + 0f;

        public static RectangleF DipToDoc(RectangleF rect) => 
            Convert(rect, (float) 96f, (float) 300f);

        public static SizeF DipToDoc(SizeF size) => 
            Convert(size, (float) 96f, (float) 300f);

        public static float DipToDoc(float val) => 
            Convert(val, (float) 96f, (float) 300f);

        public static RectangleF DocToDip(RectangleF val) => 
            Convert(val, (float) 300f, (float) 96f);

        public static float DocToDip(float val) => 
            Convert(val, (float) 300f, (float) 96f);

        public static MarginsF DocToPixel(MarginsF val) => 
            Convert(val, (float) 300f, GraphicsDpi.Pixel);

        public static PointF DocToPixel(PointF val) => 
            Convert(val, (float) 300f, GraphicsDpi.Pixel);

        public static RectangleF DocToPixel(RectangleF val) => 
            Convert(val, (float) 300f, GraphicsDpi.Pixel);

        public static SizeF DocToPixel(SizeF val) => 
            Convert(val, (float) 300f, GraphicsDpi.Pixel);

        public static float DocToPixel(float val) => 
            Convert(val, (float) 300f, GraphicsDpi.Pixel);

        private static double GetScale(float fromDpi, float toDpi) => 
            ((double) toDpi) / ((double) fromDpi);

        public static PointF PixelToDoc(PointF val) => 
            Convert(val, GraphicsDpi.Pixel, (float) 300f);

        public static RectangleF PixelToDoc(RectangleF val) => 
            Convert(val, GraphicsDpi.Pixel, (float) 300f);

        public static SizeF PixelToDoc(SizeF val) => 
            Convert(val, GraphicsDpi.Pixel, (float) 300f);

        public static float PixelToDoc(float val) => 
            Convert(val, GraphicsDpi.Pixel, (float) 300f);

        public static Point Round(PointF point) => 
            new Point(System.Convert.ToInt32(point.X), System.Convert.ToInt32(point.Y));

        public static Rectangle Round(RectangleF rect)
        {
            Point location = new Point(System.Convert.ToInt32(rect.Left), System.Convert.ToInt32(rect.Top));
            return new Rectangle(location, new Size(System.Convert.ToInt32(rect.Right) - location.X, System.Convert.ToInt32(rect.Bottom) - location.Y));
        }
    }
}

