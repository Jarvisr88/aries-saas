namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.XtraPrinting;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class ImageSizeModeToImageSourceConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSizeMode? nullable = value as ImageSizeMode?;
            if (nullable == null)
            {
                throw new InvalidOperationException();
            }
            string format = "pack://application:,,,/{0};component/Images/ImageEditItems/SizeMode{1}.svg";
            string str2 = null;
            switch (nullable.Value)
            {
                case ImageSizeMode.Normal:
                case ImageSizeMode.StretchImage:
                case ImageSizeMode.ZoomImage:
                case ImageSizeMode.Squeeze:
                case ImageSizeMode.Tile:
                {
                    str2 = nullable.Value.ToString();
                    SvgImageSourceExtension extension1 = new SvgImageSourceExtension();
                    extension1.Uri = new Uri(string.Format(format, "DevExpress.Xpf.Printing.v19.2", str2));
                    return extension1.ProvideValue(null);
                }
            }
            throw new InvalidOperationException();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

