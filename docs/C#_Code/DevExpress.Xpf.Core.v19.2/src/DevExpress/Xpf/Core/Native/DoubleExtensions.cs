namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class DoubleExtensions
    {
        public static bool AreClose(this double value1, double value2);
        public static bool AreClose(Point point1, Point point2);
        public static bool AreClose(Size size1, Size size2);
        public static bool GreaterThan(this double value1, double value2);
        public static bool GreaterThanOrClose(this double value1, double value2);
        public static bool InRange(this double value, double min, double max);
        public static bool IsNaN(this double value);
        public static bool IsZero(this double value);
        public static bool LessThan(this double value1, double value2);
        public static bool LessThanOrClose(this double value1, double value2);
        public static double Round(this double value, bool toLower = false);
        public static double ToRange(this double value, double min, double max);
    }
}

