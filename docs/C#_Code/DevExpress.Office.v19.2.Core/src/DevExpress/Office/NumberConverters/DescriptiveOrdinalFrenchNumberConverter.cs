namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveOrdinalFrenchNumberConverter : DescriptiveFrenchNumberConverterBase
    {
        protected internal override void GenerateDigits(DigitInfoCollection digits, long value)
        {
            base.GenerateDigits(digits, value);
            if (digits.Count == 1)
            {
                digits.Last.Provider = new OrdinalFrenchOptionalNumericsProvider();
            }
            else
            {
                digits.Last.Provider = new OrdinalFrenchNumericsProvider();
            }
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.OrdinalText;
    }
}

