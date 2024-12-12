namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class PointMapping : IMapping
    {
        public PointMapping(IMapping mapping, Point offset)
        {
            this.Location = this.Add(mapping.Location, offset);
            this.Size = new System.Windows.Size(mapping.Size.Width - offset.X, mapping.Size.Height - offset.Y);
        }

        public PointMapping(Point offset, Rect surfaceBounds)
        {
            System.Windows.Size size = surfaceBounds.Size();
            this.Size = new System.Windows.Size(size.Width - offset.X, size.Height - offset.Y);
            this.Location = this.Add(surfaceBounds.Location, offset);
        }

        private Point Add(Point p1, Point p2) => 
            new Point(p1.X + p2.X, p1.Y + p2.Y);

        public Point GetPoint(double x, double y)
        {
            double num = this.Size.Height - y;
            return new Point(x + this.Location.X, num + this.Location.Y);
        }

        public Point GetSnappedPoint(double normalX, double normalY, bool snapToPixels = true)
        {
            double x = this.Size.Width * normalX;
            Point point = this.GetPoint(x, this.Size.Height * normalY);
            return (snapToPixels ? new Point(this.SnapToPixels(point.X), this.SnapToPixels(point.Y)) : point);
        }

        private double SnapToPixels(double value) => 
            Math.Round(value);

        public System.Windows.Size Size { get; private set; }

        public Point Location { get; private set; }
    }
}

