namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    [ValueConversion(typeof(Size), typeof(double))]
    internal abstract class SizeConverter : IValueConverter
    {
        private bool IsWidth;
        private double NaNResult;

        protected SizeConverter(bool isWidth, double nanResult)
        {
            this.IsWidth = isWidth;
            this.NaNResult = nanResult;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Size size = (Size) value;
            double d = this.IsWidth ? size.Width : size.Height;
            return (double.IsNaN(d) ? this.NaNResult : d);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

