namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveCardinalGermanNumberConverter : DescriptiveGermanNumberConverterBase
    {
        protected internal override void GenerateDigits(DigitInfoCollection digits, long value)
        {
            base.GenerateDigits(digits, value);
            if ((digits.Count != 0) && (digits.Last.Type != DigitType.Hundred))
            {
                digits.Last.Provider = new CardinalGermanOptional_1_NumericsProvider();
            }
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.CardinalText;
    }
}

