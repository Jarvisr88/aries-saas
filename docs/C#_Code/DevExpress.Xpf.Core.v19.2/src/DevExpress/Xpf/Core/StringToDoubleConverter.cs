namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class StringToDoubleConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            Convert.ToDouble(value, culture);

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

