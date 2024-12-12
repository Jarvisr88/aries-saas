namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveCardinalSwedishNumberConverter : DescriptiveSwedishNumberConverterBase
    {
        protected internal override void GenerateSinglesDigits(DigitInfoCollection digits, long value)
        {
            if ((!base.FlagMillion && (!base.FlagBillion && (!base.FlagTrillion && (!base.FlagQuadrillion && !base.FlagQuintillion)))) || (digits.Count != 0))
            {
                base.GenerateSinglesDigits(digits, value);
            }
            else
            {
                long number = (value == 1L) ? ((long) 11) : value;
                digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), number));
            }
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.CardinalText;
    }
}

