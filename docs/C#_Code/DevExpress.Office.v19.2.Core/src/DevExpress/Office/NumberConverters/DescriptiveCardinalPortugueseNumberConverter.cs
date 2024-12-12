namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveCardinalPortugueseNumberConverter : DescriptivePortugueseNumberConverterBase
    {
        protected DigitInfo ChooseCardinalProvider(int number) => 
            !base.FlagMillion ? (!base.FlagTrillion ? (!base.FlagQuintillion ? ((DigitInfo) new ThousandsDigitInfo(this.GenerateThousandProvider(), (long) number)) : ((DigitInfo) new QuintillionDigitInfo(this.GenerateQuintillionProvider(), (long) number))) : ((DigitInfo) new TrillionDigitInfo(this.GenerateTrillionProvider(), (long) number))) : ((DigitInfo) new MillionDigitInfo(this.GenerateMillionProvider(), (long) number));

        protected internal override INumericsProvider GenerateBillionProvider() => 
            new CardinalPortugueseNumericsProvider();

        protected internal override void GenerateHundredDigits(DigitInfoCollection digits, long value)
        {
            if (!base.OneHundredValue)
            {
                base.GenerateHundredDigits(digits, value);
            }
            else
            {
                this.GenerateHundredSeparator(digits, this.GenerateHundredProvider());
                digits.Add(new HundredsDigitInfo(new CardinalPortugueseNumericsProvider(), (long) 10));
            }
        }

        protected internal override INumericsProvider GenerateHundredProvider() => 
            new CardinalPortugueseNumericsProvider();

        protected internal override void GenerateHundredSeparator(DigitInfoCollection digits, INumericsProvider provider)
        {
            if ((digits.Count != 0) && base.FullHundreds)
            {
                digits.Add(new SeparatorDigitInfo(provider, 1L));
            }
            else
            {
                base.GenerateHundredSeparator(digits, provider);
            }
        }

        protected void GenerateLastDigits(DigitInfoCollection digits, long value)
        {
            this.GenerateDigitsInfo(digits, value);
            this.GenerateQuintillionSeparator(digits, this.GenerateQuintillionProvider(), value);
            if (value > 1L)
            {
                digits.Add(this.ChooseCardinalProvider(1));
            }
            else
            {
                digits.Add(this.ChooseCardinalProvider(0));
            }
        }

        protected internal override void GenerateMillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagMillion = true;
            this.GenerateLastDigits(digits, value);
            base.FlagMillion = false;
        }

        protected internal override INumericsProvider GenerateMillionProvider() => 
            new CardinalPortugueseNumericsProvider();

        protected internal override INumericsProvider GenerateQuadrillionProvider() => 
            new CardinalPortugueseNumericsProvider();

        protected internal override void GenerateQuintillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuintillion = true;
            this.GenerateLastDigits(digits, value);
            base.FlagQuintillion = false;
        }

        protected internal override INumericsProvider GenerateQuintillionProvider() => 
            new CardinalPortugueseNumericsProvider();

        protected internal override void GenerateSinglesDigits(DigitInfoCollection digits, long value)
        {
            if (((!base.FlagThousand && (!base.FlagBillion && !base.FlagQuadrillion)) || (value != 1L)) || (digits.Count != 0))
            {
                base.GenerateSinglesDigits(digits, value);
            }
        }

        protected internal override INumericsProvider GenerateSinglesProvider() => 
            new CardinalPortugueseNumericsProvider();

        protected internal override void GenerateSinglesSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            this.TeensTenthsSingleSeparator(digits, provider);
        }

        protected internal override INumericsProvider GenerateTeensProvider() => 
            new CardinalPortugueseNumericsProvider();

        protected internal override void GenerateTeensSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            this.TeensTenthsSingleSeparator(digits, provider);
        }

        protected internal override INumericsProvider GenerateTenthsProvider() => 
            new CardinalPortugueseNumericsProvider();

        protected internal override void GenerateTenthsSeparator(DigitInfoCollection digits, INumericsProvider provider)
        {
            this.TeensTenthsSingleSeparator(digits, provider);
        }

        protected internal override INumericsProvider GenerateThousandProvider() => 
            new CardinalPortugueseNumericsProvider();

        protected internal override void GenerateTrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagTrillion = true;
            this.GenerateLastDigits(digits, value);
            base.FlagTrillion = false;
        }

        protected internal override INumericsProvider GenerateTrillionProvider() => 
            new CardinalPortugueseNumericsProvider();

        protected internal override INumericsProvider GetHundredProvider(INumericsProvider provider) => 
            provider;

        protected void TeensTenthsSingleSeparator(DigitInfoCollection digits, INumericsProvider provider)
        {
            if ((digits.Count != 0) && (base.IsValueGreaterHundred && base.IsDigitInfoGreaterThousand(digits.Last)))
            {
                digits.Add(new SeparatorDigitInfo(provider, 0L));
            }
            else if (digits.Count != 0)
            {
                digits.Add(new SeparatorDigitInfo(provider, 1L));
            }
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.CardinalText;
    }
}

