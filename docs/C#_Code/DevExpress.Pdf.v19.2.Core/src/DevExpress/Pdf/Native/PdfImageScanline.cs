namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfImageScanline
    {
        private readonly byte[] scanlineData;
        private readonly byte[] maskData;

        public PdfImageScanline(int width, int componentsCount)
        {
            this.scanlineData = new byte[width * componentsCount];
            this.maskData = new byte[width];
        }

        public byte[] ScanlineData =>
            this.scanlineData;

        public byte[] MaskData =>
            this.maskData;
    }
}

