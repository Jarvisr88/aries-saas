namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class WindowBorderThicknessConverterExtension : MarkupExtension, IValueConverter
    {
        private readonly Thickness maximizedWindowMargin = new Thickness(0.0, 0.0, 0.0, 1.0);
        private static WindowBorderThicknessConverterExtension converter;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value != null) && (value != DependencyProperty.UnsetValue))
            {
                return new Thickness(this.maximizedWindowMargin.Left + ((Thickness) value).Left, this.maximizedWindowMargin.Top + ((Thickness) value).Top, this.maximizedWindowMargin.Right + ((Thickness) value).Right, this.maximizedWindowMargin.Bottom + ((Thickness) value).Bottom);
            }
            return new Thickness();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            converter ??= new WindowBorderThicknessConverterExtension();
    }
}

