namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2FastTemplate0Decoder : JBIG2GenericRegionDecoder
    {
        public JBIG2FastTemplate0Decoder(JBIG2Image image, JBIG2Decoder decoder) : base(image, decoder)
        {
        }

        public override void Decode()
        {
            JBIG2Image image = base.Image;
            int width = image.Width;
            int height = image.Height;
            int stride = image.Stride;
            int num4 = 0;
            byte[] data = image.Data;
            if (width <= 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int num5 = 0;
            while (num5 < height)
            {
                int num6 = (num5 >= 1) ? data[num4 - stride] : 0;
                int num7 = (num5 >= 2) ? (data[num4 - (stride * 2)] << 6) : 0;
                int num8 = (width + 7) & -8;
                int context = (num6 & 0x7f0) | (num7 & 0xf800);
                int num10 = 0;
                while (true)
                {
                    if (num10 >= num8)
                    {
                        num4 += stride;
                        num5++;
                        break;
                    }
                    byte num11 = 0;
                    int num12 = ((width - num10) > 8) ? 8 : (width - num10);
                    int num13 = num10 >> 3;
                    if (num5 >= 1)
                    {
                        num6 = (num6 << 8) | (((num10 + 8) < width) ? data[((num4 - stride) + num13) + 1] : 0);
                    }
                    if (num5 >= 2)
                    {
                        num7 = (num7 << 8) | (((num10 + 8) < width) ? (data[((num4 - (stride * 2)) + num13) + 1] << 6) : 0);
                    }
                    int num14 = 0;
                    while (true)
                    {
                        if (num14 >= num12)
                        {
                            data[num4 + num13] = num11;
                            num10 += 8;
                            break;
                        }
                        int num15 = 7 - num14;
                        byte num16 = (byte) base.Decoder.DecodeGB(context);
                        num11 = (byte) (num11 | ((byte) (num16 << (num15 & 0x1f))));
                        context = ((((context & 0x7bf7) << 1) | num16) | ((num6 >> (num15 & 0x1f)) & 0x10)) | ((num7 >> (num15 & 0x1f)) & 0x800);
                        num14++;
                    }
                }
            }
        }
    }
}

