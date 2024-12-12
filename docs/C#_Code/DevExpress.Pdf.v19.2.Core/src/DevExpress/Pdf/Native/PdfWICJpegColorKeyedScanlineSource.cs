namespace DevExpress.Pdf.Native
{
    using DevExpress.DirectX.NativeInterop.WIC;
    using DevExpress.Pdf;
    using System;

    public class PdfWICJpegColorKeyedScanlineSource : PdfWICJpegScanlineSource
    {
        private readonly byte[] buffer;
        private readonly PdfByteAlignedImageScanlineDecoder decoder;

        public PdfWICJpegColorKeyedScanlineSource(PdfImage image, WICBitmapSource bitmap, bool isYCCK) : base(image, bitmap, isYCCK)
        {
            int componentsCount = image.ColorSpace.ComponentsCount;
            this.decoder = new PdfByteAlignedImageScanlineDecoder(image, componentsCount, 1);
            this.buffer = new byte[image.Width * componentsCount];
        }

        public override void FillNextScanline(byte[] scanlineData)
        {
            base.FillNextScanline(this.buffer);
            this.decoder.FillNextScanline(scanlineData, this.buffer, 0);
        }

        public override int ComponentsCount =>
            base.ComponentsCount + 1;

        public override bool HasAlpha =>
            true;
    }
}

