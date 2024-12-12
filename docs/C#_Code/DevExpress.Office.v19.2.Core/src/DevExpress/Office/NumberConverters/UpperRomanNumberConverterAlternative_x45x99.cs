namespace DevExpress.Office.NumberConverters
{
    using System;

    public class UpperRomanNumberConverterAlternative_x45x99 : RomanNumberConverter
    {
        private int[] arabics = new int[] { 1, 4, 5, 9, 10, 40, 0x2d, 0x5f, 400, 450, 900, 950 };
        private string[] romans;

        public UpperRomanNumberConverterAlternative_x45x99()
        {
            string[] textArray1 = new string[12];
            textArray1[0] = "I";
            textArray1[1] = "IV";
            textArray1[2] = "V";
            textArray1[3] = "IX";
            textArray1[4] = "X";
            textArray1[5] = "XL";
            textArray1[6] = "VL";
            textArray1[7] = "VC";
            textArray1[8] = "CD";
            textArray1[9] = "LD";
            textArray1[10] = "CM";
            textArray1[11] = "LM";
            this.romans = textArray1;
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.UpperRoman;

        protected internal override string[] Romans =>
            this.romans;

        protected internal override int[] Arabics =>
            this.arabics;
    }
}

