namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class SearchControlNullTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => 
            (!(values[1] is string) || (!(values[0] is string) && (values[0] != null))) ? null : ((values[0] == null) ? values[1] : values[0]);

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

