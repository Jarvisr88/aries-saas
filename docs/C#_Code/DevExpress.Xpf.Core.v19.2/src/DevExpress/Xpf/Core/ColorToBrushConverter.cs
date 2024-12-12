namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class ColorToBrushConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            new SolidColorBrush((Color) value);

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((SolidColorBrush) value).Color;
    }
}

