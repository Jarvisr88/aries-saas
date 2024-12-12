namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2FastTPGDON0Decoder : JBIG2TPGDON0Decoder
    {
        private const int adaptiveTemplatePixelsCount = 0x10;

        public JBIG2FastTPGDON0Decoder(JBIG2Image image, JBIG2Decoder decoder, int[] gbat) : base(image, decoder, gbat)
        {
        }

        public override void Decode()
        {
            JBIG2Image image = base.Image;
            int width = image.Width;
            int height = image.Height;
            int stride = image.Stride;
            byte[] data = image.Data;
            JBIG2Decoder decoder = base.Decoder;
            int initialContext = this.InitialContext;
            int[] numArray = new int[0x10];
            int y = 0;
            int num6 = 0;
            while (y < height)
            {
                num6 ^= decoder.DecodeGB(initialContext);
                numArray[15] = 0;
                numArray[14] = 0;
                numArray[10] = 0;
                numArray[9] = 0;
                numArray[8] = 0;
                numArray[3] = 0;
                numArray[2] = 0;
                numArray[1] = 0;
                numArray[0] = 0;
                numArray[13] = image.GetPixel(0, y - 2);
                numArray[7] = image.GetPixel(0, y - 1);
                numArray[12] = image.GetPixel(1, y - 2);
                numArray[6] = image.GetPixel(1, y - 1);
                numArray[11] = image.GetPixel(2, y - 2);
                numArray[5] = image.GetPixel(2, y - 1);
                numArray[4] = image.GetPixel(3, y - 1);
                if (num6 != 0)
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
                        int index = 0;
                        while (true)
                        {
                            if (index >= 0x10)
                            {
                                image.SetPixel(x, y, base.Decoder.DecodeGB(context));
                                x++;
                                numArray[3] = numArray[2];
                                numArray[2] = numArray[1];
                                numArray[1] = numArray[0];
                                numArray[0] = image.GetPixel(x - 1, y);
                                numArray[10] = numArray[9];
                                numArray[9] = numArray[8];
                                numArray[8] = numArray[7];
                                numArray[7] = numArray[6];
                                numArray[6] = numArray[5];
                                numArray[5] = numArray[4];
                                numArray[4] = image.GetPixel(x + 3, y - 1);
                                numArray[15] = numArray[14];
                                numArray[14] = numArray[13];
                                numArray[13] = numArray[12];
                                numArray[12] = numArray[11];
                                numArray[11] = image.GetPixel(x + 2, y - 2);
                                break;
                            }
                            context |= numArray[index] << (index & 0x1f);
                            index++;
                        }
                    }
                }
                y++;
            }
        }
    }
}

