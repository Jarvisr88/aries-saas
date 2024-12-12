namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class TrackBarEditRangeMultiValueConverter : IMultiValueConverter
    {
        private static bool IsNullOrUnset(object value) => 
            (value == null) || (value == DependencyProperty.UnsetValue);

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => 
            (IsNullOrUnset(values[0]) || IsNullOrUnset(values[1])) ? DependencyProperty.UnsetValue : new TrackBarEditRange(Convert.ToDouble(values[0]), Convert.ToDouble(values[1]));

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            TrackBarEditRange range = value as TrackBarEditRange;
            if (range == null)
            {
                return null;
            }
            Type nullableType = targetTypes[0] ?? typeof(object);
            nullableType = Nullable.GetUnderlyingType(nullableType) ?? nullableType;
            return new object[] { Convert.ChangeType(range.SelectionStart, nullableType), Convert.ChangeType(range.SelectionEnd, nullableType) };
        }
    }
}

