namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class SubMenuScrollingVisibilityConverter : IMultiValueConverter
    {
        private bool AreClose(double value1, double value2);
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);
    }
}

