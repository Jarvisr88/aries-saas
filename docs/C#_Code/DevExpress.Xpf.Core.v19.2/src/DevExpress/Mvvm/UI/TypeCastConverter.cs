namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class TypeCastConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            TypeCastHelper.TryCast(value, targetType);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            TypeCastHelper.TryCast(value, targetType);
    }
}

