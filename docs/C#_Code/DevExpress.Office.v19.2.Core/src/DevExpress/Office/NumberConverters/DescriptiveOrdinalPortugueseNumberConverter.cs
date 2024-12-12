namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveOrdinalPortugueseNumberConverter : DescriptivePortugueseNumberConverterBase
    {
        protected DigitInfo ChooseOrdinalProvider(int number) => 
            !base.FlagMillion ? (!base.FlagBillion ? (!base.FlagTrillion ? (!base.FlagQuadrillion ? (!base.FlagQuintillion ? ((DigitInfo) new ThousandsDigitInfo(this.GenerateThousandProvider(), (long) number)) : ((DigitInfo) new QuintillionDigitInfo(this.GenerateQuintillionProvider(), (long) number))) : ((DigitInfo) new QuadrillionDigitInfo(this.GenerateQuadrillionProvider(), (long) number))) : ((DigitInfo) new TrillionDigitInfo(this.GenerateTrillionProvider(), (long) number))) : ((DigitInfo) new BillionDigitInfo(this.GenerateBillionProvider(), (long) number))) : ((DigitInfo) new MillionDigitInfo(this.GenerateMillionProvider(), (long) number));

        protected internal override void GenerateBillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagBillion = true;
            this.GenerateLastDigits(digits, value);
            base.FlagBillion = false;
        }

        protected internal override INumericsProvider GenerateBillionProvider() => 
            new OrdinalPortugueseNumericsProvider();

        protected internal override void GenerateHundredDigits(DigitInfoCollection digits, long value)
        {
            if (!base.IsValueGreaterHundred)
            {
                base.GenerateHundredDigits(digits, value);
            }
            else
            {
                if ((digits.Count != 0) && base.FullHundreds)
                {
                    digits.Add(new SeparatorDigitInfo(new OrdinalPortugueseOptionalNumericProvider(), 1L));
                }
                else
                {
                    base.GenerateHundredSeparator(digits, this.GenerateHundredProvider());
                }
                long number = base.OneHundredValue ? ((long) 10) : value;
                digits.Add(new HundredsDigitInfo(new CardinalPortugueseNumericsProvider(), number));
            }
        }

        protected internal override INumericsProvider GenerateHundredProvider() => 
            new OrdinalPortugueseNumericsProvider();

        protected internal override void GenerateHundredSeparator(DigitInfoCollection digits, INumericsProvider provider)
        {
            if ((digits.Count != 0) && base.FullHundreds)
            {
                digits.Add(new SeparatorDigitInfo(new OrdinalPortugueseOptionalNumericProvider(), 1L));
            }
            else
            {
                base.GenerateHundredSeparator(digits, provider);
            }
        }

        protected void GenerateLastDigits(DigitInfoCollection digits, long value)
        {
            if (value == 1L)
            {
                digits.Add(this.ChooseOrdinalProvider(1));
            }
            else if ((value % ((long) 100)) != 1L)
            {
                this.GenerateDigitsInfo(digits, value);
                digits.Add(new SeparatorDigitInfo(new OrdinalPortugueseNumericsProvider(), 0L));
                digits.Add(this.ChooseOrdinalProvider(0));
            }
            else
            {
                long number = ((value / ((long) 100)) == 1L) ? (value % ((long) 100)) : (value / ((long) 100));
                digits.Add(new HundredsDigitInfo(new CardinalPortugueseNumericsProvider(), number));
                digits.Add(new SeparatorDigitInfo(new OrdinalPortugueseNumericsProvider(), 0L));
                digits.Add(this.ChooseOrdinalProvider(1));
            }
        }

        protected internal override void GenerateMillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagMillion = true;
            this.GenerateLastDigits(digits, value);
            base.FlagMillion = false;
        }

        protected internal override INumericsProvider GenerateMillionProvider() => 
            new OrdinalPortugueseNumericsProvider();

        protected internal override void GenerateQuadrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuadrillion = true;
            this.GenerateLastDigits(digits, value);
            base.FlagQuadrillion = false;
        }

        protected internal override INumericsProvider GenerateQuadrillionProvider() => 
            new OrdinalPortugueseNumericsProvider();

        protected internal override void GenerateQuintillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuintillion = true;
            this.GenerateLastDigits(digits, value);
            base.FlagQuintillion = false;
        }

        protected internal override INumericsProvider GenerateQuintillionProvider() => 
            new OrdinalPortugueseNumericsProvider();

        protected internal override INumericsProvider GenerateSinglesProvider() => 
            !base.IsValueGreaterHundred ? ((INumericsProvider) new OrdinalPortugueseNumericsProvider()) : ((INumericsProvider) new OrdinalPortugueseOptionalNumericProvider());

        protected internal override INumericsProvider GenerateTeensProvider() => 
            !base.IsValueGreaterHundred ? ((INumericsProvider) new OrdinalPortugueseNumericsProvider()) : ((INumericsProvider) new OrdinalPortugueseOptionalNumericProvider());

        protected internal override void GenerateTenthsDigits(DigitInfoCollection digits, long value)
        {
            if (!base.IsValueGreaterHundred)
            {
                base.GenerateTenthsDigits(digits, value);
            }
            else
            {
                this.GenerateTenthsSeparator(digits, new OrdinalPortugueseOptionalNumericProvider());
                digits.Add(new TenthsDigitInfo(new OrdinalPortugueseOptionalNumericProvider(), value / ((long) 10)));
                if ((value % ((long) 10)) != 0)
                {
                    digits.Add(new SeparatorDigitInfo(new OrdinalPortugueseOptionalNumericProvider(), 1L));
                    digits.Add(new SingleNumeralDigitInfo(new OrdinalPortugueseOptionalNumericProvider(), value % ((long) 10)));
                }
            }
        }

        protected internal override INumericsProvider GenerateTenthsProvider() => 
            new OrdinalPortugueseNumericsProvider();

        protected internal override void GenerateThousandDigits(DigitInfoCollection digits, long value)
        {
            base.FlagThousand = true;
            this.GenerateLastDigits(digits, value);
            base.FlagThousand = false;
        }

        protected internal override INumericsProvider GenerateThousandProvider() => 
            new OrdinalPortugueseNumericsProvider();

        protected internal override void GenerateTrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagTrillion = true;
            this.GenerateLastDigits(digits, value);
            base.FlagTrillion = false;
        }

        protected internal override INumericsProvider GenerateTrillionProvider() => 
            new OrdinalPortugueseNumericsProvider();

        protected internal override INumericsProvider GetHundredProvider(INumericsProvider provider) => 
            new OrdinalPortugueseOptionalNumericProvider();

        protected internal override NumberingFormat Type =>
            NumberingFormat.OrdinalText;
    }
}

