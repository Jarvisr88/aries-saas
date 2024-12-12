namespace DevExpress.Office.NumberConverters
{
    using System;

    public class LowerLatinLetterNumberConverter : AlphabetBasedNumberConverter
    {
        private static char[] upperLetter = new char[] { 
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
            'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };
        private static int alphabetSize = upperLetter.Length;

        protected internal override char[] Alphabet =>
            upperLetter;

        protected internal override int AlphabetSize =>
            alphabetSize;

        protected internal override NumberingFormat Type =>
            NumberingFormat.LowerLetter;
    }
}

