namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class SvgPolylineWrapper : SvgElementWrapper
    {
        public SvgPolylineWrapper(SvgElement element) : base(element)
        {
        }

        protected override GraphicsPath GetPathCore(double scale)
        {
            PointF[] points = new PointF[this.Polyline.SvgPoints.Length];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new PointF(base.ScaleValue(this.Polyline.SvgPoints[i].X, scale), base.ScaleValue(this.Polyline.SvgPoints[i].Y, scale));
            }
            GraphicsPath path = new GraphicsPath();
            path.AddLines(points);
            return path;
        }

        protected override SmoothingMode GetSmoothingModeCore(SmoothingMode defaultValue) => 
            SmoothingMode.AntiAlias;

        private SvgPolyline Polyline =>
            base.Element as SvgPolyline;
    }
}

