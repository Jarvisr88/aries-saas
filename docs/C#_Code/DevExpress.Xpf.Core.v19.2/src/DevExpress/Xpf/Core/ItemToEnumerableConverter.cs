namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    public class ItemToEnumerableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            return new object[] { value };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Array source = value as Array;
            return ((source == null) ? null : source.Cast<object>().FirstOrDefault<object>());
        }
    }
}

