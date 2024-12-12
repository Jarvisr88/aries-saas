namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    internal class GdiColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture) => 
            (value != null) ? new SolidColorBrush(DrawingConverter.FromGdiColor((System.Drawing.Color) value)) : null;

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

