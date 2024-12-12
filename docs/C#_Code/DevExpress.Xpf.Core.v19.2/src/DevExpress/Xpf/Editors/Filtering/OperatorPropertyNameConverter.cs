namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class OperatorPropertyNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            OperandProperty property = value as OperandProperty;
            return ((property == null) ? string.Empty : property.PropertyName);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

