namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfDecodeImageScanlineSource : PdfImageScanlineSourceDecorator
    {
        private const double factor = 0.00392156862745098;
        private readonly IList<PdfRange> decode;

        public PdfDecodeImageScanlineSource(IList<PdfRange> decode, int imageWidth, IPdfImageScanlineSource source) : base(source, imageWidth)
        {
            this.decode = decode;
        }

        public override void FillNextScanline(byte[] scanline)
        {
            base.Source.FillNextScanline(scanline);
            int count = this.decode.Count;
            int sourceWidth = base.SourceWidth;
            int num3 = base.Source.ComponentsCount - count;
            int num4 = 0;
            int index = 0;
            while (num4 < sourceWidth)
            {
                int num6 = 0;
                while (true)
                {
                    if (num6 >= count)
                    {
                        num4++;
                        index += num3;
                        break;
                    }
                    PdfRange range = this.decode[num6];
                    double min = range.Min;
                    double max = range.Max;
                    double num9 = min + ((scanline[index] * (max - min)) * 0.00392156862745098);
                    scanline[index] = PdfMathUtils.ToByte(num9 * 255.0);
                    num6++;
                    index++;
                }
            }
        }
    }
}

