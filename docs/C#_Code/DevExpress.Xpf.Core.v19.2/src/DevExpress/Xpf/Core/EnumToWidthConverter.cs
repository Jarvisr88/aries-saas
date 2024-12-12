namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class EnumToWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (value.ToString() == this.EnumValue.ToString()) ? this.WidthTrue : this.WidthFalse;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object EnumValue { get; set; }

        public double WidthTrue { get; set; }

        public double WidthFalse { get; set; }
    }
}

