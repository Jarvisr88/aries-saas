namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class HColorToZColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            new HSBColor((int) value, 100, 100).Color;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

