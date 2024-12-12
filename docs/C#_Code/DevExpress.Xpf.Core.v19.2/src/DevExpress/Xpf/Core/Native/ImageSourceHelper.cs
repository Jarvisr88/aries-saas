namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Media;

    public static class ImageSourceHelper
    {
        [ThreadStatic]
        private static bool isLoaded;
        [ThreadStatic]
        private static Dictionary<Uri, ImageSource> cache;

        public static ImageSource GetImageSource(Stream stream);
        public static ImageSource GetImageSource(string uri);
        public static ImageSource GetImageSource(Uri uri);
        public static void RegisterPackScheme();

        private static Dictionary<Uri, ImageSource> Cache { get; }
    }
}

