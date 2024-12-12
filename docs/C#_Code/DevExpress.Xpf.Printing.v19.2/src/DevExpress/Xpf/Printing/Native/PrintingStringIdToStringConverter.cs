namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    internal class PrintingStringIdToStringConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            PrintingStringId? nullable = value as PrintingStringId?;
            return ((nullable != null) ? PrintingLocalizer.GetString(nullable.Value) : null);
        }

        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

