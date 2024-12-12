namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfScanlineTransformationResult
    {
        private readonly IPdfImageScanlineSource scanlineSource;
        private readonly PdfPixelFormat pixelFormat;

        public PdfScanlineTransformationResult(IPdfImageScanlineSource scanlineSource) : this(scanlineSource, PdfPixelFormat.Argb24bpp)
        {
        }

        public PdfScanlineTransformationResult(IPdfImageScanlineSource scanlineSource, PdfPixelFormat pixelFormat)
        {
            this.scanlineSource = scanlineSource;
            this.pixelFormat = pixelFormat;
        }

        public IPdfImageScanlineSource ScanlineSource =>
            this.scanlineSource;

        public PdfPixelFormat PixelFormat =>
            this.pixelFormat;
    }
}

