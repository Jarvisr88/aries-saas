namespace DevExpress.Utils
{
    using DevExpress.Utils.Zip;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;

    public class HtmlImageHelper
    {
        private static readonly Dictionary<Guid, string> mimeHT;

        static HtmlImageHelper()
        {
            Dictionary<Guid, string> dictionary1 = new Dictionary<Guid, string>();
            dictionary1.Add(GetKey(ImageFormat.Bmp), "bmp");
            dictionary1.Add(GetKey(ImageFormat.Gif), "gif");
            dictionary1.Add(GetKey(ImageFormat.Jpeg), "jpeg");
            dictionary1.Add(GetKey(ImageFormat.Png), "png");
            dictionary1.Add(GetKey(ImageFormat.Tiff), "tiff");
            mimeHT = dictionary1;
        }

        private static ImageFormat GetImageFormat(Image img) => 
            !(img is Metafile) ? ((GetValue(img.RawFormat) != null) ? img.RawFormat : ImageFormat.Png) : ImageFormat.Emf;

        public static long GetImageHashCode(Image img) => 
            (img != null) ? Adler32.Calculate(ImageToArray(img)) : 0L;

        private static Guid GetKey(ImageFormat format) => 
            format.Guid;

        public static string GetMimeType(Image img) => 
            GetValue(img.RawFormat) ?? "png";

        private static string GetValue(ImageFormat format)
        {
            string str;
            mimeHT.TryGetValue(GetKey(format), out str);
            return str;
        }

        public static byte[] ImageToArray(Image img) => 
            PSConvert.ImageToArray(img, GetImageFormat(img));

        public static void SaveImage(Image image, string path)
        {
            image.Save(path, GetImageFormat(image));
        }
    }
}

