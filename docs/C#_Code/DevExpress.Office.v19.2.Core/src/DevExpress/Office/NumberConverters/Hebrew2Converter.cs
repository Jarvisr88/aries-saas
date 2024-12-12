namespace DevExpress.Office.NumberConverters
{
    using System;

    public class Hebrew2Converter : AlphabetBasedNumberConverter
    {
        private static char[] hebrew = new char[] { 
            'א', 'ב', 'ג', 'ד', 'ה', 'ו', 'ז', 'ח', 'ט', 'י', 'כ', 'ל', 'מ', 'נ', 'ס', 'ע',
            'פ', 'צ', 'ק', 'ר', 'ש', 'ת'
        };
        private static int alphabetSize = hebrew.Length;

        public override string ConvertNumberCore(long value)
        {
            if (value == 0)
            {
                return string.Empty;
            }
            value -= 1L;
            int index = ((int) value) % this.AlphabetSize;
            return (new string(this.Alphabet[this.AlphabetSize - 1], ((int) value) / this.AlphabetSize) + hebrew[index].ToString());
        }

        protected internal override char[] Alphabet =>
            hebrew;

        protected internal override int AlphabetSize =>
            alphabetSize;

        protected internal override NumberingFormat Type =>
            NumberingFormat.Hebrew2;
    }
}

