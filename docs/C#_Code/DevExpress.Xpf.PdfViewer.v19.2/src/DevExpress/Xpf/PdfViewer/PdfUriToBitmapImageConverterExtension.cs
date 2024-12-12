namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Media.Imaging;

    public class PdfUriToBitmapImageConverterExtension : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri uriSource = value as Uri;
            return ((uriSource != null) ? new BitmapImage(uriSource) : null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            null;

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new UriToBitmapImageConverterExtension();
    }
}

