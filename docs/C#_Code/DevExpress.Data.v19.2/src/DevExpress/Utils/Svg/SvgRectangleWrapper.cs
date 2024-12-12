namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class SvgRectangleWrapper : SvgElementWrapper
    {
        public SvgRectangleWrapper(SvgElement element) : base(element)
        {
        }

        protected override GraphicsPath GetPathCore(double scale)
        {
            RectangleF rect = new RectangleF(base.ScaleValue(this.Rectangle.X, scale), base.ScaleValue(this.Rectangle.Y, scale), base.ScaleValue(this.Rectangle.Width, scale), base.ScaleValue(this.Rectangle.Height, scale));
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddRectangle(rect);
            path.CloseFigure();
            return path;
        }

        private SvgRectangle Rectangle =>
            base.Element as SvgRectangle;
    }
}

