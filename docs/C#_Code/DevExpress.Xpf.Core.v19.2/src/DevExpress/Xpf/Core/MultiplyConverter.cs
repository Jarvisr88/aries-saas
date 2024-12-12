namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class MultiplyConverter : IValueConverter
    {
        public static readonly MultiplyConverter Instance = new MultiplyConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            System.Convert.ToDouble(value) * System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception();
        }
    }
}

