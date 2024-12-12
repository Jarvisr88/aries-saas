namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class BitmapSourceBuilder : ImagesSourceBuilder<BitmapSource>
    {
        internal BitmapSourceBuilder(BitmapSource source) : base(source)
        {
        }

        public static Bitmap CreateBitmap(BitmapSource source)
        {
            BitmapSource actualSource = GetActualSource(source);
            int stride = Math.Max(4, (actualSource.PixelWidth * actualSource.Format.BitsPerPixel) / 8);
            while ((stride % 4) > 0)
            {
                stride++;
            }
            Bitmap bitmap = new Bitmap(actualSource.PixelWidth, actualSource.PixelHeight);
            BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, actualSource.PixelWidth, actualSource.PixelHeight), ImageLockMode.WriteOnly, GetActualTargetFormat(actualSource));
            actualSource.CopyPixels(new Int32Rect(0, 0, actualSource.PixelWidth, actualSource.PixelHeight), bitmapdata.Scan0, stride * actualSource.PixelHeight, stride);
            bitmap.UnlockBits(bitmapdata);
            return bitmap;
        }

        public override Bitmap CreateImage() => 
            CreateBitmap(base.Source);

        public override void GenerateData(PdfGraphicsCommandConstructor constructor)
        {
            Bitmap image = CreateBitmap(base.Source);
            PdfXObjectCachedResource cachedResource = constructor.ImageCache.AddXObject(image);
            constructor.DrawXObject(cachedResource, new PointF(0f, 0f));
        }

        private static BitmapSource GetActualSource(BitmapSource source)
        {
            System.Windows.Media.PixelFormat[] formatArray1 = new System.Windows.Media.PixelFormat[] { PixelFormats.Gray8, PixelFormats.Gray4, PixelFormats.Gray2, PixelFormats.Indexed2, PixelFormats.Indexed4, PixelFormats.Indexed8 };
            return (!formatArray1.Contains<System.Windows.Media.PixelFormat>(source.Format) ? source : new FormatConvertedBitmap(source, PixelFormats.Pbgra32, null, 0.0));
        }

        private static System.Drawing.Imaging.PixelFormat GetActualTargetFormat(BitmapSource source) => 
            ((source.Format == PixelFormats.BlackWhite) || (source.Format == PixelFormats.Indexed1)) ? System.Drawing.Imaging.PixelFormat.Format1bppIndexed : System.Drawing.Imaging.PixelFormat.Format32bppArgb;
    }
}

