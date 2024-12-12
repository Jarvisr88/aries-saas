namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public class PdfImageBase : PdfXObject
    {
        public PdfImageBase(string name, bool compressed) : base(name, compressed)
        {
        }

        private static Bitmap ConvertImageTo(Image source, PixelFormat destFormat)
        {
            Bitmap image = new Bitmap(source.Width, source.Height, destFormat);
            image.SetResolution(source.HorizontalResolution, source.VerticalResolution);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.PageUnit = GraphicsUnit.Pixel;
                graphics.DrawImageUnscaled(source, 0, 0);
            }
            return image;
        }

        public static PdfImageBase CreateInstance(IPdfDocumentOwner documentInfo, Image image, PdfDocument document, string name, bool compressed, bool convertImageToJpeg, PdfJpegImageQuality jpegQuality)
        {
            if (Is4bppIndexedFormat(image))
            {
                return new Pdf4bppIndexedImage(image, name, compressed);
            }
            if (Is1bppIndexedFormat(image))
            {
                return new Pdf1bppIndexedImage(image, name, compressed);
            }
            if (IsPixelFormat32bppCMYK(image))
            {
                image = ConvertImageTo(image, PixelFormat.Format24bppRgb);
            }
            if (convertImageToJpeg)
            {
                return new PdfJpegImage(image, name, jpegQuality, compressed);
            }
            if (Is8bppIndexedFormat(image))
            {
                return new Pdf8bppIndexedImage(image, name, compressed);
            }
            if (Is24bppRGBFormat(image))
            {
                return new Pdf24bppRgbImage(image, name, compressed);
            }
            if (image.RawFormat.Equals(ImageFormat.Jpeg))
            {
                return new PdfJpegImage(image, name, jpegQuality, compressed);
            }
            if (Is32bppARGBFormat(image))
            {
                return new Pdf32bppArgbImage(image, name, compressed);
            }
            if ((image is Metafile) && !PdfGraphics.RenderMetafileAsBitmap)
            {
                return new PdfVectorImage(documentInfo, image, document, name, compressed);
            }
            using (Image image2 = BitmapCreator.CreateBitmapWithResolutionLimit(image, DXColor.White))
            {
                return new Pdf32bppArgbImage(image2, name, compressed);
            }
        }

        private static bool Is1bppIndexedFormat(Image image) => 
            (image.PixelFormat == PixelFormat.Format1bppIndexed) || ((image.PixelFormat == PixelFormat.Indexed) && (Image.GetPixelFormatSize(image.PixelFormat) == 1));

        private static bool Is24bppRGBFormat(Image image) => 
            image.PixelFormat == PixelFormat.Format24bppRgb;

        private static bool Is32bppARGBFormat(Image image) => 
            image.PixelFormat == PixelFormat.Format32bppArgb;

        private static bool Is4bppIndexedFormat(Image image) => 
            (image.PixelFormat == PixelFormat.Format4bppIndexed) || ((image.PixelFormat == PixelFormat.Indexed) && (Image.GetPixelFormatSize(image.PixelFormat) == 4));

        private static bool Is8bppIndexedFormat(Image image) => 
            (image.PixelFormat == PixelFormat.Format8bppIndexed) || ((image.PixelFormat == PixelFormat.Indexed) && (Image.GetPixelFormatSize(image.PixelFormat) == 8));

        private static bool IsPixelFormat32bppCMYK(Image image) => 
            image.PixelFormat == ((PixelFormat) 0x200f);

        public virtual Matrix Transform(RectangleF correctedBounds) => 
            new Matrix(correctedBounds.Width, 0f, 0f, correctedBounds.Height, correctedBounds.X, correctedBounds.Y);

        public virtual PdfImageBase MaskImage =>
            null;
    }
}

