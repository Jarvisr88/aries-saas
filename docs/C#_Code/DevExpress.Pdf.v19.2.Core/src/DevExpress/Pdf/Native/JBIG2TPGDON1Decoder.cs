﻿namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2TPGDON1Decoder : JBIG2TPGDONDecoder
    {
        internal JBIG2TPGDON1Decoder(JBIG2Image image, JBIG2Decoder decoder, int[] gbat) : base(image, decoder, gbat)
        {
        }

        protected override int[] CreateAdaptiveTemplate(int[] gbat)
        {
            int[] numArray1 = new int[] { 
                -1, 0, -2, 0, -3, 0, 0, 0, 2, -1, 1, -1, 0, -1, -1, -1,
                -2, -1, 2, -2, 1, -2, 0, -2, -1, -2
            };
            numArray1[6] = gbat[0];
            numArray1[7] = gbat[1];
            return numArray1;
        }

        protected override int InitialContext =>
            0x795;
    }
}

