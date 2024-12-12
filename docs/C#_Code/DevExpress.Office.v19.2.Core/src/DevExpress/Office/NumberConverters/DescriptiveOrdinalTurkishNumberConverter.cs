namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveOrdinalTurkishNumberConverter : DescriptiveTurkishNumberConverterBase
    {
        protected internal override void GenerateDigits(DigitInfoCollection digits, long value)
        {
            base.GenerateDigits(digits, value);
            digits.Last.Provider = new OrdinalTurkishNumericsProvider();
        }

        protected internal override void GenerateSinglesDigits(DigitInfoCollection digits, long value)
        {
            if (((!base.FlagMillion && (!base.FlagBillion && (!base.FlagTrillion && (!base.FlagQuadrillion && !base.FlagQuintillion)))) || (digits.Count != 0)) || (value != 1L))
            {
                base.GenerateSinglesDigits(digits, value);
            }
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.OrdinalText;
    }
}

