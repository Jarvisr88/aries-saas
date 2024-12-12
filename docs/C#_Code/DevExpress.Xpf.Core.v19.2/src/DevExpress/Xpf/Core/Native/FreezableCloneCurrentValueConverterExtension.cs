namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class FreezableCloneCurrentValueConverterExtension : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
        public override object ProvideValue(IServiceProvider serviceProvider);
    }
}

