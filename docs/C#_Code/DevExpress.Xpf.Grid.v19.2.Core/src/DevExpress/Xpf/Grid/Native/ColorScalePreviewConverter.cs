namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Media;

    public class ColorScalePreviewConverter : MarkupExtension, IValueConverter
    {
        private static Point startPoint = new Point(0.0, 0.5);
        private static Point endPoint = new Point(1.0, 0.5);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ColorScaleFormat format = value as ColorScaleFormat;
            if (format == null)
            {
                return null;
            }
            GradientStopCollection stops = new GradientStopCollection {
                new GradientStop(format.ColorMin, 0.0)
            };
            if (format.ColorMiddle != null)
            {
                stops.Add(new GradientStop(format.ColorMiddle.Value, 0.5));
            }
            stops.Add(new GradientStop(format.ColorMax, 1.0));
            LinearGradientBrush brush1 = new LinearGradientBrush();
            brush1.StartPoint = startPoint;
            brush1.EndPoint = endPoint;
            brush1.GradientStops = stops;
            return brush1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            null;

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

