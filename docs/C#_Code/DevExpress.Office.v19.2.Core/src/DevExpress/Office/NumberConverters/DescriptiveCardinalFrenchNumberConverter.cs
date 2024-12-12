namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveCardinalFrenchNumberConverter : DescriptiveFrenchNumberConverterBase
    {
        protected internal override void GenerateDigits(DigitInfoCollection digits, long value)
        {
            base.GenerateDigits(digits, value);
            if (digits.Count == 1)
            {
                digits.Last.Provider = new CardinalFrenchOptionalNumericsProvider();
            }
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.CardinalText;
    }
}

