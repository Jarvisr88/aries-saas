namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class ColorToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ColorDisplayFormat format = ColorDisplayFormat.Default;
            if (parameter != null)
            {
                format = (ColorDisplayFormat) parameter;
            }
            return ColorEditHelper.GetColorName((Color) value, format, true);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

