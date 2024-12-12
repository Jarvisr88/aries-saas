namespace DevExpress.Xpf.Office.UI
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public abstract class StringDelimitedWithCRLFAndTabsToPartConverter : IValueConverter
    {
        protected StringDelimitedWithCRLFAndTabsToPartConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
            {
                return value;
            }
            string str = this.GetSubstring(value as string, this.Y, this.X);
            return (string.IsNullOrEmpty(str) ? value : str);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected string GetSubstring(string entireString, int y, int x)
        {
            if (string.IsNullOrEmpty(entireString))
            {
                return string.Empty;
            }
            entireString = entireString.Replace("\t\t", "\t");
            entireString = entireString.Replace("\r\n", "\n");
            char[] separator = new char[] { '\n' };
            string[] strArray = entireString.Split(separator);
            if (y >= strArray.Length)
            {
                return string.Empty;
            }
            char[] chArray2 = new char[] { '\t' };
            string[] strArray2 = strArray[y].Split(chArray2);
            return ((x < strArray2.Length) ? strArray2[x] : string.Empty);
        }

        protected abstract int X { get; }

        protected abstract int Y { get; }
    }
}

