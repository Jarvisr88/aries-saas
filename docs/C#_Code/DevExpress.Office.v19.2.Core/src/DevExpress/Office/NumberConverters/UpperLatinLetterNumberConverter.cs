namespace DevExpress.Office.NumberConverters
{
    using System;

    public class UpperLatinLetterNumberConverter : AlphabetBasedNumberConverter
    {
        private static char[] upperLetter = new char[] { 
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
            'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };
        private static int alphabetSize = upperLetter.Length;

        protected internal override char[] Alphabet =>
            upperLetter;

        protected internal override int AlphabetSize =>
            alphabetSize;

        protected internal override NumberingFormat Type =>
            NumberingFormat.UpperLetter;
    }
}

