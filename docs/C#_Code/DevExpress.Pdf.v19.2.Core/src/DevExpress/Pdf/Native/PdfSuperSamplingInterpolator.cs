namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public abstract class PdfSuperSamplingInterpolator : IPdfImageScanlineSource, IDisposable
    {
        private readonly IPdfImageScanlineSource dataSource;
        private readonly int windowSize;
        private readonly PdfSourceImageScanlineInfo[] pixelInfo;

        protected PdfSuperSamplingInterpolator(IPdfImageScanlineSource dataSource, int targetDimension, int sourceDimension)
        {
            this.dataSource = dataSource;
            this.pixelInfo = new PdfSourceImageScanlineInfo[targetDimension];
            float num = ((float) sourceDimension) / ((float) targetDimension);
            int index = 0;
            while (index < targetDimension)
            {
                List<PdfSourceImagePixelInfo> list = new List<PdfSourceImagePixelInfo>();
                float num3 = index * num;
                float num4 = (index + 1) * num;
                int num5 = (int) Math.Ceiling((double) num3);
                int num6 = (int) Math.Floor((double) num4);
                float num7 = num5 - num3;
                float num8 = Math.Min((float) sourceDimension, num4) - num3;
                int startIndex = num5;
                if (num7 != 0f)
                {
                    list.Add(new PdfSourceImagePixelInfo(num5 - 1, new PdfFixedPointNumber(num7 / num8)));
                    startIndex--;
                }
                int num10 = Math.Min(num6, sourceDimension);
                PdfFixedPointNumber factor = new PdfFixedPointNumber(1f / num8);
                int num13 = num5;
                while (true)
                {
                    if (num13 >= num10)
                    {
                        float num11 = num4 - num6;
                        int endIndex = num6 - 1;
                        if ((num11 != 0f) && (num6 < sourceDimension))
                        {
                            list.Add(new PdfSourceImagePixelInfo(num6, new PdfFixedPointNumber(num11 / num8)));
                            endIndex++;
                        }
                        this.windowSize = Math.Max(list.Count, this.windowSize);
                        this.pixelInfo[index] = new PdfSourceImageScanlineInfo(list.ToArray(), startIndex, endIndex);
                        index++;
                        break;
                    }
                    list.Add(new PdfSourceImagePixelInfo(num13, factor));
                    num13++;
                }
            }
        }

        public void Dispose()
        {
            this.dataSource.Dispose();
        }

        public abstract void FillNextScanline(byte[] scanlineData);

        protected IPdfImageScanlineSource DataSource =>
            this.dataSource;

        protected int WindowSize =>
            this.windowSize;

        protected PdfSourceImageScanlineInfo[] PixelInfo =>
            this.pixelInfo;

        public int ComponentsCount =>
            this.dataSource.ComponentsCount;

        public bool HasAlpha =>
            this.dataSource.HasAlpha;
    }
}

