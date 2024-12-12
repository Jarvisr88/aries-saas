namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ColorConverter : IValueConverter, IMultiValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            Text2ColorHelper.Convert(value, true);

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool includeAlpha = (bool) values[1];
            return Text2ColorHelper.Convert(values[0], includeAlpha);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            Text2ColorHelper.ConvertBack(value);

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => 
            new object[] { Text2ColorHelper.ConvertBack(value) };
    }
}

