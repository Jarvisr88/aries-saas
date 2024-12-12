namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class GridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double d = (double) value;
            return (!double.IsNaN(d) ? new GridLength(d, GridUnitType.Star) : GridLength.Auto);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;
    }
}

