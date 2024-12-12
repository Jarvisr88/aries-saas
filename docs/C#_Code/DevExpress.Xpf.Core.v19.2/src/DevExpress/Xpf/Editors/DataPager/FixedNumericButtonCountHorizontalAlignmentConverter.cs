namespace DevExpress.Xpf.Editors.DataPager
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class FixedNumericButtonCountHorizontalAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (((HorizontalAlignment) value) == HorizontalAlignment.Stretch) ? HorizontalAlignment.Left : value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

