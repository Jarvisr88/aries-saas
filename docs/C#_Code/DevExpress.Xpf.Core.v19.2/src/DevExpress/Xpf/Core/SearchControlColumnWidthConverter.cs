namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class SearchControlColumnWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => 
            ((values == null) || ((values.Length != 2) || (!(values[0] as bool) || !(values[1] as bool)))) ? GridLength.Auto : (((bool) values[0]) ? ((!((bool) values[0]) || ((bool) values[1])) ? new GridLength(1.0, GridUnitType.Star) : GridLength.Auto) : GridLength.Auto);

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

