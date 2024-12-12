namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class BrushToColorConverter : IValueConverter
    {
        public static Color Convert(object value) => 
            ColorToBrushConverter.ConvertBack(value);

        public static SolidColorBrush ConvertBack(object value)
        {
            byte? customA = null;
            return ColorToBrushConverter.Convert(value, customA);
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (value is SolidColorBrush) ? Convert(value) : Colors.Black;

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            ConvertBack(value);
    }
}

