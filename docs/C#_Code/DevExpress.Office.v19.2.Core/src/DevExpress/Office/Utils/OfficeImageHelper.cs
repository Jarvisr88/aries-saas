namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing.Imaging;

    public static class OfficeImageHelper
    {
        public static ImageFormat GetImageFormat(OfficeImageFormat rawFormat)
        {
            switch (rawFormat)
            {
                case OfficeImageFormat.Bmp:
                    return ImageFormat.Bmp;

                case OfficeImageFormat.Emf:
                    return ImageFormat.Emf;

                case OfficeImageFormat.Exif:
                    return ImageFormat.Exif;

                case OfficeImageFormat.Gif:
                    return ImageFormat.Gif;

                case OfficeImageFormat.Icon:
                    return ImageFormat.Icon;

                case OfficeImageFormat.Jpeg:
                    return ImageFormat.Jpeg;

                case OfficeImageFormat.MemoryBmp:
                    return ImageFormat.MemoryBmp;

                case OfficeImageFormat.Png:
                    return ImageFormat.Png;

                case OfficeImageFormat.Tiff:
                    return ImageFormat.Tiff;

                case OfficeImageFormat.Wmf:
                    return ImageFormat.Wmf;
            }
            return null;
        }

        public static OfficeImageFormat GetRtfImageFormat(ImageFormat rawFormat) => 
            !Equals(rawFormat, ImageFormat.Bmp) ? (!Equals(rawFormat, ImageFormat.Emf) ? (!Equals(rawFormat, ImageFormat.Exif) ? (!Equals(rawFormat, ImageFormat.Gif) ? (!Equals(rawFormat, ImageFormat.Icon) ? (!Equals(rawFormat, ImageFormat.Jpeg) ? (!Equals(rawFormat, ImageFormat.MemoryBmp) ? (!Equals(rawFormat, ImageFormat.Png) ? (!Equals(rawFormat, ImageFormat.Tiff) ? (!Equals(rawFormat, ImageFormat.Wmf) ? OfficeImageFormat.None : OfficeImageFormat.Wmf) : OfficeImageFormat.Tiff) : OfficeImageFormat.Png) : OfficeImageFormat.MemoryBmp) : OfficeImageFormat.Jpeg) : OfficeImageFormat.Icon) : OfficeImageFormat.Gif) : OfficeImageFormat.Exif) : OfficeImageFormat.Emf) : OfficeImageFormat.Bmp;
    }
}

