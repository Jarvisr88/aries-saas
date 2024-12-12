namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class StringEmptyToSpaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            !string.IsNullOrEmpty(System.Convert.ToString(value)) ? value : " ";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;
    }
}

