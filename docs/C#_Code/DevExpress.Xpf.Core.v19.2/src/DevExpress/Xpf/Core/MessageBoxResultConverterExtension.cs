namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class MessageBoxResultConverterExtension : MarkupExtension, IValueConverter
    {
        private static MessageBoxResultConverterExtension converter;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            !(value is MessageBoxResult) ? ((value is MessageResult) ? ((object) ((MessageResult) value).ToMessageBoxResult()) : ((object) 0)) : value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            converter ??= new MessageBoxResultConverterExtension();
    }
}

