namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveCardinalUkrainianNumberConverter : SlavicCardinalBase
    {
        protected internal override INumericsProvider GenerateBillionProvider() => 
            new CardinalUkrainianNumericsProvider();

        protected internal override INumericsProvider GenerateHundredProvider() => 
            new CardinalUkrainianNumericsProvider();

        protected internal override INumericsProvider GenerateMillionProvider() => 
            new CardinalUkrainianNumericsProvider();

        protected internal override INumericsProvider GenerateQuadrillionProvider() => 
            new CardinalUkrainianNumericsProvider();

        protected internal override INumericsProvider GenerateQuintillionProvider() => 
            new CardinalUkrainianNumericsProvider();

        protected internal override void GenerateSinglesDigits(DigitInfoCollection digits, long value)
        {
            if (!base.IsValueGreaterHundred || ((value != 1L) || (digits.Count != 0)))
            {
                base.GenerateSinglesDigits(digits, value);
            }
        }

        protected internal override INumericsProvider GenerateSinglesProvider() => 
            new CardinalUkrainianNumericsProvider();

        protected internal override INumericsProvider GenerateTeensProvider() => 
            new CardinalUkrainianNumericsProvider();

        protected internal override INumericsProvider GenerateTenthsProvider() => 
            new CardinalUkrainianNumericsProvider();

        protected internal override INumericsProvider GenerateThousandProvider() => 
            new CardinalUkrainianNumericsProvider();

        protected internal override INumericsProvider GenerateTrillionProvider() => 
            new CardinalUkrainianNumericsProvider();

        protected internal override NumberingFormat Type =>
            NumberingFormat.CardinalText;
    }
}

