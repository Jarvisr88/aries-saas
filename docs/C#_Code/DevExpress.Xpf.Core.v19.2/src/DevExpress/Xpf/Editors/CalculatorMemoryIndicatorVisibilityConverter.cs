namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class CalculatorMemoryIndicatorVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (((decimal) value) != 0M) ? Visibility.Visible : Visibility.Collapsed;

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

