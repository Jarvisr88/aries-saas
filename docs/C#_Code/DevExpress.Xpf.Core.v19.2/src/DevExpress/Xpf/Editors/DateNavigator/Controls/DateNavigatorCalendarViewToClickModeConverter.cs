namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using DevExpress.Xpf.Editors.DateNavigator;
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class DateNavigatorCalendarViewToClickModeConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            char[] separator = new char[] { ';' };
            string[] strArray = ((string) parameter).Split(separator);
            return Enum.Parse(typeof(ClickMode), strArray[(int) ((DateNavigatorCalendarView) value)], false);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

