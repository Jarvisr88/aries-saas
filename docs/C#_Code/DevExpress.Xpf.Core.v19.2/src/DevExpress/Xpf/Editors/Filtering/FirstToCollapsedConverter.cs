namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class FirstToCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((((StackPanelElementPosition) value) == StackPanelElementPosition.First) || (((StackPanelElementPosition) value) == StackPanelElementPosition.Single)) ? Visibility.Collapsed : Visibility.Visible;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

