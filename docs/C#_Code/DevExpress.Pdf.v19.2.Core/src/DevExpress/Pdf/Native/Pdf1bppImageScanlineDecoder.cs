namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class Pdf1bppImageScanlineDecoder : PdfImageScanlineDecoder
    {
        private readonly int alignedStride;

        public Pdf1bppImageScanlineDecoder(PdfImage image) : base(image, 1)
        {
            this.alignedStride = (int) Math.Ceiling((double) (((float) base.Width) / 8f));
        }

        public override void FillNextScanline(byte[] scanline, byte[] sourceData, int sourceOffset)
        {
            int width = base.Width;
            int index = 0;
            int num3 = 7;
            while (index < width)
            {
                int num4 = (sourceData[sourceOffset] >> (num3 & 0x1f)) & 1;
                scanline[index] = (byte) (num4 * 0xff);
                if (num3 == 0)
                {
                    num3 = 8;
                    sourceOffset++;
                    while ((index + 8) < width)
                    {
                        byte num5 = sourceData[sourceOffset];
                        if ((num5 != 0) && (num5 != 0xff))
                        {
                            break;
                        }
                        int num6 = index + 8;
                        while (true)
                        {
                            if (index >= num6)
                            {
                                sourceOffset++;
                                break;
                            }
                            scanline[++index] = num5;
                        }
                    }
                }
                num3--;
                index++;
            }
        }

        public override int Stride =>
            this.alignedStride;
    }
}

