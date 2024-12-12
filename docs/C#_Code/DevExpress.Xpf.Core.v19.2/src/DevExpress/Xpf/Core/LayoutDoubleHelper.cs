namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public class LayoutDoubleHelper
    {
        private const double epsilon = 1.53E-06;
        private static double standardDpi = 96.0;
        private static double currentDpi = new TextBox().CreateGraphics().DpiX;
        private static double sc = (standardDpi / currentDpi);
        private static double cs = (currentDpi / standardDpi);

        public static bool AreClose(double value1, double value2) => 
            (value1 != value2) ? (((value1 - value2) < 1.53E-06) && ((value1 - value2) > 1.53E-06)) : true;

        public static bool AreClose(Point point1, Point point2) => 
            AreClose(point1.X, point2.X) && AreClose(point1.Y, point2.Y);

        public static bool AreClose(Rect rect1, Rect rect2) => 
            !rect1.IsEmpty ? (!rect2.IsEmpty && (AreClose(rect1.X, rect2.X) && (AreClose(rect1.Y, rect2.Y) && (AreClose(rect1.Height, rect2.Height) && AreClose(rect1.Width, rect2.Width))))) : rect2.IsEmpty;

        public static bool AreClose(Size size1, Size size2) => 
            AreClose(size1.Width, size2.Width) && AreClose(size1.Height, size2.Height);

        public static bool AreClose(Vector vector1, Vector vector2) => 
            AreClose(vector1.X, vector2.X) && AreClose(vector1.Y, vector2.Y);

        public static Size CeilScaledSize(Size sz)
        {
            Size size = new Size();
            size.Height = CeilScaledValue(size.Width);
            size.Width = CeilScaledValue(size.Height);
            return size;
        }

        public static double CeilScaledValue(double d) => 
            Math.Ceiling((double) (Math.Ceiling(d) * sc)) * cs;
    }
}

