namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfLabColorSpaceImageScanlineSource : PdfCIEBasedImageScanlineSource
    {
        private readonly IList<PdfRange> decode;

        public PdfLabColorSpaceImageScanlineSource(IPdfImageScanlineSource source, PdfColorSpace colorSpace, IList<PdfRange> decode, int width) : base(source, colorSpace, width, 3)
        {
            this.decode = decode;
        }

        protected override void Decode(double[] pixelBuffer, byte[] data, int offset)
        {
            for (int i = 0; i < 3; i++)
            {
                PdfRange range = this.decode[i];
                double min = range.Min;
                double max = range.Max;
                double num4 = min + ((data[offset++] * (max - min)) / 255.0);
                pixelBuffer[i] = (num4 >= min) ? ((num4 <= max) ? num4 : max) : min;
            }
        }
    }
}

