namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils.Design;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;

    public class SvgGradientWrapper : SvgElementWrapper
    {
        public SvgGradientWrapper(SvgElement element) : base(element)
        {
        }

        protected float GetActualValue(SvgUnit unit, RectangleF bounds) => 
            (unit.UnitType != SvgUnitType.Percentage) ? (((float) unit.Value) / bounds.Width) : (((float) unit.UnitValue) / 100f);

        protected ColorBlend GetColorBlend(RectangleF bounds, double opacity, bool radial)
        {
            int count = this.Gradient.Stops.Count;
            bool flag = false;
            bool flag2 = false;
            double unitValue = this.Gradient.Stops[this.Gradient.Stops.Count - 1].Offset.UnitValue;
            if (this.Gradient.Stops[0].Offset.UnitValue > 0.0)
            {
                count++;
                if (radial)
                {
                    flag2 = true;
                }
                else
                {
                    flag = true;
                }
            }
            if ((unitValue < 100.0) || (unitValue < 1.0))
            {
                count++;
                if (radial)
                {
                    flag = true;
                }
                else
                {
                    flag2 = true;
                }
            }
            ColorBlend blend = new ColorBlend(count);
            int num4 = 0;
            double num5 = 0.0;
            float num6 = 0f;
            Color black = Color.Black;
            for (int i = 0; i < count; i++)
            {
                SvgGradientStop stop = this.Gradient.Stops[radial ? ((this.Gradient.Stops.Count - 1) - num4) : num4];
                double? nullable = stop.Opacity;
                num5 = opacity * nullable.GetValueOrDefault(1.0);
                num6 = radial ? (1f - this.GetActualValue(stop.Offset, bounds)) : this.GetActualValue(stop.Offset, bounds);
                nullable = null;
                nullable = null;
                black = Color.FromArgb((int) (255.0 * num5), base.GetColor(base.PaletteProvider, stop.StopColor, nullable, nullable, true));
                if (flag && (i == 0))
                {
                    blend.Positions[i] = 0f;
                    blend.Colors[i] = black;
                    i++;
                }
                blend.Positions[i] = num6;
                blend.Colors[i] = black;
                if (flag2 && (i == (count - 2)))
                {
                    i++;
                    blend.Positions[i] = 1f;
                    blend.Colors[i] = black;
                }
                num4++;
            }
            return blend;
        }

        protected PointF GetPoint(SvgCoordinateUnits gradientUnits, SvgUnit x, SvgUnit y, double scale = 1.0)
        {
            double num = x.Value;
            double num2 = y.Value;
            if (gradientUnits == SvgCoordinateUnits.ObjectBoundingBox)
            {
                num = (x.UnitType == SvgUnitType.Percentage) ? (x.UnitValue / 100.0) : num;
                num2 = (y.UnitType == SvgUnitType.Percentage) ? (y.UnitValue / 100.0) : num2;
            }
            else if (scale < 1.0)
            {
                num = Math.Round((double) (x.Value * scale), 3);
                num2 = Math.Round((double) (y.Value * scale), 3);
            }
            else
            {
                num = x.Value * scale;
                num2 = y.Value * scale;
            }
            return new PointF((float) num, (float) num2);
        }

        protected internal override Matrix GetTransform(Matrix m, double scale, bool clone = false)
        {
            Matrix matrix = m;
            if (clone)
            {
                matrix = m.Clone();
            }
            if (this.Gradient.GradientTransformation != null)
            {
                foreach (SvgTransform transform in this.Gradient.GradientTransformation)
                {
                    matrix.Multiply(transform.GetMatrix(scale));
                }
            }
            return matrix;
        }

        protected double GetValue(SvgCoordinateUnits gradientUnits, SvgUnit value) => 
            (gradientUnits != SvgCoordinateUnits.ObjectBoundingBox) ? value.Value : ((value.UnitType == SvgUnitType.Percentage) ? (value.UnitValue / 100.0) : value.Value);

        public override void Render(ISvgGraphics g, ISvgPaletteProvider paletteProvider, double scale)
        {
        }

        private SvgGradient Gradient =>
            base.Element as SvgGradient;
    }
}

