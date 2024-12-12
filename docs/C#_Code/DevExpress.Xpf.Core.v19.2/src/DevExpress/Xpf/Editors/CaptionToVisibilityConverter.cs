namespace DevExpress.Xpf.Editors
{
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class CaptionToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            string.IsNullOrEmpty((string) value) ? DefaultBoolean.False : DefaultBoolean.True;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

