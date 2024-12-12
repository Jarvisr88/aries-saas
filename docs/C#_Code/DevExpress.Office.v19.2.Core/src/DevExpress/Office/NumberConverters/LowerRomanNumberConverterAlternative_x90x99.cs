namespace DevExpress.Office.NumberConverters
{
    using System;

    public class LowerRomanNumberConverterAlternative_x90x99 : RomanNumberConverter
    {
        private int[] arabics = new int[] { 1, 4, 5, 9, 490, 990 };
        private string[] romans = new string[] { "i", "iv", "v", "ix", "xd", "xm" };

        protected internal override NumberingFormat Type =>
            NumberingFormat.LowerRoman;

        protected internal override string[] Romans =>
            this.romans;

        protected internal override int[] Arabics =>
            this.arabics;
    }
}

