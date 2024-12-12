namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfBGRImageDataSource : PdfImageDataSource
    {
        private readonly int stride;
        private readonly byte[] componentsData;

        public PdfBGRImageDataSource(IPdfImageScanlineSource source, int width, int stride) : base(source, width)
        {
            this.stride = stride;
            this.componentsData = new byte[this.ComponentsCount];
        }

        public override void FillBuffer(byte[] buffer, int scanlineCount)
        {
            int width = base.Width;
            int componentsCount = this.ComponentsCount;
            int num3 = componentsCount - 1;
            int num4 = 0;
            int num5 = 0;
            while (num4 < scanlineCount)
            {
                int num6 = num5;
                int num7 = 0;
                byte[] nextSourceScanline = base.GetNextSourceScanline();
                int num8 = 0;
                while (true)
                {
                    if (num8 >= width)
                    {
                        num4++;
                        num5 += this.stride;
                        break;
                    }
                    int index = 0;
                    while (true)
                    {
                        if (index >= componentsCount)
                        {
                            int num10 = 0;
                            int num11 = num3;
                            while (true)
                            {
                                if (num10 >= componentsCount)
                                {
                                    num8++;
                                    break;
                                }
                                buffer[num6++] = this.componentsData[num11--];
                                num10++;
                            }
                            break;
                        }
                        this.componentsData[index] = nextSourceScanline[num7++];
                        index++;
                    }
                }
            }
        }

        public override int ComponentsCount =>
            base.SourceComponentsCount;
    }
}

