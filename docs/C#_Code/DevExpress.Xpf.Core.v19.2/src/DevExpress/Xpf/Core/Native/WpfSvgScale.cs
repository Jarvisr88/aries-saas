namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal class WpfSvgScale
    {
        public WpfSvgScale(double scaleX, double scaleY);

        public double ScaleX { get; set; }

        public double ScaleY { get; set; }

        public double OffsetX { get; set; }

        public double OffsetY { get; set; }
    }
}

