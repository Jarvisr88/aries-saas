namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Pdf;
    using DevExpress.Xpf.PdfViewer;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class PdfPrintPageOrientationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (((PdfPrintPageOrientation) value))
            {
                case PdfPrintPageOrientation.Auto:
                    return PdfViewerLocalizer.GetString(PdfViewerStringId.PrintDialogPrintOrientationAuto);

                case PdfPrintPageOrientation.Portrait:
                    return PdfViewerLocalizer.GetString(PdfViewerStringId.PrintDialogPrintOrientationPortrait);

                case PdfPrintPageOrientation.Landscape:
                    return PdfViewerLocalizer.GetString(PdfViewerStringId.PrintDialogPrintOrientationLandscape);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

