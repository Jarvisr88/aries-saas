namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfNonByteAlignedImageScanlineDecoder : PdfImageScanlineDecoder
    {
        private readonly int bitsPerComponent;
        private readonly int mask;
        private readonly int factor;
        private readonly int alignedStride;

        public PdfNonByteAlignedImageScanlineDecoder(PdfImage image, int componentsCount) : base(image, componentsCount)
        {
            this.bitsPerComponent = image.BitsPerComponent;
            this.mask = (1 << (this.bitsPerComponent & 0x1f)) - 1;
            this.factor = 0xff / this.mask;
            this.alignedStride = (int) Math.Ceiling((double) (((float) ((base.Width * componentsCount) * this.bitsPerComponent)) / 8f));
        }

        public override void FillNextScanline(byte[] scanline, byte[] sourceData, int sourceOffset)
        {
            int num = sourceOffset * 8;
            bool isColorKeyPresent = base.IsColorKeyPresent;
            IList<PdfRange> colorKey = base.ColorKey;
            int width = base.Width;
            int componentsCount = base.ComponentsCount;
            int num4 = 0;
            int num5 = 0;
            while (num4 < width)
            {
                bool flag2 = isColorKeyPresent;
                int num6 = 0;
                while (true)
                {
                    if (num6 >= componentsCount)
                    {
                        if (isColorKeyPresent)
                        {
                            scanline[num5++] = flag2 ? ((byte) 0) : ((byte) 0xff);
                        }
                        num4++;
                        break;
                    }
                    int index = num / 8;
                    int num8 = (8 - this.bitsPerComponent) - (num % 8);
                    int num9 = (sourceData[index] >> (num8 & 0x1f)) & this.mask;
                    flag2 &= isColorKeyPresent && colorKey[num6].Contains(num9);
                    scanline[num5++] = (byte) (num9 * this.factor);
                    num += this.bitsPerComponent;
                    num6++;
                }
            }
        }

        public override int Stride =>
            this.alignedStride;
    }
}

