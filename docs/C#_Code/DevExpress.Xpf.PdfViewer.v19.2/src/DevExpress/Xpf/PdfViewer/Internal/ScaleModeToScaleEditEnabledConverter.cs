namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Pdf;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ScaleModeToScaleEditEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((PdfPrintScaleMode) value) == PdfPrintScaleMode.CustomScale;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

