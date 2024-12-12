namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class SearchControlFindButtonVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => 
            ((values == null) || ((values.Length != 2) || (!(values[0] as bool) || !(values[1] as bool)))) ? ((object) Visibility.Collapsed) : (((bool) values[0]) ? ((object) 0) : ((object) ((bool) values[1])));

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

