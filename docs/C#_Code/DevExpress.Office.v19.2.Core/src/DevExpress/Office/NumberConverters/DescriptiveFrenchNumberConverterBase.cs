namespace DevExpress.Office.NumberConverters
{
    using System;

    public abstract class DescriptiveFrenchNumberConverterBase : DescriptiveNumberConverterBase
    {
        protected DescriptiveFrenchNumberConverterBase()
        {
        }

        protected DigitInfo ChooseCardinalProvider(long value) => 
            !base.FlagMillion ? (!base.FlagBillion ? (!base.FlagTrillion ? (!base.FlagQuadrillion ? (!base.FlagQuintillion ? ((DigitInfo) new ThousandsDigitInfo(this.GenerateThousandProvider(), value)) : ((DigitInfo) new QuintillionDigitInfo(this.GenerateQuintillionProvider(), value))) : ((DigitInfo) new QuadrillionDigitInfo(this.GenerateQuadrillionProvider(), value))) : ((DigitInfo) new TrillionDigitInfo(this.GenerateTrillionProvider(), value))) : ((DigitInfo) new BillionDigitInfo(this.GenerateBillionProvider(), value))) : ((DigitInfo) new MillionDigitInfo(this.GenerateMillionProvider(), value));

        protected internal override void GenerateBillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagBillion = true;
            this.GenerateLastDigit(digits, value);
            base.FlagBillion = false;
        }

        protected internal override INumericsProvider GenerateBillionProvider() => 
            new CardinalFrenchNumericsProvider();

        protected internal override INumericsProvider GenerateHundredProvider() => 
            new CardinalFrenchNumericsProvider();

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
            new CardinalFrenchNumericsProvider();

        protected internal override void GenerateQuadrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuadrillion = true;
            this.GenerateLastDigit(digits, value);
            base.FlagQuadrillion = false;
        }

        protected internal override INumericsProvider GenerateQuadrillionProvider() => 
            new CardinalFrenchNumericsProvider();

        protected internal override void GenerateQuintillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuintillion = true;
            this.GenerateLastDigit(digits, value);
            base.FlagQuintillion = false;
        }

        protected internal override INumericsProvider GenerateQuintillionProvider() => 
            new CardinalFrenchNumericsProvider();

        protected internal override void GenerateSinglesDigits(DigitInfoCollection digits, long value)
        {
            if ((digits.Count != 0) && (digits.Last.Type == DigitType.Tenth))
            {
                long number = digits.Last.Number;
                if ((number == 5L) || (number == 7L))
                {
                    this.GenerateTeensDigits(digits, value);
                    return;
                }
                if (number == 6L)
                {
                    if (value == 0)
                    {
                        digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), (long) 11));
                        return;
                    }
                    this.GenerateSinglesSeparator(digits, this.GenerateSinglesProvider(), value);
                    digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value));
                    return;
                }
            }
            base.GenerateSinglesDigits(digits, value);
        }

        protected internal override INumericsProvider GenerateSinglesProvider() => 
            new CardinalFrenchNumericsProvider();

        protected internal override void GenerateSinglesSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (digits.Count != 0)
            {
                if (digits.Last.Type == DigitType.Tenth)
                {
                    if ((value == 1L) && (digits.Last.Number != 6L))
                    {
                        digits.Add(new SeparatorDigitInfo(provider, 2L));
                    }
                    else
                    {
                        digits.Add(new SeparatorDigitInfo(provider, 1L));
                    }
                }
                else
                {
                    digits.Add(new SeparatorDigitInfo(provider, 0L));
                }
            }
        }

        protected internal override INumericsProvider GenerateTeensProvider() => 
            new CardinalFrenchNumericsProvider();

        protected internal override void GenerateTeensSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (digits.Count != 0)
            {
                if (digits.Last.Type == DigitType.Tenth)
                {
                    if ((digits.Last.Number == 5L) && (value == 1L))
                    {
                        digits.Add(new SeparatorDigitInfo(provider, 2L));
                    }
                    else
                    {
                        digits.Add(new SeparatorDigitInfo(provider, 1L));
                    }
                }
                else
                {
                    digits.Add(new SeparatorDigitInfo(provider, 0L));
                }
            }
        }

        protected internal override INumericsProvider GenerateTenthsProvider() => 
            new CardinalFrenchNumericsProvider();

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
            new CardinalFrenchNumericsProvider();

        protected internal override void GenerateTrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagTrillion = true;
            this.GenerateLastDigit(digits, value);
            base.FlagTrillion = false;
        }

        protected internal override INumericsProvider GenerateTrillionProvider() => 
            new CardinalFrenchNumericsProvider();
    }
}

