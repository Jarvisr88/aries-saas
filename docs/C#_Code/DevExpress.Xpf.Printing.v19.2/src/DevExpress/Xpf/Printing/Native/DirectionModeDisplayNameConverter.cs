namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class DirectionModeDisplayNameConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, System.Type targetType, object parameter, CultureInfo culture) => 
            WatermarkLocalizers.LocalizeDirectionMode(Guard.ArgumentMatchType<DirectionMode>(value, "value"));

        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

