namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            value?.ToString();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((value == null) || !targetType.IsEnum) ? value : Enum.Parse(targetType, value.ToString());
    }
}

