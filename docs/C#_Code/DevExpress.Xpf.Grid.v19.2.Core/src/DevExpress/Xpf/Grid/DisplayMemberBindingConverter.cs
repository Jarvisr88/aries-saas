namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class DisplayMemberBindingConverter : IValueConverter
    {
        public static DisplayMemberBindingConverter Instance = new DisplayMemberBindingConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;
    }
}

