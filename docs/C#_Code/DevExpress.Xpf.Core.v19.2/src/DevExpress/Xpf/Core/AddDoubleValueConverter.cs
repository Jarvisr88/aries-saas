namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class AddDoubleValueConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((double) value) + double.Parse((string) parameter);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            null;

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

