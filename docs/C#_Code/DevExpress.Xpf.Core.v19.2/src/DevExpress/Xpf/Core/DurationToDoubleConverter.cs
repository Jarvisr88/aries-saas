namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class DurationToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Duration duration = (Duration) value;
            return (duration.HasTimeSpan ? ((object) ((duration.TimeSpan.Seconds * 0x3e8) + duration.TimeSpan.Milliseconds)) : null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            new Duration(TimeSpan.FromMilliseconds((double) value));
    }
}

