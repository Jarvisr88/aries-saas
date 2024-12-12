namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Localization;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public abstract class StringIdConverter<T> : IValueConverter where T: struct
    {
        protected StringIdConverter()
        {
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            this.Localizer.GetLocalizedString((T) Enum.Parse(typeof(T), (string) parameter, false));

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected abstract XtraLocalizer<T> Localizer { get; }
    }
}

