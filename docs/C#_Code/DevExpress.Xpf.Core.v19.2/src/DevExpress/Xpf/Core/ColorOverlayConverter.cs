namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class ColorOverlayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color transparent = Colors.Transparent;
            if (parameter is string)
            {
                transparent = ColorHelper.CreateColorFromString(parameter as string);
            }
            else if (parameter is Color)
            {
                transparent = (Color) parameter;
            }
            Color color2 = Colors.Transparent;
            if (value is Color)
            {
                color2 = (Color) value;
            }
            else if (value is SolidColorBrush)
            {
                color2 = ((SolidColorBrush) value).Color;
            }
            Color color = ColorHelper.OverlayColor(transparent, color2);
            return (!(targetType == typeof(Color)) ? (!(targetType == typeof(SolidColorBrush)) ? (!(targetType == typeof(Brush)) ? ((object) color) : ((object) new SolidColorBrush(color))) : ((object) new SolidColorBrush(color))) : color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            Colors.Gray;

        private static byte TransformValue(byte baseValue, byte value) => 
            (value <= 0x80) ? ((byte) ((baseValue * value) / 0x80)) : ((byte) (baseValue + (((0xff - baseValue) * (value - 0x80)) / 0x80)));
    }
}

