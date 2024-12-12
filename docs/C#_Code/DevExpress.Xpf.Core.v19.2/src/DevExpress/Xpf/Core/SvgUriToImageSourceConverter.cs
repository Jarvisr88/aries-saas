namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class SvgUriToImageSourceConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri uri1;
            SvgImageSourceExtension extension = new SvgImageSourceExtension();
            if (!double.IsNaN(this.SvgWidth) && ((this.SvgWidth > 0.0) && (!double.IsNaN(this.SvgHeight) && (this.SvgHeight > 0.0))))
            {
                extension.Size = new Size(this.SvgWidth, this.SvgHeight);
            }
            if (value is string)
            {
                uri1 = new Uri((string) value);
            }
            else
            {
                uri1 = extension.Uri = (Uri) value;
            }
            extension.Uri = uri1;
            return extension.ProvideValue(null);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public double SvgWidth { get; set; }

        public double SvgHeight { get; set; }
    }
}

