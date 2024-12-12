namespace DevExpress.Pdf.Native
{
    using DevExpress.DirectX.Common.WIC;
    using DevExpress.DirectX.NativeInterop.WIC;
    using System;
    using System.Security;

    public class PdfWICImageInterpolator : IPdfImageScanlineSource, IDisposable
    {
        private readonly PdfWICBitmapSource source;
        private readonly int targetWidth;
        private readonly IPdfImageScanlineSource scanlineSource;
        private WICBitmapScaler scaler;
        private WICFormatConverter formatConverter;
        private int currentY;

        [SecuritySafeCritical]
        public PdfWICImageInterpolator(IPdfImageScanlineSource scanlineSource, int targetWidth, int targetHeight, int sourceWidth, int sourceHeight, WICImagingFactory factory, bool shouldInterpolate)
        {
            this.targetWidth = targetWidth;
            this.scanlineSource = scanlineSource;
            int componentsCount = scanlineSource.ComponentsCount;
            Guid pixelFormat = (componentsCount == 1) ? WICPixelFormats.PixelFormat8bppGray : ((componentsCount == 4) ? WICPixelFormats.PixelFormat32bppRGBA : WICPixelFormats.PixelFormat24bppRGB);
            this.source = new PdfWICBitmapSource(scanlineSource, pixelFormat, sourceWidth, sourceHeight);
            this.scaler = factory.CreateBitmapScaler();
            this.scaler.Initialize(this.source, targetWidth, targetHeight, shouldInterpolate ? WICBitmapInterpolationMode.Fant : WICBitmapInterpolationMode.NearestNeighbor);
            if (this.scaler.GetPixelFormat() != pixelFormat)
            {
                this.formatConverter = factory.CreateFormatConverter();
                this.formatConverter.Initialize(this.scaler, pixelFormat);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [SecuritySafeCritical]
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.scaler == null)
                {
                    WICBitmapScaler scaler = this.scaler;
                }
                else
                {
                    this.scaler.Dispose();
                }
                if (this.formatConverter == null)
                {
                    WICFormatConverter formatConverter = this.formatConverter;
                }
                else
                {
                    this.formatConverter.Dispose();
                }
                this.source.Dispose();
            }
        }

        [SecuritySafeCritical]
        public void FillNextScanline(byte[] scanlineData)
        {
            int currentY = this.currentY;
            this.currentY = currentY + 1;
            WICRect rect = new WICRect(0, currentY, this.targetWidth, 1);
            if (this.formatConverter != null)
            {
                this.formatConverter.CopyPixels(rect, this.targetWidth * this.ComponentsCount, scanlineData);
            }
            else
            {
                this.scaler.CopyPixels(rect, this.targetWidth * this.ComponentsCount, scanlineData);
            }
        }

        ~PdfWICImageInterpolator()
        {
            this.Dispose(false);
        }

        public int ComponentsCount =>
            this.scanlineSource.ComponentsCount;

        public bool HasAlpha =>
            this.scanlineSource.HasAlpha;
    }
}

