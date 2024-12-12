namespace DevExpress.Data.Svg
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SvgRect
    {
        public static readonly SvgRect Empty;
        private double x1;
        private double y1;
        private double x2;
        private double y2;
        public double X1 { get; }
        public double X2 { get; }
        public double Y1 { get; }
        public double Y2 { get; }
        public double Width { get; }
        public double Height { get; }
        public bool IsEmpty { get; }
        public SvgRect(double x1, double y1, double x2, double y2);
        public static SvgRect Union(SvgRect bounds, double x, double y);
        public static SvgRect Union(SvgRect rect1, SvgRect rect2);
        public static SvgRect FromString(string content);
        static SvgRect();
    }
}

