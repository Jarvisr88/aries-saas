namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DXTabbedWindowMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);
    }
}

