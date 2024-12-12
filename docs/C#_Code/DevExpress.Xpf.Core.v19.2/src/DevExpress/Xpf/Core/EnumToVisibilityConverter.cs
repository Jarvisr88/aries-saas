namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class EnumToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (value.Equals(Enum.Parse(value.GetType(), (string) parameter, true)) ^ this.Invert) ? Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("EnumToVisibilityConverter.ConvertBack");
        }

        public bool Invert { get; set; }
    }
}

