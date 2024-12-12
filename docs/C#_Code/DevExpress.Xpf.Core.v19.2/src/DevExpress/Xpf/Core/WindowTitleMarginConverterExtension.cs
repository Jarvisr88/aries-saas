namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class WindowTitleMarginConverterExtension : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value[0] == null)
            {
                return new Thickness();
            }
            Thickness thickness = new Thickness();
            Thickness thickness2 = new Thickness();
            if (value[0] is Thickness)
            {
                thickness = (Thickness) value[0];
            }
            if (value[1] is Thickness)
            {
                thickness2 = (Thickness) value[1];
            }
            thickness.Left = thickness2.Left;
            return thickness;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

