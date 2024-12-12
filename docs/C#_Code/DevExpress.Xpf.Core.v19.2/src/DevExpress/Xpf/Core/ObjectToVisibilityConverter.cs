namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class ObjectToVisibilityConverter : IValueConverter
    {
        public ObjectToVisibilityConverter()
        {
            this.HandleList = true;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!this.HandleList)
            {
                return (((value != null) ^ this.Invert) ? Visibility.Visible : Visibility.Collapsed);
            }
            IList list = value as IList;
            return ((((list != null) && (list.Count > 0)) ^ this.Invert) ? Visibility.Visible : Visibility.Collapsed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            null;

        public bool Invert { get; set; }

        public bool HandleList { get; set; }
    }
}

