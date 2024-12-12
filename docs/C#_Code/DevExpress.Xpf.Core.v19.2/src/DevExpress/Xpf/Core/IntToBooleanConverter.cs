namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class IntToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            !(value is int) ? null : (this.Invert ? ((object) (((int) value) == 0)) : ((object) (((int) value) != 0)));

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            null;

        public bool Invert { get; set; }
    }
}

