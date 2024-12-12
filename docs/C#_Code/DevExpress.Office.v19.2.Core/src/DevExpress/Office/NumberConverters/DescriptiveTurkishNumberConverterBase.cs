namespace DevExpress.Office.NumberConverters
{
    using System;

    public abstract class DescriptiveTurkishNumberConverterBase : SomeLanguagesBased
    {
        protected DescriptiveTurkishNumberConverterBase()
        {
        }

        protected internal override void GenerateBillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagBillion = true;
            this.GenerateLastDigits(digits, value);
            if (!base.FlagIntegerBillion)
            {
                digits.Add(new SeparatorDigitInfo(new CardinalTurkishNumericsProvider(), 2L));
            }
            base.FlagBillion = false;
        }

        protected internal override INumericsProvider GenerateBillionProvider() => 
            new CardinalTurkishNumericsProvider();

        protected internal override INumericsProvider GenerateHundredProvider() => 
            new CardinalTurkishNumericsProvider();

        protected void GenerateLastDigits(DigitInfoCollection digits, long value)
        {
            this.GenerateDigitsInfo(digits, value);
            if ((this.Type == NumberingFormat.OrdinalText) && (value == 1L))
            {
                digits.Add(new SeparatorDigitInfo(new CardinalTurkishNumericsProvider(), 0L));
            }
            else
            {
                digits.Add(new SeparatorDigitInfo(new CardinalTurkishNumericsProvider(), 2L));
            }
            if ((value % 0xf4240L) > 1L)
            {
                digits.Add(this.GetCardinalProvider(1));
            }
            else
            {
                digits.Add(this.GetCardinalProvider(0));
            }
        }

        protected internal override void GenerateMillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagMillion = true;
            this.GenerateLastDigits(digits, value);
            if (!base.FlagIntegerMillion)
            {
                digits.Add(new SeparatorDigitInfo(new CardinalTurkishNumericsProvider(), 2L));
            }
            base.FlagMillion = false;
        }

        protected internal override INumericsProvider GenerateMillionProvider() => 
            new CardinalTurkishNumericsProvider();

        protected internal override void GenerateQuadrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuadrillion = true;
            this.GenerateLastDigits(digits, value);
            if (!base.FlagIntegerQuadrillion)
            {
                digits.Add(new SeparatorDigitInfo(new CardinalTurkishNumericsProvider(), 2L));
            }
            base.FlagQuadrillion = false;
        }

        protected internal override INumericsProvider GenerateQuadrillionProvider() => 
            new CardinalTurkishNumericsProvider();

        protected internal override void GenerateQuintillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuintillion = true;
            this.GenerateLastDigits(digits, value);
            if (!base.FlagIntegerQuintillion)
            {
                digits.Add(new SeparatorDigitInfo(new CardinalTurkishNumericsProvider(), 2L));
            }
            base.FlagQuintillion = false;
        }

        protected internal override INumericsProvider GenerateQuintillionProvider() => 
            new CardinalTurkishNumericsProvider();

        protected internal override INumericsProvider GenerateSinglesProvider() => 
            new CardinalTurkishNumericsProvider();

        protected internal override INumericsProvider GenerateTeensProvider() => 
            new CardinalTurkishNumericsProvider();

        protected internal override INumericsProvider GenerateTenthsProvider() => 
            new CardinalTurkishNumericsProvider();

        protected internal override void GenerateThousandDigits(DigitInfoCollection digits, long value)
        {
            if (value != 1L)
            {
                base.GenerateThousandDigits(digits, value);
            }
            else
            {
                this.GenerateThousandSeparator(digits, this.GenerateThousandProvider(), value);
                digits.Add(new ThousandsDigitInfo(this.GenerateThousandProvider(), 0L));
            }
        }

        protected internal override INumericsProvider GenerateThousandProvider() => 
            new CardinalTurkishNumericsProvider();

        protected internal override void GenerateTrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagTrillion = true;
            this.GenerateLastDigits(digits, value);
            if (!base.FlagIntegerTrillion)
            {
                digits.Add(new SeparatorDigitInfo(new CardinalTurkishNumericsProvider(), 2L));
            }
            base.FlagTrillion = false;
        }

        protected internal override INumericsProvider GenerateTrillionProvider() => 
            new CardinalTurkishNumericsProvider();

        protected DigitInfo GetCardinalProvider(int number) => 
            !base.FlagMillion ? (!base.FlagBillion ? (!base.FlagTrillion ? (!base.FlagQuadrillion ? (!base.FlagQuintillion ? ((DigitInfo) new ThousandsDigitInfo(this.GenerateThousandProvider(), (long) number)) : ((DigitInfo) new QuintillionDigitInfo(this.GenerateQuintillionProvider(), (long) number))) : ((DigitInfo) new QuadrillionDigitInfo(this.GenerateQuadrillionProvider(), (long) number))) : ((DigitInfo) new TrillionDigitInfo(this.GenerateTrillionProvider(), (long) number))) : ((DigitInfo) new BillionDigitInfo(this.GenerateBillionProvider(), (long) number))) : ((DigitInfo) new MillionDigitInfo(this.GenerateMillionProvider(), (long) number));
    }
}

