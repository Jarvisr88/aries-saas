namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfSuperSamplingVerticalInterpolator : PdfSuperSamplingInterpolator
    {
        private readonly int sourceWidth;
        private readonly byte[][] sourceScanlines;
        private int currentY;
        private int lastRowIndex;
        private int previousLastIndex;

        public PdfSuperSamplingVerticalInterpolator(IPdfImageScanlineSource dataSource, int sourceWidth, int height, int sourceHeight) : base(dataSource, height, sourceHeight)
        {
            this.previousLastIndex = -1;
            this.sourceWidth = sourceWidth;
            int windowSize = base.WindowSize;
            this.sourceScanlines = new byte[windowSize][];
            for (int i = 0; i < windowSize; i++)
            {
                this.sourceScanlines[i] = new byte[sourceWidth * dataSource.ComponentsCount];
            }
        }

        private void FillBuffers(PdfSourceImageScanlineInfo scanlineInfo)
        {
            int startIndex = scanlineInfo.StartIndex;
            int endIndex = scanlineInfo.EndIndex;
            bool flag = startIndex == this.previousLastIndex;
            if (flag)
            {
                int lastRowIndex = this.lastRowIndex;
                this.sourceScanlines[0] = this.sourceScanlines[lastRowIndex];
                this.sourceScanlines[lastRowIndex] = new byte[this.sourceWidth * base.ComponentsCount];
            }
            this.previousLastIndex = endIndex;
            int num3 = (endIndex - startIndex) + 1;
            IPdfImageScanlineSource dataSource = base.DataSource;
            this.lastRowIndex = num3 - 1;
            for (int i = flag ? 1 : 0; i < num3; i++)
            {
                dataSource.FillNextScanline(this.sourceScanlines[i]);
            }
        }

        public override unsafe void FillNextScanline(byte[] scanline)
        {
            PdfSourceImageScanlineInfo scanlineInfo = base.PixelInfo[this.currentY];
            this.FillBuffers(scanlineInfo);
            int startIndex = scanlineInfo.StartIndex;
            PdfSourceImagePixelInfo[] pixelInfo = scanlineInfo.PixelInfo;
            int componentsCount = base.ComponentsCount;
            int num3 = 0;
            int num4 = 0;
            while (num3 < this.sourceWidth)
            {
                PdfFixedPointNumber[] numberArray = new PdfFixedPointNumber[componentsCount];
                PdfSourceImagePixelInfo[] infoArray3 = pixelInfo;
                int index = 0;
                while (true)
                {
                    if (index >= infoArray3.Length)
                    {
                        int num7 = 0;
                        while (true)
                        {
                            if (num7 >= componentsCount)
                            {
                                num3++;
                                num4 += componentsCount;
                                break;
                            }
                            scanline[num4 + num7] = numberArray[num7].RoundToByte();
                            num7++;
                        }
                        break;
                    }
                    PdfSourceImagePixelInfo info2 = infoArray3[index];
                    PdfFixedPointNumber factor = info2.Factor;
                    byte[] buffer = this.sourceScanlines[info2.Index - startIndex];
                    int num6 = 0;
                    while (true)
                    {
                        if (num6 >= componentsCount)
                        {
                            index++;
                            break;
                        }
                        PdfFixedPointNumber* numberPtr1 = &(numberArray[num6]);
                        numberPtr1[0] += buffer[num4 + num6] * factor;
                        num6++;
                    }
                }
            }
            this.currentY++;
        }
    }
}

