namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfImageDataRasterCacheItem : PdfImageDataCacheItem
    {
        private readonly byte[] data;

        public PdfImageDataRasterCacheItem(PdfImageData imageData, PdfImage image, byte[] data) : base(imageData, image)
        {
            this.data = data;
        }

        public override void DrawImage(PdfLegacyCommandInterpreter interpreter)
        {
            interpreter.DrawImage(this, this.data);
        }
    }
}

