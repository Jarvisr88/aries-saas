namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class SvgPolygonWrapper : SvgElementWrapper
    {
        public SvgPolygonWrapper(SvgElement element) : base(element)
        {
        }

        protected override GraphicsPath GetPathCore(double scale)
        {
            GraphicsPath path = new GraphicsPath();
            if ((this.Polygon != null) && (this.Polygon.SvgPoints != null))
            {
                PointF[] points = new PointF[this.Polygon.SvgPoints.Length];
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = new PointF(base.ScaleValue(this.Polygon.SvgPoints[i].X, scale), base.ScaleValue(this.Polygon.SvgPoints[i].Y, scale));
                }
                path.StartFigure();
                path.AddPolygon(points);
                path.CloseFigure();
            }
            return path;
        }

        protected override SmoothingMode GetSmoothingModeCore(SmoothingMode defaultValue) => 
            SmoothingMode.AntiAlias;

        private SvgPolygon Polygon =>
            base.Element as SvgPolygon;
    }
}

