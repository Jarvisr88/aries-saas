namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2TPGDON3Decoder : JBIG2TPGDONDecoder
    {
        internal JBIG2TPGDON3Decoder(JBIG2Image image, JBIG2Decoder decoder, int[] gbat) : base(image, decoder, gbat)
        {
        }

        protected override int[] CreateAdaptiveTemplate(int[] gbat)
        {
            int[] numArray1 = new int[] { 
                -1, 0, -2, 0, -3, 0, -4, 0, 0, 0, 1, -1, 0, -1, -1, -1,
                -2, -1, -3, -1
            };
            numArray1[8] = gbat[0];
            numArray1[9] = gbat[1];
            return numArray1;
        }

        protected override int InitialContext =>
            0x195;
    }
}

