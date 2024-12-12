namespace DevExpress.Office.NumberConverters
{
    using System;

    public abstract class DescriptiveSwedishNumberConverterBase : SomeLanguagesBased
    {
        protected DescriptiveSwedishNumberConverterBase()
        {
        }

        protected DigitInfo ChooseCardinalProvider(int number) => 
            !base.FlagMillion ? (!base.FlagBillion ? (!base.FlagTrillion ? (!base.FlagQuadrillion ? (!base.FlagQuintillion ? ((DigitInfo) new ThousandsDigitInfo(this.GenerateThousandProvider(), (long) number)) : ((DigitInfo) new QuintillionDigitInfo(this.GenerateQuintillionProvider(), (long) number))) : ((DigitInfo) new QuadrillionDigitInfo(this.GenerateQuadrillionProvider(), (long) number))) : ((DigitInfo) new TrillionDigitInfo(this.GenerateTrillionProvider(), (long) number))) : ((DigitInfo) new BillionDigitInfo(this.GenerateBillionProvider(), (long) number))) : ((DigitInfo) new MillionDigitInfo(this.GenerateMillionProvider(), (long) number));

        protected internal override void GenerateBillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagBillion = true;
            this.GenerateLastDigits(digits, value);
            if (!base.FlagIntegerBillion)
            {
                digits.Add(new SeparatorDigitInfo(new CardinalSwedishNumericsProvider(), 2L));
            }
            base.FlagBillion = false;
        }

        protected internal override INumericsProvider GenerateBillionProvider() => 
            new CardinalSwedishNumericsProvider();

        protected internal override INumericsProvider GenerateHundredProvider() => 
            new CardinalSwedishNumericsProvider();

        protected void GenerateLastDigits(DigitInfoCollection digits, long value)
        {
            this.GenerateDigitsInfo(digits, value);
            if ((this.Type == NumberingFormat.OrdinalText) && (value == 1L))
            {
                digits.Add(new SeparatorDigitInfo(new CardinalSwedishNumericsProvider(), 0L));
            }
            else
            {
                digits.Add(new SeparatorDigitInfo(new CardinalSwedishNumericsProvider(), 2L));
            }
            if ((value % 0xf4240L) > 1L)
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
            if (!base.FlagIntegerMillion)
            {
                digits.Add(new SeparatorDigitInfo(new CardinalSwedishNumericsProvider(), 2L));
            }
            base.FlagMillion = false;
        }

        protected internal override INumericsProvider GenerateMillionProvider() => 
            new CardinalSwedishNumericsProvider();

        protected internal override void GenerateQuadrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuadrillion = true;
            this.GenerateLastDigits(digits, value);
            if (!base.FlagIntegerQuadrillion)
            {
                digits.Add(new SeparatorDigitInfo(new CardinalSwedishNumericsProvider(), 2L));
            }
            base.FlagQuadrillion = false;
        }

        protected internal override INumericsProvider GenerateQuadrillionProvider() => 
            new CardinalSwedishNumericsProvider();

        protected internal override void GenerateQuintillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuintillion = true;
            this.GenerateLastDigits(digits, value);
            if (!base.FlagIntegerQuintillion)
            {
                digits.Add(new SeparatorDigitInfo(new CardinalSwedishNumericsProvider(), 2L));
            }
            base.FlagQuintillion = false;
        }

        protected internal override INumericsProvider GenerateQuintillionProvider() => 
            new CardinalSwedishNumericsProvider();

        protected internal override INumericsProvider GenerateSinglesProvider() => 
            new CardinalSwedishNumericsProvider();

        protected internal override INumericsProvider GenerateTeensProvider() => 
            new CardinalSwedishNumericsProvider();

        protected internal override INumericsProvider GenerateTenthsProvider() => 
            new CardinalSwedishNumericsProvider();

        protected internal override INumericsProvider GenerateThousandProvider() => 
            new CardinalSwedishNumericsProvider();

        protected internal override void GenerateTrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagTrillion = true;
            this.GenerateLastDigits(digits, value);
            if (!base.FlagIntegerTrillion)
            {
                digits.Add(new SeparatorDigitInfo(new CardinalSwedishNumericsProvider(), 2L));
            }
            base.FlagTrillion = false;
        }

        protected internal override INumericsProvider GenerateTrillionProvider() => 
            new CardinalSwedishNumericsProvider();
    }
}

