namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveOrdinalEnglishUKNumberConverter : DescriptiveEnglishUKNumberConverterBase
    {
        protected internal override void GenerateDigits(DigitInfoCollection digits, long value)
        {
            base.GenerateDigits(digits, value);
            digits.Last.Provider = new OrdinalEnglishNumericsProvider();
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.OrdinalText;
    }
}

