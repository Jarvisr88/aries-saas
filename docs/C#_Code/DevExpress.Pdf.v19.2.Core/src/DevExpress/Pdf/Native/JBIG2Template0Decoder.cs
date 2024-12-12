namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2Template0Decoder : JBIG2AdaptiveTemplateRegionDecoder
    {
        public JBIG2Template0Decoder(JBIG2Image image, JBIG2Decoder decoder, int[] gbat) : base(image, decoder, gbat)
        {
        }

        protected override int[] CreateAdaptiveTemplate(int[] gbat)
        {
            int[] numArray1 = new int[] { 
                -1, 0, -2, 0, -3, 0, -4, 0, 0, 0, 2, -1, 1, -1, 0, -1,
                -1, -1, -2, -1, 0, 0, 0, 0, 1, -2, 0, -2, -1, -2, 0, 0
            };
            numArray1[8] = gbat[0];
            numArray1[9] = gbat[1];
            numArray1[20] = gbat[2];
            numArray1[0x15] = gbat[3];
            numArray1[0x16] = gbat[4];
            numArray1[0x17] = gbat[5];
            numArray1[30] = gbat[6];
            numArray1[0x1f] = gbat[7];
            return numArray1;
        }
    }
}

