namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public static class ImageHelper
    {
        public static BitmapImage CreateImageFromCoreEmbeddedResource(string resource);
        public static BitmapImage CreateImageFromEmbeddedResource(Assembly assembly, string resource);
        public static BitmapImage CreateImageFromStream(Stream stream);
        public static ImageSource CreateImageSource(Image img);
        public static Image CreatePlatformImage(Image img);
        private static ImageCodecInfo FindImageCodec(ImageFormat imageFormat);
        public static void UpdateBaseUri(DependencyObject d, ImageSource source);
    }
}

