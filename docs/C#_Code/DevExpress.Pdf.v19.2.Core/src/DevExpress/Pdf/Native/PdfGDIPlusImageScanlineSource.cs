namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfGDIPlusImageScanlineSource : IPdfImageScanlineSource, IDisposable
    {
        private readonly PdfDCTDecodeResult sourceData;
        private readonly int width;
        private readonly int componentsCount;
        private IList<PdfRange> colorKeyMask;
        private int offset;

        public PdfGDIPlusImageScanlineSource(byte[] imageData, PdfImage image)
        {
            this.width = image.Width;
            this.sourceData = PdfDCTDecoder.Decode(imageData, this.width, image.Height);
            this.componentsCount = image.ColorSpace.ComponentsCount;
            this.colorKeyMask = image.ColorKeyMask;
        }

        public void Dispose()
        {
        }

        public void FillNextScanline(byte[] scanlineData)
        {
            int stride = this.sourceData.Stride;
            bool hasAlpha = this.HasAlpha;
            byte[] data = this.sourceData.Data;
            int num2 = 0;
            int offset = this.offset;
            int num4 = 0;
            while (num2 < this.width)
            {
                bool flag2 = hasAlpha;
                if (this.componentsCount == 4)
                {
                    for (int i = 0; i < this.componentsCount; i++)
                    {
                        byte num6 = (byte) (0xff - data[offset + i]);
                        scanlineData[num4++] = num6;
                        if (flag2)
                        {
                            flag2 &= this.colorKeyMask[i].Contains(num6);
                        }
                    }
                }
                else
                {
                    int num7 = 0;
                    for (int i = (offset + this.componentsCount) - 1; num7 < this.componentsCount; i--)
                    {
                        byte num9 = data[i];
                        scanlineData[num4++] = num9;
                        if (flag2)
                        {
                            flag2 &= this.colorKeyMask[num7].Contains(num9);
                        }
                        num7++;
                    }
                }
                if (hasAlpha)
                {
                    scanlineData[num4++] = flag2 ? ((byte) 0) : ((byte) 0xff);
                }
                num2++;
                offset += this.componentsCount;
            }
            this.offset += stride;
        }

        public int ComponentsCount =>
            this.componentsCount + (this.HasAlpha ? 1 : 0);

        public bool HasAlpha =>
            this.colorKeyMask != null;
    }
}

