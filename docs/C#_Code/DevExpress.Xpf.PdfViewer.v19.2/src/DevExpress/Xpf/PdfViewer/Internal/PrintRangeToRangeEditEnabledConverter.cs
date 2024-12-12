namespace DevExpress.Xpf.PdfViewer.Internal
{
    using System;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Windows.Data;

    public class PrintRangeToRangeEditEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((PrintRange) value) == PrintRange.SomePages;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

