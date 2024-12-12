namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfTransparentImageDataSource : PdfImageDataSource
    {
        private readonly IPdfImageScanlineSource maskSource;
        private readonly byte[] maskScanline;

        public PdfTransparentImageDataSource(IPdfImageScanlineSource source, IPdfImageScanlineSource maskSource, int width) : base(source, width)
        {
            this.maskSource = maskSource;
            this.maskScanline = new byte[width];
        }

        public override void Dispose()
        {
            base.Dispose();
            this.maskSource.Dispose();
        }

        public override void FillBuffer(byte[] buffer, int scanlineCount)
        {
            byte[] buffer2 = new byte[3];
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
                    while (true)
                    {
                        if (num6 >= 3)
                        {
                            int num7 = 0;
                            int num8 = 2;
                            while (true)
                            {
                                if (num7 >= 3)
                                {
                                    if (sourceHasAlpha)
                                    {
                                        num4++;
                                    }
                                    buffer[num3++] = this.maskScanline[index];
                                    index++;
                                    break;
                                }
                                buffer[num3++] = buffer2[num8--];
                                num7++;
                            }
                            break;
                        }
                        buffer2[num6] = nextSourceScanline[num4++];
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

