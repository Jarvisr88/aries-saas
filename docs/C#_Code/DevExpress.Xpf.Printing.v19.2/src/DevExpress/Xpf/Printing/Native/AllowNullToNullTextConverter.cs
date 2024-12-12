namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class AllowNullToNullTextConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture) => 
            ((value == null) || (value == DependencyProperty.UnsetValue)) ? null : (!((bool) value) ? string.Empty : EditorLocalizer.GetString(EditorStringId.EmptyItem));

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

