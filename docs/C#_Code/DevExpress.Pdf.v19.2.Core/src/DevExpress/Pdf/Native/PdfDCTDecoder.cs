namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security;

    public class PdfDCTDecoder
    {
        private static readonly IPdfDCTDecoderFactory decoderFactory;
        private readonly byte[] imageData;
        private readonly int imageWidth;
        private readonly int imageHeight;

        static PdfDCTDecoder()
        {
            if (PdfRenderingSettings.UseExternalDctDecoder)
            {
                decoderFactory = new PdfWICDCTDecoderFactory();
            }
            else
            {
                decoderFactory = new PdfGDIPlusDCTDecoderFactory();
            }
        }

        private PdfDCTDecoder(byte[] imageData, int imageWidth, int imageHeight)
        {
            this.imageData = imageData;
            this.imageWidth = imageWidth;
            this.imageHeight = imageHeight;
        }

        private Bitmap CreateBitmap()
        {
            try
            {
                return new Bitmap(new MemoryStream(this.imageData));
            }
            catch (ArgumentException)
            {
                return new Bitmap(new MemoryStream(PdfDCTDataValidator.ChangeImageHeight(this.imageData, this.imageHeight)));
            }
        }

        public static IPdfImageScanlineSource CreateScanlineSource(byte[] imageData, PdfImage image, int componentsCount) => 
            decoderFactory.CreateSource(imageData, image);

        [SecuritySafeCritical]
        private PdfDCTDecodeResult Decode()
        {
            using (Bitmap bitmap = this.CreateBitmap())
            {
                int stride;
                byte[] buffer;
                PixelFormat pixelFormat = bitmap.PixelFormat;
                BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, this.imageWidth, this.imageHeight), ImageLockMode.ReadOnly, pixelFormat);
                try
                {
                    stride = bitmapdata.Stride;
                    int length = stride * this.imageHeight;
                    buffer = new byte[length];
                    Marshal.Copy(bitmapdata.Scan0, buffer, 0, length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapdata);
                }
                return new PdfDCTDecodeResult(buffer, stride);
            }
        }

        public static PdfDCTDecodeResult Decode(byte[] imageData, int imageWidth, int imageHeight) => 
            new PdfDCTDecoder(imageData, imageWidth, imageHeight).Decode();
    }
}

