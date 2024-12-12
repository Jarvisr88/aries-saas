namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;

    public static class ImageRepository
    {
        private static Dictionary<string, Dictionary<string, ImageSource>> items;

        static ImageRepository();
        public static void Clear(string psId);
        public static ImageSource GetImageSource(string psId, string imageId);
        public static void RegisterImageSource(string psId, string imageId, ImageSource imageSource);
    }
}

