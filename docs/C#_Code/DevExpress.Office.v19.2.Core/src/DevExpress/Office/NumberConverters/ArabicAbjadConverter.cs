namespace DevExpress.Office.NumberConverters
{
    using System;

    public class ArabicAbjadConverter : ArabicAlphabetBasedNumberConverter
    {
        private static char[] arabicAbjad = new char[] { 
            'أ', 'ب', 'ج', 'د', 'ه', 'و', 'ز', 'ح', 'ط', 'ي', 'ك', 'ل', 'م', 'ن', 'س', 'ع',
            'ف', 'ص', 'ق', 'ر', 'ش', 'ت', 'ث', 'خ', 'ذ', 'ض', 'ظ', 'غ'
        };
        private static int alphabetSize = arabicAbjad.Length;

        protected internal override char[] Alphabet =>
            arabicAbjad;

        protected internal override int AlphabetSize =>
            alphabetSize;

        protected internal override NumberingFormat Type =>
            NumberingFormat.ArabicAbjad;
    }
}

