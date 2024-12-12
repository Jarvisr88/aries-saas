namespace DevExpress.Xpf.Printing.Parameters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class StringToGuidConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = value as string;
            Guid result = default(Guid);
            return (!Guid.TryParse(input, out result) ? value : result);
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

