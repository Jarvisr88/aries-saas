namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfColorKeyMaskedImageDataSource : PdfImageDataSource
    {
        public PdfColorKeyMaskedImageDataSource(IPdfImageScanlineSource source, int width) : base(source, width)
        {
        }

        public override void FillBuffer(byte[] buffer, int scanlineCount)
        {
            byte[] buffer2 = new byte[3];
            int width = base.Width;
            int num2 = 0;
            int num3 = 0;
            while (num2 < scanlineCount)
            {
                int num4 = 0;
                byte[] nextSourceScanline = base.GetNextSourceScanline();
                int num5 = 0;
                while (true)
                {
                    if (num5 >= width)
                    {
                        num2++;
                        break;
                    }
                    int index = 0;
                    while (true)
                    {
                        if (index >= 3)
                        {
                            int num7 = 0;
                            int num8 = 2;
                            while (true)
                            {
                                if (num7 >= 3)
                                {
                                    buffer[num3++] = nextSourceScanline[num4++];
                                    num5++;
                                    break;
                                }
                                buffer[num3++] = buffer2[num8--];
                                num7++;
                            }
                            break;
                        }
                        buffer2[index] = nextSourceScanline[num4++];
                        index++;
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

