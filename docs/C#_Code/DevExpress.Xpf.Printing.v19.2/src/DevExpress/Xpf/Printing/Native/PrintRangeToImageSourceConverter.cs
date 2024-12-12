namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    internal class PrintRangeToImageSourceConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            PrintRange? nullable = value as PrintRange?;
            if (nullable == null)
            {
                return null;
            }
            string format = "pack://application:,,,/{0};component/Images/Printers/Print{1}.svg";
            SvgImageSourceExtension extension1 = new SvgImageSourceExtension();
            extension1.Uri = new Uri(string.Format(format, "DevExpress.Xpf.Printing.v19.2", nullable.Value));
            return extension1.ProvideValue(null);
        }

        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

