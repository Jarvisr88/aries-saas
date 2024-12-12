namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class WpfSvgStartEndPoints
    {
        public WpfSvgStartEndPoints(Point start, Point end);

        public Point Start { get; }

        public Point End { get; }
    }
}

