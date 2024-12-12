namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class BoolToStringConverter : IValueConverter
    {
        public BoolToStringConverter()
        {
        }

        public BoolToStringConverter(string trueString, string falseString)
        {
            this.TrueString = trueString;
            this.FalseString = falseString;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            !(value as bool) ? value.ToString() : (((bool) value) ? this.TrueString : this.FalseString);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public string TrueString { get; set; }

        public string FalseString { get; set; }
    }
}

