namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DebugConverter : IValueConverter
    {
        public static readonly DebugConverter Instance = new DebugConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;
    }
}

