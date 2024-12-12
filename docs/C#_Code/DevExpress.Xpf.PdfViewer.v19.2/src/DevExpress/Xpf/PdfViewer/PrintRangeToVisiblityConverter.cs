namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class PrintRangeToVisiblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (((PrintRange) value) == PrintRange.AllPages) ? Visibility.Collapsed : Visibility.Visible;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

