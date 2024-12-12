namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    [StructLayout(LayoutKind.Sequential)]
    internal struct RectEx
    {
        private double x;
        private double y;
        private double width;
        private double height;
        private VerticalAlignment verticalPosition;
        public RectEx(double x, double y, double width, double height, VerticalAlignment position);
        public VerticalAlignment VerticalPosition { get; }
        public static implicit operator Rect(RectEx rX);
    }
}

