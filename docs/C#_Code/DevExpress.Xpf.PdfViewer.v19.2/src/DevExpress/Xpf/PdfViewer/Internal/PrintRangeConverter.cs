namespace DevExpress.Xpf.PdfViewer.Internal
{
    using System;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Windows.Data;

    public class PrintRangeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PrintRange range = (PrintRange) value;
            if (range == PrintRange.AllPages)
            {
                return 0;
            }
            if (range == PrintRange.SomePages)
            {
                return 2;
            }
            if (range != PrintRange.CurrentPage)
            {
                throw new NotSupportedException();
            }
            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (((int) value))
            {
                case 0:
                    return PrintRange.AllPages;

                case 1:
                    return PrintRange.CurrentPage;

                case 2:
                    return PrintRange.SomePages;
            }
            throw new NotSupportedException();
        }
    }
}

