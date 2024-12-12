namespace DevExpress.Data.Svg
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SvgSize
    {
        public static readonly SvgSize Empty;
        public static readonly SvgSize Invalid;
        private double width;
        private double height;
        public bool IsEmpty { get; }
        public bool IsValid { get; }
        public double Width { get; set; }
        public double Height { get; set; }
        public SvgSize(double width, double height);
        static SvgSize();
    }
}

