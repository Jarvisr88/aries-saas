namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class WindowCaptionHeightConverterExtension : MarkupExtension, IMultiValueConverter
    {
        private static WindowCaptionHeightConverterExtension converter;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            WindowKind kind = (WindowKind) values[1];
            return (double) values[0];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            converter ??= new WindowCaptionHeightConverterExtension();
    }
}

