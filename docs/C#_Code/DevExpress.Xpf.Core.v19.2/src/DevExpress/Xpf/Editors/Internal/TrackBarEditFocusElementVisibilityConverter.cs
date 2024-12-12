namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class TrackBarEditFocusElementVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (((EditMode) value) == EditMode.Standalone) ? Visibility.Visible : Visibility.Collapsed;

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

