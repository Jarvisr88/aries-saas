namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveCardinalSpanishNumberConverter : DescriptiveSpanishNumberConverterBase
    {
        protected DigitInfo ChooseCardinalProvider(long value) => 
            !base.FlagMillion ? (!base.FlagBillion ? (!base.FlagTrillion ? (!base.FlagQuadrillion ? (!base.FlagQuintillion ? ((DigitInfo) new ThousandsDigitInfo(this.GenerateThousandProvider(), value)) : ((DigitInfo) new QuintillionDigitInfo(this.GenerateQuintillionProvider(), value))) : ((DigitInfo) new QuadrillionDigitInfo(this.GenerateQuadrillionProvider(), value))) : ((DigitInfo) new TrillionDigitInfo(this.GenerateTrillionProvider(), value))) : ((DigitInfo) new BillionDigitInfo(this.GenerateBillionProvider(), value))) : ((DigitInfo) new MillionDigitInfo(this.GenerateMillionProvider(), value));

        protected internal override void GenerateBillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagBillion = true;
            this.GenerateLastDigit(digits, value);
            base.FlagBillion = false;
        }

        protected internal override INumericsProvider GenerateBillionProvider() => 
            new CardinalSpanishNumericsProvider();

        protected internal override void GenerateHundredDigits(DigitInfoCollection digits, long value)
        {
            if ((value != 1L) || (!base.IsValueGreaterHundred || !base.OneHundredValue))
            {
                base.GenerateHundredDigits(digits, value);
            }
            else
            {
                if (digits.Count != 0)
                {
                    digits.Add(new SeparatorDigitInfo(new CardinalSpanishNumericsProvider(), 0L));
                }
                digits.Add(new HundredsDigitInfo(new CardinalSpanishNumericsProvider(), (long) 10));
            }
        }

        protected internal override INumericsProvider GenerateHundredProvider() => 
            new CardinalSpanishNumericsProvider();

        protected void GenerateLastDigit(DigitInfoCollection digits, long value)
        {
            base.GenerateDigits(digits, value);
            digits.Add(new SeparatorDigitInfo(this.GenerateMillionProvider(), 0L));
            if ((value % ((long) 100)) == 1L)
            {
                digits.Add(this.ChooseCardinalProvider(0L));
            }
            else
            {
                digits.Add(this.ChooseCardinalProvider(1L));
            }
        }

        protected internal override void GenerateMillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagMillion = true;
            this.GenerateLastDigit(digits, value);
            base.FlagMillion = false;
        }

        protected internal override INumericsProvider GenerateMillionProvider() => 
            new CardinalSpanishNumericsProvider();

        protected internal override void GenerateQuadrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuadrillion = true;
            this.GenerateLastDigit(digits, value);
            base.FlagQuadrillion = false;
        }

        protected internal override INumericsProvider GenerateQuadrillionProvider() => 
            new CardinalSpanishNumericsProvider();

        protected internal override void GenerateQuintillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuintillion = true;
            this.GenerateLastDigit(digits, value);
            base.FlagQuintillion = false;
        }

        protected internal override INumericsProvider GenerateQuintillionProvider() => 
            new CardinalSpanishNumericsProvider();

        protected internal override void GenerateSinglesDigits(DigitInfoCollection digits, long value)
        {
            if (!base.FlagThousand || ((value != 1L) || (digits.Count != 0)))
            {
                if ((digits.Count != 0) && ((value == 1L) && (base.FlagThousand && base.IsDigitInfoGreaterThousand(digits.Last))))
                {
                    digits.Add(new SeparatorDigitInfo(this.GenerateSinglesProvider(), 0L));
                }
                else if ((digits.Count == 0) && base.IsValueGreaterThousand)
                {
                    digits.Add(new SingleNumeralDigitInfo(this.GenerateSinglesProvider(), value));
                }
                else
                {
                    base.GenerateSinglesDigits(digits, value);
                }
            }
        }

        protected internal override INumericsProvider GenerateSinglesProvider() => 
            new CardinalSpanishNumericsProvider();

        protected internal override INumericsProvider GenerateTeensProvider() => 
            new CardinalSpanishNumericsProvider();

        protected internal override void GenerateTenthsDigits(DigitInfoCollection digits, long value)
        {
            if ((value <= 20) || (value >= 30))
            {
                base.GenerateTenthsDigits(digits, value);
            }
            else
            {
                this.GenerateTenthsSeparator(digits, this.GenerateTenthsProvider());
                digits.Add(new TenthsDigitInfo(new CardinalSpanishOptionalNumericsProvider(), (value % ((long) 10)) + 1L));
            }
        }

        protected internal override INumericsProvider GenerateTenthsProvider() => 
            new CardinalSpanishNumericsProvider();

        protected internal override INumericsProvider GenerateThousandProvider() => 
            new CardinalSpanishNumericsProvider();

        protected internal override void GenerateTrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagTrillion = true;
            this.GenerateLastDigit(digits, value);
            base.FlagTrillion = false;
        }

        protected internal override INumericsProvider GenerateTrillionProvider() => 
            new CardinalSpanishNumericsProvider();

        protected internal override NumberingFormat Type =>
            NumberingFormat.CardinalText;
    }
}

