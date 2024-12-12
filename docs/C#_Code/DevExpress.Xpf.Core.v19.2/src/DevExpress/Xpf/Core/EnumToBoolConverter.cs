namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class EnumToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            value.Equals(Enum.Parse(value.GetType(), (string) parameter, true));

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            !((bool) value) ? DependencyProperty.UnsetValue : Enum.Parse(targetType, (string) parameter, true);
    }
}

