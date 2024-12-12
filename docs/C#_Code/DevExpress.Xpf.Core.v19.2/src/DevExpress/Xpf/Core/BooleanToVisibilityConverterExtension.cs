namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class BooleanToVisibilityConverterExtension : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility collapsed = Visibility.Collapsed;
            if ((parameter is string) && (((string) parameter) == "HiddenOnFalse"))
            {
                collapsed = Visibility.Hidden;
            }
            return ((value != null) ? (!((bool) value) ? (this.Invert ? ((object) 0) : ((object) collapsed)) : (this.Invert ? ((object) collapsed) : ((object) 0))) : (this.Invert ? ((object) 0) : ((object) collapsed)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            (((Visibility) value) != Visibility.Collapsed) ? !this.Invert : this.Invert;

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        public bool Invert { get; set; }
    }
}

