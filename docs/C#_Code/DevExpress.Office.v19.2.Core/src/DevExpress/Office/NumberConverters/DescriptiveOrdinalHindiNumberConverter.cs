namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveOrdinalHindiNumberConverter : DescriptiveHindiNumberConverterBase
    {
        protected internal override void GenerateDigits(DigitInfoCollection digits, long value)
        {
            base.GenerateDigits(digits, value);
            if ((digits.Last.GetType() == typeof(SingleDigitInfo)) && (digits.Count > 1))
            {
                digits.Last.Provider = new OrdinalHindiOptionalNumericsProvider();
            }
            else
            {
                digits.Last.Provider = new OrdinalHindiNumericsProvider();
            }
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.OrdinalText;
    }
}

