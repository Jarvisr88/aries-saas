namespace DevExpress.Xpf
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Media.Imaging;

    public class EmbeddedResourceAboutImageConverter : MarkupExtension, IValueConverter
    {
        public static BitmapImage CreateImageFromStream(Stream stream)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = stream;
            image.EndInit();
            image.Freeze();
            return image;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = Convert.ToString(value);
            return CreateImageFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(name));
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

