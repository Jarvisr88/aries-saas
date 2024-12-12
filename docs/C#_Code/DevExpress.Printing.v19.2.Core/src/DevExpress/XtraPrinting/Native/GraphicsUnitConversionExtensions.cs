namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal static class GraphicsUnitConversionExtensions
    {
        public static RectangleF DocToDip(this RectangleF value);
        public static SizeF DocToDip(this SizeF value);
        public static float DocToDip(this float value);
        public static RectangleF DocToPixel(this RectangleF value);
        public static Rectangle HundredthsOfAnInchToDip(this Rectangle value);
        public static int HundredthsOfAnInchToDip(this int value);
        public static float ToDocFrom(this float value, GraphicsUnit sourceUnit);
    }
}

