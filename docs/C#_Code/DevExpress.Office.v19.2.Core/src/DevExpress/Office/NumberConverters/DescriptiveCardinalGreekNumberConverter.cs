namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveCardinalGreekNumberConverter : DescriptiveGreekNumberConverterBase
    {
        protected DigitInfo ChooseCardinalProvider(int number) => 
            !base.FlagMillion ? (!base.FlagBillion ? (!base.FlagTrillion ? (!base.FlagQuadrillion ? (!base.FlagQuintillion ? ((DigitInfo) new ThousandsDigitInfo(this.GenerateThousandProvider(), (long) number)) : ((DigitInfo) new QuintillionDigitInfo(this.GenerateQuintillionProvider(), (long) number))) : ((DigitInfo) new QuadrillionDigitInfo(this.GenerateQuadrillionProvider(), (long) number))) : ((DigitInfo) new TrillionDigitInfo(this.GenerateTrillionProvider(), (long) number))) : ((DigitInfo) new BillionDigitInfo(this.GenerateBillionProvider(), (long) number))) : ((DigitInfo) new MillionDigitInfo(this.GenerateMillionProvider(), (long) number));

        protected internal override void GenerateBillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagBillion = true;
            if (value == 1L)
            {
                this.GenerateLastDigits(digits, value);
            }
            else
            {
                base.GenerateBillionDigits(digits, value);
            }
            base.FlagBillion = false;
        }

        protected internal override INumericsProvider GenerateBillionProvider() => 
            new CardinalGreekNumericsProvider();

        protected internal override void GenerateHundredDigits(DigitInfoCollection digits, long value)
        {
            if (!base.FlagOneFullHundred || (value != 1L))
            {
                base.GenerateHundredDigits(digits, value);
            }
            else
            {
                if (digits.Count != 0)
                {
                    digits.Add(new SeparatorDigitInfo(this.GenerateHundredProvider(), 0L));
                }
                digits.Add(new HundredsDigitInfo(this.GenerateHundredProvider(), (long) 10));
            }
        }

        protected internal override INumericsProvider GenerateHundredProvider() => 
            !base.FlagThousand ? ((INumericsProvider) new CardinalGreekNumericsProvider()) : ((INumericsProvider) new CardinalGreekOptionalNumericsProvider());

        protected void GenerateLastDigits(DigitInfoCollection digits, long value)
        {
            digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value));
            digits.Add(new SeparatorDigitInfo(this.GenerateQuintillionProvider(), 0L));
            digits.Add(this.ChooseCardinalProvider(1));
        }

        protected internal override void GenerateMillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagMillion = true;
            if (value == 1L)
            {
                this.GenerateLastDigits(digits, value);
            }
            else
            {
                base.GenerateMillionDigits(digits, value);
            }
            base.FlagMillion = false;
        }

        protected internal override INumericsProvider GenerateMillionProvider() => 
            new CardinalGreekNumericsProvider();

        protected internal override void GenerateQuadrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuadrillion = true;
            if (value == 1L)
            {
                this.GenerateLastDigits(digits, value);
            }
            else
            {
                base.GenerateQuadrillionDigits(digits, value);
            }
            base.FlagQuadrillion = false;
        }

        protected internal override INumericsProvider GenerateQuadrillionProvider() => 
            new CardinalGreekNumericsProvider();

        protected internal override void GenerateQuintillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuintillion = true;
            if (value == 1L)
            {
                this.GenerateLastDigits(digits, value);
            }
            else
            {
                base.GenerateQuintillionDigits(digits, value);
            }
            base.FlagQuintillion = false;
        }

        protected internal override INumericsProvider GenerateQuintillionProvider() => 
            new CardinalGreekNumericsProvider();

        protected internal override void GenerateSinglesDigits(DigitInfoCollection digits, long value)
        {
            if ((digits.Count != 0) && ((value == 1L) && (base.FlagThousand && base.IsDigitInfoGreaterHundred(digits.Last))))
            {
                digits.Add(new SeparatorDigitInfo(this.GenerateThousandProvider(), 0L));
            }
            else
            {
                value = ((value != 1L) || !base.FlagThousand) ? value : ((long) 10);
                base.GenerateSinglesDigits(digits, value);
            }
        }

        protected internal override INumericsProvider GenerateSinglesProvider() => 
            new CardinalGreekNumericsProvider();

        protected internal override INumericsProvider GenerateTeensProvider() => 
            new CardinalGreekNumericsProvider();

        protected internal override void GenerateTeensSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (digits.Count != 0)
            {
                digits.Add(new SeparatorDigitInfo(provider, 0L));
            }
        }

        protected internal override INumericsProvider GenerateTenthsProvider() => 
            new CardinalGreekNumericsProvider();

        protected internal override void GenerateThousandDigits(DigitInfoCollection digits, long value)
        {
            if (value != 1L)
            {
                base.GenerateThousandDigits(digits, value);
            }
            else
            {
                if (digits.Count != 0)
                {
                    digits.Add(new SeparatorDigitInfo(this.GenerateHundredProvider(), 0L));
                }
                digits.Add(new ThousandsDigitInfo(this.GenerateThousandProvider(), 1L));
            }
        }

        protected internal override INumericsProvider GenerateThousandProvider() => 
            new CardinalGreekNumericsProvider();

        protected internal override void GenerateThousandSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (value != 1L)
            {
                base.GenerateThousandSeparator(digits, provider, value);
            }
        }

        protected internal override void GenerateTrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagTrillion = true;
            if (value == 1L)
            {
                this.GenerateLastDigits(digits, value);
            }
            else
            {
                base.GenerateTrillionDigits(digits, value);
            }
            base.FlagTrillion = false;
        }

        protected internal override INumericsProvider GenerateTrillionProvider() => 
            new CardinalGreekNumericsProvider();

        protected internal override INumericsProvider GetTeensOptionProvider(long digitCount) => 
            new CardinalGreekOptionalNumericsProvider();

        protected internal override NumberingFormat Type =>
            NumberingFormat.CardinalText;
    }
}

