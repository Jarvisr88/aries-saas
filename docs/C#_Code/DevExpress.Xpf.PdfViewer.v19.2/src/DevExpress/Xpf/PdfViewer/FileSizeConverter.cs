namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class FileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            PdfFileSizeConverter.ToString(PdfCoreLocalizer.GetString(PdfCoreStringId.FileSize), (double) ((long) value));

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

