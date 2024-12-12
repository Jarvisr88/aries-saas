namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Media.Media3D;

    public class SpinStyleCameraPositionConverter : MarkupExtension, IMultiValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => 
            ((values[0] == DependencyProperty.UnsetValue) || (values[1] == DependencyProperty.UnsetValue)) ? new Point3D(0.0, 0.0, 0.0) : new Point3D(0.0, 0.0, ((double) values[0]) / (2.0 * Math.Tan((((double) values[1]) * 3.1415926535897931) / 360.0)));

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

