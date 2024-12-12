namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class TouchValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (!(value is DependencyObject) || !ThemeHelper.IsTouchTheme((DependencyObject) value)) ? this.NormalValue : this.TouchValue;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            null;

        public object NormalValue { get; set; }

        public object TouchValue { get; set; }
    }
}

