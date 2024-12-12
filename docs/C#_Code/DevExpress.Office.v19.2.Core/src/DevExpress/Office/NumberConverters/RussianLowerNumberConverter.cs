namespace DevExpress.Office.NumberConverters
{
    using System;

    public class RussianLowerNumberConverter : AlphabetBasedNumberConverter
    {
        private static char[] russianLower = new char[] { 
            'а', 'б', 'в', 'г', 'д', 'е', 'ж', 'з', 'и', 'к', 'л', 'м', 'н', 'о', 'п', 'р',
            'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ы', 'э', 'ю', 'я'
        };
        private static int alphabetSize = russianLower.Length;

        protected internal override char[] Alphabet =>
            russianLower;

        protected internal override int AlphabetSize =>
            alphabetSize;

        protected internal override NumberingFormat Type =>
            NumberingFormat.RussianLower;
    }
}

