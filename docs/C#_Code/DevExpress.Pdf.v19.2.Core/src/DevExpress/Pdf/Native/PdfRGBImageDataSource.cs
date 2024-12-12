namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfRGBImageDataSource : PdfImageDataSource
    {
        private readonly int stride;

        public PdfRGBImageDataSource(IPdfImageScanlineSource source, int width, int stride) : base(source, width)
        {
            this.stride = stride;
        }

        public override void FillBuffer(byte[] buffer, int scanlineCount)
        {
            int count = base.Width * this.ComponentsCount;
            int num2 = 0;
            for (int i = 0; num2 < scanlineCount; i += this.stride)
            {
                Buffer.BlockCopy(base.GetNextSourceScanline(), 0, buffer, i, count);
                num2++;
            }
        }

        public override int ComponentsCount =>
            base.SourceComponentsCount;
    }
}

