namespace DevExpress.Office.Extensions
{
    using DevExpress.Office;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public static class ImageExtensions
    {
        private static float defaultTargetDpi = 220f;

        public static ImageSource ToImageSource(this Image image) => 
            image.ToImageSource(defaultTargetDpi);

        public static ImageSource ToImageSource(this Image image, bool scaleToScreenDpi)
        {
            MemoryStream stream = new MemoryStream();
            if (!scaleToScreenDpi)
            {
                try
                {
                    image.Save(stream, ImageFormat.Png);
                }
                catch
                {
                    stream.Dispose();
                    stream = new MemoryStream();
                    image.Save(stream, image.RawFormat);
                }
            }
            else
            {
                int width = (int) (((double) (image.Width * DocumentModelDpi.Dpi)) / 96.0);
                int height = (int) (((double) (image.Height * DocumentModelDpi.Dpi)) / 96.0);
                using (Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                {
                    bitmap.SetResolution(96f, 96f);
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.DrawImage(image, 0, 0, width, height);
                        bitmap.Save(stream, ImageFormat.Png);
                    }
                }
            }
            stream.Position = 0L;
            BitmapImage image2 = new BitmapImage();
            image2.BeginInit();
            image2.StreamSource = stream;
            image2.EndInit();
            return image2;
        }

        public static ImageSource ToImageSource(this Image image, float targetDpi)
        {
            System.Windows.Size targetSize = new System.Windows.Size((double) ((image.Width * targetDpi) / image.HorizontalResolution), (double) ((image.Height * targetDpi) / image.VerticalResolution));
            return image.ToImageSource(targetSize);
        }

        public static ImageSource ToImageSource(this Image image, System.Windows.Size targetSize)
        {
            MemoryStream stream = new MemoryStream();
            Metafile metafile = image as Metafile;
            if (metafile == null)
            {
                try
                {
                    image.Save(stream, ImageFormat.Png);
                }
                catch
                {
                    stream.Dispose();
                    stream = new MemoryStream();
                    image.Save(stream, image.RawFormat);
                }
            }
            else
            {
                int width = (int) ((targetSize.Width * DocumentModelDpi.Dpi) / 96.0);
                int height = (int) ((targetSize.Height * DocumentModelDpi.Dpi) / 96.0);
                using (Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.DrawImage(metafile, 0, 0, width, height);
                        bitmap.Save(stream, ImageFormat.Png);
                    }
                }
            }
            stream.Position = 0L;
            BitmapImage image2 = new BitmapImage();
            image2.BeginInit();
            image2.StreamSource = stream;
            image2.EndInit();
            return image2;
        }
    }
}

