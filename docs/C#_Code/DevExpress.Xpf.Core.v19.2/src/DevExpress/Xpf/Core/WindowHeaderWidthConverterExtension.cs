namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class WindowHeaderWidthConverterExtension : MarkupExtension, IMultiValueConverter
    {
        private static WindowHeaderWidthConverterExtension converter;

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            double num = 0.0;
            Thickness thickness = new Thickness();
            if (value[0] != DependencyProperty.UnsetValue)
            {
                num = (double) value[0];
            }
            if (value[1] != DependencyProperty.UnsetValue)
            {
                thickness = (Thickness) value[1];
            }
            return Math.Max((num + thickness.Left) + thickness.Right, SystemParameters.MinimumWindowWidth);
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            converter ??= new WindowHeaderWidthConverterExtension();
    }
}

