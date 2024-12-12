namespace DevExpress.Office.NumberConverters
{
    using System;

    public class LowerRomanNumberConverterAlternative_x95x99 : RomanNumberConverter
    {
        private int[] arabics = new int[] { 1, 4, 0x1ef, 0x3e3 };
        private string[] romans = new string[] { "i", "iv", "vd", "vm" };

        protected internal override NumberingFormat Type =>
            NumberingFormat.LowerRoman;

        protected internal override string[] Romans =>
            this.romans;

        protected internal override int[] Arabics =>
            this.arabics;
    }
}

