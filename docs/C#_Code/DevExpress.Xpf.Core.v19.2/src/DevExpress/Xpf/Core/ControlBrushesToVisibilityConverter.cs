namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class ControlBrushesToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Control control = value as Control;
            return (this.Invert ? (((control == null) || ((control.Background == null) && (control.BorderBrush == null))) ? Visibility.Visible : Visibility.Collapsed) : (((control == null) || ((control.Background == null) && (control.BorderBrush == null))) ? Visibility.Collapsed : Visibility.Visible));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            null;

        public bool Invert { get; set; }
    }
}

