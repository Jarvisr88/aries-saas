namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class PdfColorToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PdfColor color = (PdfColor) value;
            return new SolidColorBrush(Color.FromArgb(0xff, (byte) (255.0 * color.Components[0]), (byte) (255.0 * color.Components[1]), (byte) (255.0 * color.Components[2])));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

