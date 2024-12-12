namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class ShowDefaultColorToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((bool) value) ? new GridLength(1.0, GridUnitType.Star) : new GridLength(0.0, GridUnitType.Pixel);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

