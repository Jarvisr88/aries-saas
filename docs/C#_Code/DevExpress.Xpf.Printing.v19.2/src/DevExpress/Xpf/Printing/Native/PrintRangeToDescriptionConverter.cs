namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class PrintRangeToDescriptionConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            PrintRange? nullable = value as PrintRange?;
            return (((nullable == null) || (((PrintRange) nullable.Value) == PrintRange.Selection)) ? null : ((((PrintRange) nullable.Value) != PrintRange.AllPages) ? ((((PrintRange) nullable.Value) != PrintRange.CurrentPage) ? PrintingLocalizer.GetString(PrintingStringId.PrintCustomPagesDescription) : PrintingLocalizer.GetString(PrintingStringId.PrintCurrentPageDescription)) : PrintingLocalizer.GetString(PrintingStringId.PrintAllPagesDescription)));
        }

        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

