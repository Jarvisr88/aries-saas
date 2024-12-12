namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public abstract class NullableStructToValueConverterExtension<TValue> : MarkupExtension, IValueConverter where TValue: struct
    {
        private TValue defaultValue;

        public NullableStructToValueConverterExtension();
        public NullableStructToValueConverterExtension(TValue defaultValue);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
        public override object ProvideValue(IServiceProvider serviceProvider);
    }
}

