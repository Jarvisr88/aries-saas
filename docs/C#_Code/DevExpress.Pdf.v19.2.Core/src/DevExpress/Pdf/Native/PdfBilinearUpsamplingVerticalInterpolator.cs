namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfBilinearUpsamplingVerticalInterpolator : PdfBilinearUpsamplingInterpolator
    {
        private readonly int sourceHeight;
        private readonly byte[][] sourceBuffers;
        private int currentY;
        private int currentSourceY;

        public PdfBilinearUpsamplingVerticalInterpolator(IPdfImageScanlineSource dataSource, int sourceWidth, int height, int sourceHeight) : base(dataSource, sourceWidth, height, sourceHeight)
        {
            this.currentSourceY = -1;
            this.sourceHeight = sourceHeight;
            this.sourceBuffers = new byte[2][];
            this.sourceBuffers[1] = this.ReadNextSourceScanline();
        }

        public override unsafe void FillNextScanline(byte[] scanline)
        {
            PdfConvolutionWindowInfo info = base.ConvolutionWindowInfo[this.currentY];
            int startPosition = info.StartPosition;
            if ((startPosition > this.currentSourceY) && (startPosition < (this.sourceHeight - 1)))
            {
                this.currentSourceY = startPosition;
                this.sourceBuffers[0] = this.sourceBuffers[1];
                this.sourceBuffers[1] = this.ReadNextSourceScanline();
            }
            PdfFixedPointNumber[] weights = info.Weights;
            int sourceWidth = base.SourceWidth;
            int componentsCount = this.ComponentsCount;
            int num4 = info.StartPosition - this.currentSourceY;
            int length = weights.Length;
            PdfFixedPointNumber[] array = new PdfFixedPointNumber[componentsCount];
            int num6 = 0;
            int num7 = 0;
            while (num6 < sourceWidth)
            {
                Array.Clear(array, 0, componentsCount);
                int index = 0;
                while (true)
                {
                    if (index >= length)
                    {
                        int num10 = 0;
                        while (true)
                        {
                            if (num10 >= componentsCount)
                            {
                                num6++;
                                num7 += componentsCount;
                                break;
                            }
                            scanline[num7 + num10] = array[num10].RoundToByte();
                            num10++;
                        }
                        break;
                    }
                    PdfFixedPointNumber number = weights[index];
                    byte[] buffer = this.sourceBuffers[num4 + index];
                    int num9 = 0;
                    while (true)
                    {
                        if (num9 >= componentsCount)
                        {
                            index++;
                            break;
                        }
                        PdfFixedPointNumber* numberPtr1 = &(array[num9]);
                        numberPtr1[0] += buffer[num7 + num9] * number;
                        num9++;
                    }
                }
            }
            this.currentY++;
        }

        private byte[] ReadNextSourceScanline()
        {
            byte[] scanlineData = new byte[base.SourceWidth * this.ComponentsCount];
            base.Source.FillNextScanline(scanlineData);
            return scanlineData;
        }
    }
}

