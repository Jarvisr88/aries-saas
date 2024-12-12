namespace DevExpress.Xpf.Editors.Filtering
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class AdditionalOperandsCountToVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int num = Convert.ToInt32(value);
            return (((num == 0) || (num == 1)) ? Visibility.Collapsed : Visibility.Visible);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

