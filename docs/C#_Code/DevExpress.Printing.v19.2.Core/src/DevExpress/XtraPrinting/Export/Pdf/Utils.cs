namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;
    using System.Globalization;

    public class Utils
    {
        public static string Format(double value) => 
            value.ToString("G", NumberFormatInfo.InvariantInfo);

        public static double Round(double value) => 
            Math.Round(value, 3);

        public static PdfRectangle ToPdfRectangle(RectangleF bounds) => 
            new PdfRectangle(bounds.X, bounds.Y, bounds.Right, bounds.Bottom);

        public static string ToString(double value) => 
            Format(Round(value));
    }
}

