namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Windows.Data;
    using System.Windows.Markup;

    internal class ImageToImageSourceConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Image image = value as Image;
            MemoryStream stream = new MemoryStream();
            image.Save(stream, image.RawFormat.Equals(ImageFormat.MemoryBmp) ? ImageFormat.Bmp : image.RawFormat);
            stream.Position = 0L;
            return ImageSourceHelper.GetImageSource(stream);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

