namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfByteAlignedImageScanlineDecoder : PdfImageScanlineDecoder
    {
        private readonly int step;

        public PdfByteAlignedImageScanlineDecoder(PdfImage image, int componentsCount, int step) : base(image, componentsCount)
        {
            this.step = step;
        }

        public override void FillNextScanline(byte[] scanline, byte[] sourceData, int sourceOffset)
        {
            int width = base.Width;
            bool isColorKeyPresent = base.IsColorKeyPresent;
            int componentsCount = base.ComponentsCount;
            IList<PdfRange> colorKey = base.ColorKey;
            int num3 = 0;
            int num4 = 0;
            while (num3 < width)
            {
                bool flag2 = isColorKeyPresent;
                int num5 = 0;
                while (true)
                {
                    if (num5 >= componentsCount)
                    {
                        if (isColorKeyPresent)
                        {
                            scanline[num4++] = flag2 ? ((byte) 0) : ((byte) 0xff);
                        }
                        num3++;
                        break;
                    }
                    byte num6 = sourceData[sourceOffset];
                    flag2 &= isColorKeyPresent && colorKey[num5].Contains(num6);
                    scanline[num4++] = num6;
                    sourceOffset += this.step;
                    num5++;
                }
            }
        }

        public override int Stride =>
            (base.Width * base.ComponentsCount) * this.step;
    }
}

