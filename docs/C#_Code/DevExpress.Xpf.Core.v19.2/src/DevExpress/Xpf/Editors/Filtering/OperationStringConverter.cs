namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class OperationStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            OperationHelper.GetMenuStringByType((ClauseType) value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

