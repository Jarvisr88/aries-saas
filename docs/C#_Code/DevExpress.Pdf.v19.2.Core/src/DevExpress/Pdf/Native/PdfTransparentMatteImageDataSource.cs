namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfTransparentMatteImageDataSource : PdfImageDataSource
    {
        private readonly IPdfImageScanlineSource maskSource;
        private readonly byte[] maskScanline;
        private readonly IList<double> matte;

        public PdfTransparentMatteImageDataSource(IPdfImageScanlineSource source, IPdfImageScanlineSource maskSource, int width, IList<double> matte) : base(source, width)
        {
            this.maskSource = maskSource;
            this.maskScanline = new byte[width];
            this.matte = matte;
        }

        public override void Dispose()
        {
            base.Dispose();
            this.maskSource.Dispose();
        }

        public override void FillBuffer(byte[] buffer, int scanlineCount)
        {
            byte[] buffer2 = new byte[this.ComponentsCount];
            int width = base.Width;
            bool sourceHasAlpha = base.SourceHasAlpha;
            int num2 = 0;
            int num3 = 0;
            while (num2 < scanlineCount)
            {
                int num4 = 0;
                byte[] nextSourceScanline = base.GetNextSourceScanline();
                this.maskSource.FillNextScanline(this.maskScanline);
                int index = 0;
                while (true)
                {
                    if (index >= width)
                    {
                        num2++;
                        break;
                    }
                    int num6 = 0;
                    int num7 = 2;
                    while (true)
                    {
                        if (num6 >= 3)
                        {
                            if (sourceHasAlpha)
                            {
                                num4++;
                            }
                            num3 += 3;
                            buffer[num3++] = this.maskScanline[index];
                            index++;
                            break;
                        }
                        float num8 = ((float) nextSourceScanline[num4++]) / 255f;
                        float num9 = (float) this.matte[num6];
                        float num10 = ((float) this.maskScanline[index]) / 255f;
                        float num11 = ((num8 - num9) / num10) + num9;
                        buffer[num3 + num7--] = PdfMathUtils.ToByte((double) (num11 * 255f));
                        num6++;
                    }
                }
            }
        }

        public override int ComponentsCount =>
            4;

        public override bool HasAlpha =>
            true;
    }
}

