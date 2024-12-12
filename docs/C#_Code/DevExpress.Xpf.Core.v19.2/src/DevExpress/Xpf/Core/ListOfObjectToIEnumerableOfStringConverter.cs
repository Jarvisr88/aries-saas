namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    public class ListOfObjectToIEnumerableOfStringConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (value != null) ? ((IEnumerable) value).OfType<object>().ToList<object>() : null;

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            (value != null) ? ((List<object>) value).Cast<string>() : null;
    }
}

