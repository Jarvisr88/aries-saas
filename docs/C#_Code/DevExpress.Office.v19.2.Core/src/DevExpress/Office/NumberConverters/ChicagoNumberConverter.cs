namespace DevExpress.Office.NumberConverters
{
    using System;

    public class ChicagoNumberConverter : AlphabetBasedNumberConverter
    {
        private static readonly char[] symbols = new char[] { '*', '†', '‡', '\x00a7' };
        private static int alphabetSize = symbols.Length;

        public override string ConvertNumberCore(long value) => 
            (value != 0) ? base.ConvertNumberCore(value) : "*";

        protected internal override char[] Alphabet =>
            symbols;

        protected internal override int AlphabetSize =>
            alphabetSize;

        protected internal override NumberingFormat Type =>
            NumberingFormat.Chicago;
    }
}

