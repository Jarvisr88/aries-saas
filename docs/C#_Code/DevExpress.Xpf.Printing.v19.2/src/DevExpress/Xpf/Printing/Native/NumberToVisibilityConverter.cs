namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class NumberToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture) => 
            (System.Convert.ToInt32(value) > 0) ? Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility))
            {
                throw new ArgumentException("value");
            }
            return ((((Visibility) value) == Visibility.Visible) ? 1 : 0);
        }
    }
}

