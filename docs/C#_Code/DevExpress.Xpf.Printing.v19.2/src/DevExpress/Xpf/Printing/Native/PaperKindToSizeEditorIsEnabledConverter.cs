namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class PaperKindToSizeEditorIsEnabledConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, System.Type targetType, object parameter, CultureInfo culture) => 
            ((PaperKind) value) == PaperKind.Custom;

        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

