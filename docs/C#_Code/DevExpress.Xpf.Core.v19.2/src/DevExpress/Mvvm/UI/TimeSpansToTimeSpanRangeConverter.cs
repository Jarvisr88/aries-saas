namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    public class TimeSpansToTimeSpanRangeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Count<object>() != 2)
            {
                throw new ArgumentOutOfRangeException();
            }
            return ((!(values[0] is TimeSpan) || !(values[1] is TimeSpan)) ? null : ((object) new TimeSpanRange((TimeSpan) values[0], (TimeSpan) values[1])));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is TimeSpanRange)
            {
                return new object[] { ((TimeSpanRange) value).Start, ((TimeSpanRange) value).End };
            }
            return null;
        }
    }
}

