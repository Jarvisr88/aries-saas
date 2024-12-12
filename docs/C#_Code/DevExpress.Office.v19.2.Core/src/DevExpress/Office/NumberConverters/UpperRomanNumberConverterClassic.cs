namespace DevExpress.Office.NumberConverters
{
    using System;

    public class UpperRomanNumberConverterClassic : RomanNumberConverter
    {
        private static int[] arabics = new int[] { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 0x3e8 };
        private static string[] romans;

        static UpperRomanNumberConverterClassic()
        {
            string[] textArray1 = new string[13];
            textArray1[0] = "I";
            textArray1[1] = "IV";
            textArray1[2] = "V";
            textArray1[3] = "IX";
            textArray1[4] = "X";
            textArray1[5] = "XL";
            textArray1[6] = "L";
            textArray1[7] = "XC";
            textArray1[8] = "C";
            textArray1[9] = "CD";
            textArray1[10] = "D";
            textArray1[11] = "CM";
            textArray1[12] = "M";
            romans = textArray1;
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.UpperRoman;

        protected internal override string[] Romans =>
            romans;

        protected internal override int[] Arabics =>
            arabics;
    }
}

