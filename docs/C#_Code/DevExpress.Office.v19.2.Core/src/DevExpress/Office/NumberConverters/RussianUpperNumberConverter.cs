namespace DevExpress.Office.NumberConverters
{
    using System;

    public class RussianUpperNumberConverter : AlphabetBasedNumberConverter
    {
        private static char[] russianUpper = new char[] { 
            'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ж', 'З', 'И', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р',
            'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ы', 'Э', 'Ю', 'Я'
        };
        private static int alphabetSize = russianUpper.Length;

        protected internal override char[] Alphabet =>
            russianUpper;

        protected internal override int AlphabetSize =>
            alphabetSize;

        protected internal override NumberingFormat Type =>
            NumberingFormat.RussianUpper;
    }
}

