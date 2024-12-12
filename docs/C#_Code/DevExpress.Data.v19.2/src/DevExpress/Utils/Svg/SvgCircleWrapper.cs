namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing.Drawing2D;

    public class SvgCircleWrapper : SvgElementWrapper
    {
        public SvgCircleWrapper(SvgElement element) : base(element)
        {
        }

        protected override GraphicsPath GetPathCore(double scale)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddEllipse(base.ScaleValue(this.Circle.CenterX - this.Circle.Radius, scale), base.ScaleValue(this.Circle.CenterY - this.Circle.Radius, scale), base.ScaleValue(this.Circle.Radius * 2.0, scale), base.ScaleValue(this.Circle.Radius * 2.0, scale));
            path.CloseFigure();
            return path;
        }

        protected override SmoothingMode GetSmoothingModeCore(SmoothingMode defaultValue) => 
            SmoothingMode.AntiAlias;

        private SvgCircle Circle =>
            base.Element as SvgCircle;
    }
}

