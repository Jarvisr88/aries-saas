namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Data;

    public class ObjectToCollectionConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            List<object> list1 = new List<object>();
            list1.Add(value);
            return list1;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

