namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DecimalToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            System.Convert.ToInt32(value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            System.Convert.ToDecimal(value);
    }
}

