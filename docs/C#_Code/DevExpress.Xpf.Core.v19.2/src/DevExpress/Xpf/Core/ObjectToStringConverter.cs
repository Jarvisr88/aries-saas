namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ObjectToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (value == null) ? value : string.Format("{0:" + ((string) parameter) + "}", value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;
    }
}

