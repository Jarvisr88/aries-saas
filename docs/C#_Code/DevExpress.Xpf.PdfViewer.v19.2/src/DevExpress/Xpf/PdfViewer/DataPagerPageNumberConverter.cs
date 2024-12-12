namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DataPagerPageNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            string.Format(PdfViewerLocalizer.GetString(PdfViewerStringId.PrintDialogPaginationOf), value.ToString());

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

