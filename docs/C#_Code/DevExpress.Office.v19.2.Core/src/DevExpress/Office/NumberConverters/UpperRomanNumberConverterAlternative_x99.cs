namespace DevExpress.Office.NumberConverters
{
    using System;

    public class UpperRomanNumberConverterAlternative_x99 : RomanNumberConverter
    {
        private int[] arabics = new int[] { 0x31, 0x63, 0x1f3, 0x3e7 };
        private string[] romans = new string[] { "IL", "IC", "ID", "IM" };

        protected internal override NumberingFormat Type =>
            NumberingFormat.UpperRoman;

        protected internal override string[] Romans =>
            this.romans;

        protected internal override int[] Arabics =>
            this.arabics;
    }
}

