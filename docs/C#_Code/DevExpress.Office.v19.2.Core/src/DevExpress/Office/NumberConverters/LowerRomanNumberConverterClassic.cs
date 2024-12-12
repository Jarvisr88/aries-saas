namespace DevExpress.Office.NumberConverters
{
    using System;

    public class LowerRomanNumberConverterClassic : RomanNumberConverter
    {
        private static int[] arabics = new int[] { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 0x3e8 };
        private static string[] romans;

        static LowerRomanNumberConverterClassic()
        {
            string[] textArray1 = new string[13];
            textArray1[0] = "i";
            textArray1[1] = "iv";
            textArray1[2] = "v";
            textArray1[3] = "ix";
            textArray1[4] = "x";
            textArray1[5] = "xl";
            textArray1[6] = "l";
            textArray1[7] = "xc";
            textArray1[8] = "c";
            textArray1[9] = "cd";
            textArray1[10] = "d";
            textArray1[11] = "cm";
            textArray1[12] = "m";
            romans = textArray1;
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.LowerRoman;

        protected internal override string[] Romans =>
            romans;

        protected internal override int[] Arabics =>
            arabics;
    }
}

