namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    [ValueConversion(typeof(double), typeof(GridLength))]
    public class DoubleToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            new GridLength(MathHelper.CalcRealDimension((double) value), GridUnitType.Pixel);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((GridLength) value).Value;
    }
}

