namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class BoolToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((bool) value) ? double.Parse(parameter.ToString(), CultureInfo.InvariantCulture) : 0.0;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((double) value) > 0.0;
    }
}

