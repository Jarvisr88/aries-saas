namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class ContentBorderMinWidthConverterExtension : MarkupExtension, IMultiValueConverter
    {
        private static ContentBorderMinWidthConverterExtension converter;

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness thickness = (Thickness) value[1];
            return Math.Max((SystemParameters.MinimumWindowWidth - thickness.Left) - thickness.Right, (double) value[0]);
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            converter ??= new ContentBorderMinWidthConverterExtension();
    }
}

