namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class BooleanToIntegerConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture);
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}

