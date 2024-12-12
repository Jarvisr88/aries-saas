namespace DevExpress.Office.NumberConverters
{
    using System;

    public class UpperRomanNumberConverterAlternative_x95x99 : RomanNumberConverter
    {
        private int[] arabics = new int[] { 1, 4, 0x1ef, 0x3e3 };
        private string[] romans = new string[] { "I", "IV", "VD", "VM" };

        protected internal override NumberingFormat Type =>
            NumberingFormat.UpperRoman;

        protected internal override string[] Romans =>
            this.romans;

        protected internal override int[] Arabics =>
            this.arabics;
    }
}

