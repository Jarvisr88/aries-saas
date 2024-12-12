namespace DevExpress.Pdf.Native
{
    using DevExpress.DirectX.Common.WIC;
    using DevExpress.DirectX.NativeInterop;
    using DevExpress.DirectX.NativeInterop.WIC;
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.IO;

    public class PdfWICJpegScanlineSource : IPdfImageScanlineSource, IDisposable
    {
        private WICBitmapSource bitmap;
        private readonly int width;
        private readonly int componentsCount;
        private readonly bool isYCCK;
        private int column;

        protected PdfWICJpegScanlineSource(PdfImage image, WICBitmapSource bitmap, bool isYCCK)
        {
            this.width = image.Width;
            this.componentsCount = image.ColorSpace.ComponentsCount;
            this.bitmap = bitmap;
            this.isYCCK = isYCCK;
        }

        public static IPdfImageScanlineSource CreateScanlineSource(PdfImage image, byte[] data)
        {
            try
            {
                return CreateScanlineSourceCore(image, data);
            }
            catch
            {
                return CreateScanlineSourceCore(image, PdfDCTDataValidator.ChangeImageHeight(data, image.Height));
            }
        }

        private static IPdfImageScanlineSource CreateScanlineSource(PdfImage image, WICBitmapSource bitmap, bool isYCCK) => 
            (image.ColorKeyMask == null) ? new PdfWICJpegScanlineSource(image, bitmap, isYCCK) : new PdfWICJpegColorKeyedScanlineSource(image, bitmap, isYCCK);

        private static IPdfImageScanlineSource CreateScanlineSourceCore(PdfImage image, byte[] data)
        {
            IPdfImageScanlineSource source;
            using (NativeStream stream = NativeStream.Create(data))
            {
                using (WICBitmapDecoder decoder = WICImagingFactory.Instance.CreateJPEGDecoder())
                {
                    decoder.Initialize(stream, WICDecodeOptions.DecodeMetadataCacheOnDemand);
                    WICBitmapDecodeFrame bitmapSource = decoder.GetFrame(0);
                    Guid pixelFormat = bitmapSource.GetPixelFormat();
                    if (pixelFormat == WICPixelFormats.PixelFormat24bppBGR)
                    {
                        WICFormatConverter bitmap = WICImagingFactory.Instance.CreateFormatConverter();
                        bitmap.Initialize(bitmapSource, WICPixelFormats.PixelFormat24bppRGB);
                        bitmapSource.Dispose();
                        source = CreateScanlineSource(image, bitmap, false);
                    }
                    else if (!(pixelFormat == WICPixelFormats.PixelFormat32bppCMYK))
                    {
                        source = CreateScanlineSource(image, bitmapSource, false);
                    }
                    else
                    {
                        using (MemoryStream stream2 = new MemoryStream(data))
                        {
                            source = CreateScanlineSource(image, bitmapSource, PdfRecognizedImageInfo.DetectImage(stream2).Type == PdfRecognizedImageFormat.YCCKJpeg);
                        }
                    }
                }
            }
            return source;
        }

        public void Dispose()
        {
            this.bitmap.Dispose();
        }

        public virtual void FillNextScanline(byte[] scanlineData)
        {
            int column = this.column;
            this.column = column + 1;
            this.bitmap.CopyPixels(new WICRect(0, column, this.width, 1), scanlineData.Length, scanlineData);
            if (this.isYCCK)
            {
                for (int i = 0; i < (this.width * this.componentsCount); i++)
                {
                    scanlineData[i] = (byte) (0xff - scanlineData[i]);
                }
            }
        }

        public virtual int ComponentsCount =>
            this.componentsCount;

        public virtual bool HasAlpha =>
            false;
    }
}

