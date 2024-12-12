namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Svg;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public static class WpfSvgExtensions
    {
        public static Color ChangeColorBrightness(this Color baseColor, double brightness)
        {
            brightness = Math.Min(1.0, Math.Max(-1.0, brightness));
            Func<byte, byte> func = (brightness <= 0.0) ? x => ((byte) (x * (1.0 + brightness))) : x => ((byte) (x + ((0xff - x) * brightness)));
            return Color.FromArgb(baseColor.A, func(baseColor.R), func(baseColor.G), func(baseColor.B));
        }

        public static Brush SetBrightness(this Brush brush, double brightness)
        {
            Brush brush2;
            if ((brightness < -1.0) || (brightness > 1.0))
            {
                throw new ArgumentOutOfRangeException("brightness", "brightness should be between -1 and 1");
            }
            if (brush == null)
            {
                return null;
            }
            if (ReferenceEquals(brush, Brushes.Transparent))
            {
                return Brushes.Transparent;
            }
            SolidColorBrush brush3 = brush as SolidColorBrush;
            if (brush3 != null)
            {
                SolidColorBrush brush1 = new SolidColorBrush(brush3.Color.ChangeColorBrightness(brightness));
                brush1.Opacity = brush3.Opacity;
                brush2 = brush1;
            }
            else if (!(brush is GradientBrush))
            {
                brush2 = brush.Clone();
            }
            else
            {
                brush2 = brush.Clone();
                foreach (GradientStop stop in ((GradientBrush) brush2).GradientStops)
                {
                    Color color2 = stop.Color.ChangeColorBrightness(brightness);
                }
            }
            return brush2;
        }

        public static Brush SetBrushBrightness(this Brush brush, double? brightness)
        {
            Brush brush2 = null;
            if (brush != null)
            {
                brush2 = brush.Clone();
            }
            if (brightness != null)
            {
                brush2 = (brush2 != null) ? brush2.SetBrightness(brightness.Value) : null;
            }
            return brush2;
        }

        public static Brush SetBrushOpacity(this Brush brush, double? opacity)
        {
            Brush brush2 = null;
            if (brush != null)
            {
                brush2 = brush.Clone();
            }
            if ((opacity != null) && (brush2 != null))
            {
                brush2.Opacity = opacity.Value;
            }
            return brush2;
        }

        public static BrushMappingMode ToGradientMappingMode(this SvgCoordinateUnits gradientUnits) => 
            (gradientUnits == SvgCoordinateUnits.ObjectBoundingBox) ? BrushMappingMode.RelativeToBoundingBox : ((gradientUnits == SvgCoordinateUnits.UserSpaceOnUse) ? BrushMappingMode.Absolute : BrushMappingMode.Absolute);

        public static GradientSpreadMethod ToGradientSpreadMethod(this SvgGradientSpreadMethod spreadMethod)
        {
            switch (spreadMethod)
            {
                case SvgGradientSpreadMethod.Pad:
                    return GradientSpreadMethod.Pad;

                case SvgGradientSpreadMethod.Reflect:
                    return GradientSpreadMethod.Reflect;

                case SvgGradientSpreadMethod.Repeat:
                    return GradientSpreadMethod.Repeat;
            }
            return GradientSpreadMethod.Pad;
        }
    }
}

