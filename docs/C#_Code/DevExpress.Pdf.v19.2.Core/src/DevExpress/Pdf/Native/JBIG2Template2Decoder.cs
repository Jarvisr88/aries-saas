namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2Template2Decoder : JBIG2GenericRegionDecoder
    {
        public JBIG2Template2Decoder(JBIG2Image image, JBIG2Decoder decoder) : base(image, decoder)
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
                int num7 = (num5 >= 2) ? (data[num4 - (stride << 1)] << 4) : 0;
                int context = ((num6 >> 3) & 0x7c) | ((num7 >> 3) & 0x380);
                int num9 = 0;
                while (true)
                {
                    if (num9 >= ((width + 7) & -8))
                    {
                        num4 += stride;
                        num5++;
                        break;
                    }
                    byte num10 = 0;
                    if (num5 >= 1)
                    {
                        num6 = (num6 << 8) | (((num9 + 8) < width) ? data[((num4 - stride) + (num9 >> 3)) + 1] : 0);
                    }
                    if (num5 >= 2)
                    {
                        num7 = (num7 << 8) | (((num9 + 8) < width) ? (data[((num4 - (stride << 1)) + (num9 >> 3)) + 1] << 4) : 0);
                    }
                    int num11 = 0;
                    while (true)
                    {
                        if (num11 >= (((width - num9) > 8) ? 8 : (width - num9)))
                        {
                            data[num4 + (num9 >> 3)] = num10;
                            num9 += 8;
                            break;
                        }
                        byte num12 = (byte) base.Decoder.DecodeGB(context);
                        num10 = (byte) (num10 | ((byte) (num12 << ((7 - num11) & 0x1f))));
                        context = ((((context & 0x1bd) << 1) | num12) | ((num6 >> ((10 - num11) & 0x1f)) & 4)) | ((num7 >> ((10 - num11) & 0x1f)) & 0x80);
                        num11++;
                    }
                }
            }
        }
    }
}

