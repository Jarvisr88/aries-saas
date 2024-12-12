namespace DevExpress.Office.NumberConverters
{
    using System;

    public class UpperRomanNumberConverterAlternative_x90x99 : RomanNumberConverter
    {
        private int[] arabics = new int[] { 1, 4, 5, 9, 490, 990 };
        private string[] romans = new string[] { "I", "IV", "V", "IX", "XD", "XM" };

        protected internal override NumberingFormat Type =>
            NumberingFormat.UpperRoman;

        protected internal override string[] Romans =>
            this.romans;

        protected internal override int[] Arabics =>
            this.arabics;
    }
}

