namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfInvertedImageScanlineSource : IPdfImageScanlineSource, IDisposable
    {
        private readonly IPdfImageScanlineSource source;

        public PdfInvertedImageScanlineSource(IPdfImageScanlineSource source)
        {
            this.source = source;
        }

        public void Dispose()
        {
            this.source.Dispose();
        }

        public void FillNextScanline(byte[] scanline)
        {
            this.source.FillNextScanline(scanline);
            int length = scanline.Length;
            for (int i = 0; i < length; i++)
            {
                scanline[i] = (byte) (0xff ^ scanline[i]);
            }
        }

        public int ComponentsCount =>
            this.source.ComponentsCount;

        public bool HasAlpha =>
            this.source.HasAlpha;
    }
}

