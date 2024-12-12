namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Globalization;
    using System.Security;
    using System.Windows.Data;

    public class SecureStringToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            string.Empty;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;
            if (string.IsNullOrEmpty(str))
            {
                return new SecureString();
            }
            SecureString str2 = new SecureString();
            foreach (char ch in str)
            {
                str2.AppendChar(ch);
            }
            return str2;
        }
    }
}

