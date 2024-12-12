namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class JBIG2TPGDONDecoder : JBIG2AdaptiveTemplateRegionDecoder
    {
        protected JBIG2TPGDONDecoder(JBIG2Image image, JBIG2Decoder decoder, int[] gbat) : base(image, decoder, gbat)
        {
        }

        internal static JBIG2TPGDONDecoder Create(JBIG2Image image, JBIG2Decoder decoder, int[] gbat, int gbTemplate)
        {
            switch (gbTemplate)
            {
                case 0:
                    return JBIG2TPGDON0Decoder.CreateTPGDON0Decoder(image, decoder, gbat);

                case 1:
                    return new JBIG2TPGDON1Decoder(image, decoder, gbat);

                case 2:
                    return new JBIG2TPGDON2Decoder(image, decoder, gbat);

                case 3:
                    return new JBIG2TPGDON3Decoder(image, decoder, gbat);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        public override void Decode()
        {
            int[] adaptiveTemplate = base.AdaptiveTemplate;
            int length = adaptiveTemplate.Length;
            JBIG2Image image = base.Image;
            int width = image.Width;
            int height = image.Height;
            int stride = image.Stride;
            byte[] data = image.Data;
            JBIG2Decoder decoder = base.Decoder;
            int initialContext = this.InitialContext;
            int y = 0;
            int num7 = 0;
            while (y < height)
            {
                num7 ^= decoder.DecodeGB(initialContext);
                if (num7 != 0)
                {
                    if (y != 0)
                    {
                        int destinationIndex = y * stride;
                        Array.Copy(data, destinationIndex - stride, data, destinationIndex, stride);
                    }
                }
                else
                {
                    int x = 0;
                    while (x < width)
                    {
                        int context = 0;
                        int num10 = 0;
                        int num11 = 0;
                        while (true)
                        {
                            if (num10 >= length)
                            {
                                image.SetPixel(x, y, base.Decoder.DecodeGB(context));
                                x++;
                                break;
                            }
                            context |= image.GetPixel(x + adaptiveTemplate[num10++], y + adaptiveTemplate[num10++]) << (num11 & 0x1f);
                            num11++;
                        }
                    }
                }
                y++;
            }
        }

        protected abstract int InitialContext { get; }
    }
}

