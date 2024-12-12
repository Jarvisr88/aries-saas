namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    public class DateTimesToDateTimeRangeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Count<object>() != 2)
            {
                throw new ArgumentOutOfRangeException();
            }
            return ((!(values[0] is DateTime) || !(values[1] is DateTime)) ? null : ((object) new DateTimeRange((DateTime) values[0], (DateTime) values[1])));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is DateTimeRange)
            {
                return new object[] { ((DateTimeRange) value).Start, ((DateTimeRange) value).End };
            }
            return null;
        }
    }
}

