namespace DevExpress.Data.Svg
{
    using System;
    using System.Drawing;

    public class SvgRadialGradientBrush : SvgGradientBrush
    {
        public SvgRadialGradientBrush(Color color1, Color color2);
        public SvgRadialGradientBrush(Color color1, Color color2, SvgGradientUnits units);
        public SvgRadialGradientBrush(Color color1, Color color2, SvgGradientUnits units, SvgPoint location, double radius);
        protected override SvgGradientDefinition CreateDefinition();
    }
}

