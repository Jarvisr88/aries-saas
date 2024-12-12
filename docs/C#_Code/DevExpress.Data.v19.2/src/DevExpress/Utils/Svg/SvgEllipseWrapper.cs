namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing.Drawing2D;

    public class SvgEllipseWrapper : SvgElementWrapper
    {
        public SvgEllipseWrapper(SvgElement element) : base(element)
        {
        }

        protected override GraphicsPath GetPathCore(double scale)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddEllipse(base.ScaleValue(this.Ellipse.CenterX - this.Ellipse.RadiusX, scale), base.ScaleValue(this.Ellipse.CenterY - this.Ellipse.RadiusY, scale), base.ScaleValue(this.Ellipse.RadiusX * 2.0, scale), base.ScaleValue(this.Ellipse.RadiusY * 2.0, scale));
            path.CloseFigure();
            return path;
        }

        protected override SmoothingMode GetSmoothingModeCore(SmoothingMode defaultValue) => 
            SmoothingMode.AntiAlias;

        private SvgEllipse Ellipse =>
            base.Element as SvgEllipse;
    }
}

