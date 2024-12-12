namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class ObjectToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (value != null) ^ this.Invert;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            null;

        public bool Invert { get; set; }
    }
}

