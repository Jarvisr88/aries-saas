namespace DevExpress.Data.Svg
{
    using System;
    using System.Drawing;

    public class SvgLinearGradientBrush : SvgGradientBrush
    {
        public SvgLinearGradientBrush(Color color1, Color color2);
        public SvgLinearGradientBrush(Color color1, Color color2, SvgGradientUnits units);
        protected override SvgGradientDefinition CreateDefinition();
    }
}

