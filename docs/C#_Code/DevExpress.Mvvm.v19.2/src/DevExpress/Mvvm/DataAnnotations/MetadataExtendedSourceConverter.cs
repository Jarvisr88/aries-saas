namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class MetadataExtendedSourceConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            new MetadataExtendedSource(value, (MetadataLocator) parameter);

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

