namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveOrdinalSpanishNumberConverter : DescriptiveSpanishNumberConverterBase
    {
        protected internal override void GenerateBillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagBillion = true;
            this.GenerateDigitsInfo(digits, value);
            digits.Add(new BillionDigitInfo(this.GenerateBillionProvider(), 0L));
            base.FlagBillion = false;
        }

        protected internal override INumericsProvider GenerateBillionProvider() => 
            new OrdinalSpanishNumericsProvider();

        protected internal override void GenerateHundredDigits(DigitInfoCollection digits, long value)
        {
            if ((digits.Count != 0) && base.IsDigitInfoGreaterHundred(digits.Last))
            {
                digits.Add(new SeparatorDigitInfo(new OrdinalSpanishOptionalNumericsProvider(), 0L));
            }
            if (!base.FlagQuadrillion && (!base.FlagTrillion && (!base.FlagBillion && (!base.FlagMillion && !base.FlagThousand))))
            {
                digits.Add(new HundredsDigitInfo(new OrdinalSpanishNumericsProvider(), value));
            }
            else if (value != 1L)
            {
                digits.Add(new HundredsDigitInfo(new OrdinalSpanishOptionalNumericsProvider(), value));
            }
            else
            {
                int num = base.OneHundredValue ? 10 : 1;
                digits.Add(new HundredsDigitInfo(new OrdinalSpanishOptionalNumericsProvider(), (long) num));
            }
        }

        protected internal override INumericsProvider GenerateHundredProvider() => 
            new OrdinalSpanishNumericsProvider();

        protected internal override void GenerateMillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagMillion = true;
            this.GenerateDigitsInfo(digits, value);
            digits.Add(new MillionDigitInfo(this.GenerateMillionProvider(), 0L));
            base.FlagMillion = false;
        }

        protected internal override INumericsProvider GenerateMillionProvider() => 
            new OrdinalSpanishNumericsProvider();

        protected internal override void GenerateQuadrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuadrillion = true;
            this.GenerateDigitsInfo(digits, value);
            digits.Add(new QuadrillionDigitInfo(this.GenerateQuadrillionProvider(), 0L));
            base.FlagQuadrillion = false;
        }

        protected internal override INumericsProvider GenerateQuadrillionProvider() => 
            new OrdinalSpanishNumericsProvider();

        protected internal override void GenerateQuintillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuintillion = true;
            this.GenerateDigitsInfo(digits, value);
            digits.Add(new QuintillionDigitInfo(this.GenerateQuintillionProvider(), 0L));
            base.FlagQuintillion = false;
        }

        protected internal override INumericsProvider GenerateQuintillionProvider() => 
            new OrdinalSpanishNumericsProvider();

        protected internal override void GenerateSinglesDigits(DigitInfoCollection digits, long value)
        {
            if ((digits.Count != 0) || ((value != 1L) || !base.IsValueGreaterThousand))
            {
                if ((digits.Count == 0) && ((value > 1L) && base.IsValueGreaterThousand))
                {
                    digits.Add(new SingleNumeralDigitInfo(new CardinalSpanishNumericsProvider(), value));
                }
                else if ((digits.Count != 0) && ((digits.Last.Type == DigitType.Tenth) && base.IsValueGreaterThousand))
                {
                    digits.Add(new SeparatorDigitInfo(new OrdinalSpanishOptionalNumericsProvider(), 1L));
                    digits.Add(new SingleNumeralDigitInfo(new CardinalSpanishNumericsProvider(), value));
                }
                else if ((digits.Count != 0) && ((digits.Last.Type == DigitType.Billion) && base.IsValueGreaterThousand))
                {
                    digits.Add(new SeparatorDigitInfo(new OrdinalSpanishOptionalNumericsProvider(), 0L));
                    digits.Add(new SingleNumeralDigitInfo(new CardinalSpanishNumericsProvider(), value));
                }
                else if ((digits.Count == 0) || ((digits.Last.Type != DigitType.Hundred) || !base.IsValueGreaterHundred))
                {
                    base.GenerateSinglesDigits(digits, value);
                }
                else
                {
                    long number = (value == 1L) ? (value * 10) : value;
                    digits.Add(new SingleNumeralDigitInfo(this.GenerateSinglesProvider(), number));
                }
            }
        }

        protected internal override INumericsProvider GenerateSinglesProvider() => 
            new OrdinalSpanishNumericsProvider();

        protected internal override void GenerateSinglesSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (base.FlagThousand && ((digits.Count != 0) && (digits.Last.Type == DigitType.Tenth)))
            {
                digits.Add(new SeparatorDigitInfo(new OrdinalSpanishOptionalNumericsProvider(), 1L));
            }
            else
            {
                base.GenerateSinglesSeparator(digits, provider, value);
            }
        }

        protected internal override void GenerateTeensDigits(DigitInfoCollection digits, long value)
        {
            if (!base.IsValueGreaterHundred)
            {
                base.GenerateTeensDigits(digits, value);
            }
            else
            {
                if (digits.Count != 0)
                {
                    this.GenerateTeensSeparator(digits, this.GenerateTeensProvider(), value);
                }
                digits.Add(new TeensDigitInfo(new OrdinalSpanishOptionalNumericsProvider(), value));
            }
        }

        protected internal override INumericsProvider GenerateTeensProvider() => 
            new OrdinalSpanishNumericsProvider();

        protected internal override void GenerateTeensSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if ((digits.Count != 0) && base.IsDigitInfoGreaterThousand(digits.Last))
            {
                digits.Add(new SeparatorDigitInfo(this.GenerateTeensProvider(), 0L));
            }
            else if ((digits.Count != 0) && base.IsValueGreaterHundred)
            {
                digits.Add(new SeparatorDigitInfo(this.GenerateTeensProvider(), 2L));
            }
            else
            {
                base.GenerateTeensSeparator(digits, provider, value);
            }
        }

        protected internal override void GenerateTenthsDigits(DigitInfoCollection digits, long value)
        {
            if (digits.Count != 0)
            {
                if (base.FlagThousand && base.IsDigitInfoGreaterThousand(digits.Last))
                {
                    digits.Add(new SeparatorDigitInfo(new CardinalSpanishNumericsProvider(), 0L));
                }
                DigitType type = digits.Last.Type;
                if (base.FlagMillion && ((type == DigitType.Billion) || ((type == DigitType.Trillion) || ((type == DigitType.Quadrillion) || (type == DigitType.Quintillion)))))
                {
                    digits.Add(new SeparatorDigitInfo(new CardinalSpanishNumericsProvider(), 0L));
                }
                if (base.FlagBillion && ((type == DigitType.Trillion) || ((type == DigitType.Quadrillion) || (type == DigitType.Quintillion))))
                {
                    digits.Add(new SeparatorDigitInfo(new CardinalSpanishNumericsProvider(), 0L));
                }
                if (base.FlagTrillion && ((type == DigitType.Quadrillion) || (type == DigitType.Quintillion)))
                {
                    digits.Add(new SeparatorDigitInfo(new CardinalSpanishNumericsProvider(), 0L));
                }
                if (base.FlagQuadrillion && (type == DigitType.Quintillion))
                {
                    digits.Add(new SeparatorDigitInfo(new CardinalSpanishNumericsProvider(), 0L));
                }
            }
            if (!base.FlagThousand && (!base.FlagMillion && (!base.FlagBillion && (!base.FlagTrillion && !base.FlagQuadrillion))))
            {
                base.GenerateTenthsDigits(digits, value);
            }
            else if (value < 30)
            {
                long number = (value == 20) ? 2L : ((value % ((long) 10)) + 1L);
                digits.Add(new TenthsDigitInfo(this.GetTenthsProvider(value), number));
            }
            else if (!base.IsValueGreaterThousand)
            {
                digits.Add(new TenthsDigitInfo(this.GetTenthsProvider(value), value / ((long) 10)));
                this.GenerateSinglesDigits(digits, value % ((long) 10));
            }
            else
            {
                digits.Add(new TenthsDigitInfo(this.GetTenthsProvider(value), value / ((long) 10)));
                if ((value % ((long) 10)) != 0)
                {
                    this.GenerateSinglesDigits(digits, value % ((long) 10));
                }
            }
        }

        protected internal override INumericsProvider GenerateTenthsProvider() => 
            new OrdinalSpanishNumericsProvider();

        protected internal override INumericsProvider GenerateThousandProvider() => 
            new OrdinalSpanishNumericsProvider();

        protected internal override void GenerateThousandSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (((digits.Count != 0) && (value < 30)) || (value > 0x63))
            {
                digits.Add(new SeparatorDigitInfo(provider, 2L));
            }
            else if (((value % ((long) 10)) == 0) && (value < 100))
            {
                digits.Add(new SeparatorDigitInfo(provider, 0L));
            }
        }

        protected internal override void GenerateTrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagTrillion = true;
            this.GenerateDigitsInfo(digits, value);
            digits.Add(new TrillionDigitInfo(this.GenerateTrillionProvider(), 0L));
            base.FlagTrillion = false;
        }

        protected internal override INumericsProvider GenerateTrillionProvider() => 
            new OrdinalSpanishNumericsProvider();

        protected internal INumericsProvider GetTenthsProvider(long value) => 
            ((value <= 20) || (value >= 30)) ? ((INumericsProvider) new CardinalSpanishNumericsProvider()) : ((INumericsProvider) new CardinalSpanishOptionalNumericsProvider());

        protected internal override NumberingFormat Type =>
            NumberingFormat.OrdinalText;
    }
}

