namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class CollateStringToCollateDescriptionConverter : MarkupExtension, IValueConverter
    {
        private const string collateDescription = "1,2,3  1,2,3  1,2,3";
        private const string uncollateDescription = "1,1,1  2,2,2  3,3,3";

        private string GetDescription(PrintingStringId value) => 
            (value == PrintingStringId.Collated) ? "1,2,3  1,2,3  1,2,3" : ((value == PrintingStringId.Uncollated) ? "1,1,1  2,2,2  3,3,3" : null);

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            PrintingStringId? nullable = value as PrintingStringId?;
            return ((nullable != null) ? this.GetDescription(nullable.Value) : null);
        }

        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

