namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class BoolToThicknessConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((bool) value) ? this.ThicknessTrue : this.ThicknessFalse;

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public Thickness ThicknessFalse { get; set; }

        public Thickness ThicknessTrue { get; set; }
    }
}

