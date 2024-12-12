namespace DevExpress.Office.NumberConverters
{
    using System;

    public class ArabicAlphaConverter : ArabicAlphabetBasedNumberConverter
    {
        private static char[] arabicAlpha = new char[] { 
            'أ', 'ب', 'ت', 'ث', 'ج', 'ح', 'خ', 'د', 'ذ', 'ر', 'ز', 'س', 'ش', 'ص', 'ض', 'ط',
            'ظ', 'ع', 'غ', 'ف', 'ق', 'ك', 'ل', 'م', 'ن', 'ه', 'و', 'ي'
        };
        private static int alphabetSize = arabicAlpha.Length;

        protected internal override char[] Alphabet =>
            arabicAlpha;

        protected internal override int AlphabetSize =>
            alphabetSize;

        protected internal override NumberingFormat Type =>
            NumberingFormat.ArabicAlpha;
    }
}

