namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class TrackBarZoomValueConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double num = System.Convert.ToDouble(value);
            return ((num <= 1.0) ? ((num - this.MinZoomValue) / (1.0 - this.MinZoomValue)) : (1.0 + ((num - 1.0) / (this.MaxZoomValue - 1.0))));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 1.0;
            }
            double num = System.Convert.ToDouble(value);
            return ((num <= 1.0) ? (this.MinZoomValue + (num * (1.0 - this.MinZoomValue))) : (1.0 + ((num - 1.0) * (this.MaxZoomValue - 1.0))));
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        public double MinZoomValue { get; set; }

        public double MaxZoomValue { get; set; }
    }
}

