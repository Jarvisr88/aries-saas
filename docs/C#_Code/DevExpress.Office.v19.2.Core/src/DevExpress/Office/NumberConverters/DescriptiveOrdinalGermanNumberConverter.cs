namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveOrdinalGermanNumberConverter : DescriptiveGermanNumberConverterBase
    {
        protected internal override void GenerateDigits(DigitInfoCollection digits, long value)
        {
            base.GenerateDigits(digits, value);
            if (!base.FlagGermanHundred)
            {
                digits.Last.Provider = new OrdinalGermanNumericsProvider();
            }
            else
            {
                digits.Last.Provider = new OrdinalGermanOptionalNumericsProvider();
            }
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.CardinalText;
    }
}

