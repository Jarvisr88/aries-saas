namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class GradientBrushTypeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GradientBrushType type;
            return (((value is GradientBrushType) && Enum.TryParse<GradientBrushType>(parameter.ToString(), out type)) && (((GradientBrushType) value) == type));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GradientBrushType type;
            if (!(value as bool) || !Enum.TryParse<GradientBrushType>(parameter.ToString(), out type))
            {
                return GradientBrushType.Linear;
            }
            if (type == GradientBrushType.Linear)
            {
                return (((bool) value) ? GradientBrushType.Linear : GradientBrushType.Radial);
            }
            if (type != GradientBrushType.Radial)
            {
                throw new ArgumentOutOfRangeException("parameter");
            }
            return (((bool) value) ? GradientBrushType.Radial : GradientBrushType.Linear);
        }
    }
}

