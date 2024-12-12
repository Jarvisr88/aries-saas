namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class SizeHelper
    {
        private static Size infinite;
        private static Size zero;

        static SizeHelper();
        public static bool AreClose(this Size size, Size another);
        public static void Deflate(ref Size size, Thickness padding);
        public static void Inflate(ref Size size, Thickness padding);
        public static bool IsMeasureValid(double length);
        public static bool IsZero(this Size size);
        public static Size Parse(string s);
        public static Size ToInfinity(Size size, bool isInfinityWidth, bool isInfinityHeight);
        private static double ToMeasureValid(double length);
        public static Size ToMeasureValid(Size size);
        public static Size ToMeasureValid(Size size, bool updateWidth, bool updateHeight);
        public static Point ToPoint(this Size size);
        public static Rect ToRect(this Size size);
        public static void UpdateMaxSize(ref Size maxSize, Size size);
        public static void UpdateMaxSize(ref Size maxSize, Size size, bool updateWidth, bool updateHeight);
        public static void UpdateMinSize(ref Size minSize, Size size);
        public static void UpdateMinSize(ref Size minSize, Size size, bool updateWidth, bool updateHeight);

        public static Size Infinite { get; }

        public static Size Zero { get; }
    }
}

