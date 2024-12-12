namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class SvgLineWrapper : SvgElementWrapper
    {
        public SvgLineWrapper(SvgElement element) : base(element)
        {
        }

        protected override GraphicsPath GetPathCore(double scale)
        {
            PointF tf = new PointF(base.ScaleValue(this.Line.StartX, scale), base.ScaleValue(this.Line.StartY, scale));
            PointF tf2 = new PointF(base.ScaleValue(this.Line.EndX, scale), base.ScaleValue(this.Line.EndY, scale));
            GraphicsPath path = new GraphicsPath();
            path.AddLine(tf, tf2);
            return path;
        }

        protected override void RenderElement(ISvgGraphics g, double scale, SvgElement element)
        {
            SvgGradient svgGradient = null;
            double? brightness = null;
            using (SolidBrush brush = new SolidBrush(this.GetColor(element.Stroke, base.GetOpacity(element.Opacity, element.StrokeOpacity), brightness, element.UsePalette, out svgGradient, false)))
            {
                using (Pen pen = this.GetPenCore(brush, scale))
                {
                    g.DrawLine(pen, base.ScaleValue(this.Line.StartX, scale), base.ScaleValue(this.Line.StartY, scale), base.ScaleValue(this.Line.EndX, scale), base.ScaleValue(this.Line.EndY, scale));
                }
            }
        }

        private SvgLine Line =>
            base.Element as SvgLine;
    }
}

