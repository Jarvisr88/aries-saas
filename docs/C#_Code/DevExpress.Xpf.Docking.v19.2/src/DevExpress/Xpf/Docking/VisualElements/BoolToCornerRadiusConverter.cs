namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    [Obsolete, ValueConversion(typeof(bool), typeof(CornerRadius))]
    public class BoolToCornerRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((bool) value) ? this.TrueValue : this.FalseValue;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;

        public CornerRadius FalseValue { get; set; }

        public CornerRadius TrueValue { get; set; }
    }
}

