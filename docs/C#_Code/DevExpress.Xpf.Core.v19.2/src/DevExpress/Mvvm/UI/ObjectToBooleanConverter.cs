namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class ObjectToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (value != null) ^ this.Inverse;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            null;

        public bool Inverse { get; set; }
    }
}

