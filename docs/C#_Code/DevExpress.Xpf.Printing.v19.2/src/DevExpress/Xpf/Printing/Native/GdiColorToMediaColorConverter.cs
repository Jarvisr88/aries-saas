namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    internal class GdiColorToMediaColorConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture) => 
            DrawingConverter.FromGdiColor((System.Drawing.Color) value);

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture) => 
            DrawingConverter.ToGdiColor((System.Windows.Media.Color) value);
    }
}

