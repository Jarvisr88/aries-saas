namespace DevExpress.Xpf.Printing.PreviewControl.Bars
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class FormatToItemNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            $"b{this.Prefix}{value}";

        public object ConvertBack(object value, Type targetTypee, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public string Prefix { get; set; }
    }
}

