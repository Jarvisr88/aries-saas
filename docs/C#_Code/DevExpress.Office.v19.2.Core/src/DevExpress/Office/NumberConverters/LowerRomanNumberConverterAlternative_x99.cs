namespace DevExpress.Office.NumberConverters
{
    using System;

    public class LowerRomanNumberConverterAlternative_x99 : RomanNumberConverter
    {
        private int[] arabics = new int[] { 0x31, 0x63, 0x1f3, 0x3e7 };
        private string[] romans = new string[] { "il", "ic", "id", "im" };

        protected internal override NumberingFormat Type =>
            NumberingFormat.LowerRoman;

        protected internal override string[] Romans =>
            this.romans;

        protected internal override int[] Arabics =>
            this.arabics;
    }
}

