namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class BaseEditToBarCodeEditConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            value as BarCodeEdit;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            value as BaseEdit;
    }
}

