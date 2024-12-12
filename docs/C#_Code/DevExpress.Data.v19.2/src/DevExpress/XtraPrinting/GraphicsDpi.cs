namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class GraphicsDpi
    {
        public const float Display = 75f;
        public const float Inch = 1f;
        public const float Document = 300f;
        public const float Millimeter = 25.4f;
        public const float Point = 72f;
        public const float HundredthsOfAnInch = 100f;
        public const float TenthsOfAMillimeter = 254f;
        public const float Twips = 1440f;
        public const float EMU = 914400f;
        public const float DeviceIndependentPixel = 96f;
        public static readonly float Pixel = 96f;

        static GraphicsDpi()
        {
            if (!PSNativeMethods.AspIsRunning)
            {
                using (Bitmap bitmap = new Bitmap(1, 1))
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        Pixel = graphics.DpiX;
                    }
                }
            }
        }

        public static GraphicsUnit DpiToUnit(float dpi)
        {
            if (dpi.Equals((float) 75f))
            {
                return GraphicsUnit.Display;
            }
            if (dpi.Equals((float) 1f))
            {
                return GraphicsUnit.Inch;
            }
            if (dpi.Equals((float) 300f))
            {
                return GraphicsUnit.Document;
            }
            if (dpi.Equals((float) 25.4f))
            {
                return GraphicsUnit.Millimeter;
            }
            if (dpi.Equals(Pixel))
            {
                return GraphicsUnit.Pixel;
            }
            if (!dpi.Equals((float) 72f))
            {
                throw new ArgumentException("dpi");
            }
            return GraphicsUnit.Point;
        }

        public static float GetGraphicsDpi(Graphics gr) => 
            (gr.PageUnit != GraphicsUnit.Display) ? UnitToDpi(gr.PageUnit) : gr.DpiX;

        public static float GetSafeResolution(float resolution) => 
            resolution;

        public static float UnitToDpi(GraphicsUnit unit)
        {
            switch (unit)
            {
                case GraphicsUnit.World:
                case GraphicsUnit.Pixel:
                    return Pixel;

                case GraphicsUnit.Display:
                    return 75f;

                case GraphicsUnit.Point:
                    return 72f;

                case GraphicsUnit.Inch:
                    return 1f;

                case GraphicsUnit.Document:
                    return 300f;

                case GraphicsUnit.Millimeter:
                    return 25.4f;
            }
            throw new ArgumentException("unit");
        }

        public static float UnitToDpiI(GraphicsUnit unit)
        {
            switch (unit)
            {
                case GraphicsUnit.World:
                case GraphicsUnit.Pixel:
                    return 96f;

                case GraphicsUnit.Display:
                    return 75f;

                case GraphicsUnit.Point:
                    return 72f;

                case GraphicsUnit.Inch:
                    return 1f;

                case GraphicsUnit.Document:
                    return 300f;

                case GraphicsUnit.Millimeter:
                    return 25.4f;
            }
            throw new ArgumentException("unit");
        }

        public static string UnitToString(GraphicsUnit unit)
        {
            switch (unit)
            {
                case GraphicsUnit.World:
                case GraphicsUnit.Display:
                case GraphicsUnit.Pixel:
                case GraphicsUnit.Point:
                case GraphicsUnit.Document:
                    return string.Empty;

                case GraphicsUnit.Inch:
                    return "in";

                case GraphicsUnit.Millimeter:
                    return "mm";
            }
            throw new ArgumentException("unit");
        }
    }
}

