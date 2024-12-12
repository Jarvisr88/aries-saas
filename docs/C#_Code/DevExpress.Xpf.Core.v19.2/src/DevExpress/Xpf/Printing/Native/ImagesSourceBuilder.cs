namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Drawing;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public abstract class ImagesSourceBuilder
    {
        protected ImagesSourceBuilder()
        {
        }

        public static ImagesSourceBuilder Create(ImageSource source)
        {
            if (source == null)
            {
                return null;
            }
            if (source is BitmapSource)
            {
                return new BitmapSourceBuilder((BitmapSource) source);
            }
            if (source is D3DImage)
            {
                return new D3DImageBuilder((D3DImage) source);
            }
            if (!(source is DrawingImage))
            {
                throw new Exception("Unknow source");
            }
            return new DrawingImageBuilder((DrawingImage) source);
        }

        public abstract Bitmap CreateImage();
        public abstract void GenerateData(PdfGraphicsCommandConstructor constructor);
    }
}

