namespace DevExpress.Office.NumberConverters
{
    using System;

    public class LowerRomanNumberConverterAlternative_x45x99 : RomanNumberConverter
    {
        private int[] arabics = new int[] { 1, 4, 5, 9, 10, 40, 0x2d, 0x5f, 400, 450, 900, 950 };
        private string[] romans;

        public LowerRomanNumberConverterAlternative_x45x99()
        {
            string[] textArray1 = new string[12];
            textArray1[0] = "i";
            textArray1[1] = "iv";
            textArray1[2] = "v";
            textArray1[3] = "ix";
            textArray1[4] = "x";
            textArray1[5] = "xl";
            textArray1[6] = "vl";
            textArray1[7] = "vc";
            textArray1[8] = "cd";
            textArray1[9] = "ld";
            textArray1[10] = "cm";
            textArray1[11] = "lm";
            this.romans = textArray1;
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.LowerRoman;

        protected internal override string[] Romans =>
            this.romans;

        protected internal override int[] Arabics =>
            this.arabics;
    }
}

