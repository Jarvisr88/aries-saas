namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct DpiScale
    {
        private readonly double _dpiScaleX;
        private readonly double _dpiScaleY;
        public DpiScale(double dpiScaleX, double dpiScaleY);
        public double DpiScaleX { get; }
        public double DpiScaleY { get; }
        public double PixelsPerDip { get; }
    }
}

