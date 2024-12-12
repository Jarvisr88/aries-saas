namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class PageNumberConverter : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            string format = (string) values[1];
            return ((PageNumberKind) values[0]).ToPageInfo().GetText(format, ((int) values[2]), ((int) values[3]), null);
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

