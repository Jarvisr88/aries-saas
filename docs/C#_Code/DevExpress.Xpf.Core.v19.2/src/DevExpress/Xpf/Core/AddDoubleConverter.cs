namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class AddDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((value == null) || !(value is double)) ? 0.0 : (((double) value) + this.Value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public double Value { get; set; }
    }
}

