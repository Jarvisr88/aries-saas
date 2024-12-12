namespace DevExpress.Office.NumberConverters
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class DescriptiveGermanNumberConverterBase : SomeLanguagesBased
    {
        protected DescriptiveGermanNumberConverterBase()
        {
        }

        protected internal override void GenerateBillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagBillion = true;
            this.GenerateLastDigits(digits, value);
            if (!base.FlagIntegerBillion)
            {
                digits.Add(new SeparatorDigitInfo(this.GenerateBillionProvider(), 3L));
            }
            base.FlagBillion = false;
        }

        protected internal override INumericsProvider GenerateBillionProvider() => 
            new CardinalGermanNumericsProvider();

        protected internal override void GenerateHundredDigits(DigitInfoCollection digits, long value)
        {
            this.FlagGermanHundred = false;
            if ((digits.Count != 0) && (digits.Last.Type == DigitType.Thousand))
            {
                this.FlagGermanHundred = true;
            }
            base.GenerateHundredDigits(digits, value);
        }

        protected internal override INumericsProvider GenerateHundredProvider() => 
            !this.FlagGermanHundred ? ((INumericsProvider) new CardinalGermanNumericsProvider()) : ((INumericsProvider) new CardinalGermanOptional_2_NumericsProvider());

        protected void GenerateLastDigits(DigitInfoCollection digits, long value)
        {
            this.GenerateDigitsInfo(digits, value);
            digits.Add(new SeparatorDigitInfo(this.GenerateQuintillionProvider(), 3L));
            if ((value % ((long) 100)) == 1L)
            {
                digits.Add(this.GetCardinalProvider(0));
            }
            else
            {
                digits.Add(this.GetCardinalProvider(1));
            }
        }

        protected internal override void GenerateMillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagMillion = true;
            this.GenerateLastDigits(digits, value);
            if (!base.FlagIntegerMillion)
            {
                digits.Add(new SeparatorDigitInfo(this.GenerateMillionProvider(), 3L));
            }
            base.FlagMillion = false;
        }

        protected internal override INumericsProvider GenerateMillionProvider() => 
            new CardinalGermanNumericsProvider();

        protected internal override void GenerateQuadrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuadrillion = true;
            this.GenerateLastDigits(digits, value);
            if (!base.FlagIntegerQuadrillion)
            {
                digits.Add(new SeparatorDigitInfo(this.GenerateQuadrillionProvider(), 3L));
            }
            base.FlagQuadrillion = false;
        }

        protected internal override INumericsProvider GenerateQuadrillionProvider() => 
            new CardinalGermanNumericsProvider();

        protected internal override void GenerateQuintillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuintillion = true;
            this.GenerateLastDigits(digits, value);
            if (!base.FlagIntegerQuintillion)
            {
                digits.Add(new SeparatorDigitInfo(this.GenerateQuintillionProvider(), 3L));
            }
            base.FlagQuintillion = false;
        }

        protected internal override INumericsProvider GenerateQuintillionProvider() => 
            new CardinalGermanNumericsProvider();

        protected internal override void GenerateSinglesDigits(DigitInfoCollection digits, long value)
        {
            if (!base.IsValueGreaterThousand)
            {
                base.GenerateSinglesDigits(digits, value);
            }
            else
            {
                long number = (this.FlagGermanTenths || (value != 1L)) ? (value % ((long) 10)) : ((long) 11);
                digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), number));
            }
            this.FlagGermanTenths = false;
        }

        protected internal override INumericsProvider GenerateSinglesProvider() => 
            new CardinalGermanNumericsProvider();

        protected internal override INumericsProvider GenerateTeensProvider() => 
            new CardinalGermanNumericsProvider();

        protected internal override void GenerateTenthsDigits(DigitInfoCollection digits, long value)
        {
            this.FlagGermanTenths = (value % ((long) 100)) > 20;
            if ((value % ((long) 10)) == 0)
            {
                this.FlagGermanTenths = false;
            }
            else
            {
                this.GenerateSinglesDigits(digits, value % ((long) 10));
            }
            this.GenerateTenthsSeparator(digits, this.GenerateTenthsProvider());
            digits.Add(new TenthsDigitInfo(this.GenerateTenthsProvider(), value / ((long) 10)));
        }

        protected internal override INumericsProvider GenerateTenthsProvider() => 
            new CardinalGermanNumericsProvider();

        protected internal override void GenerateTenthsSeparator(DigitInfoCollection digits, INumericsProvider provider)
        {
            if (digits.Count != 0)
            {
                DigitType type = digits.Last.Type;
                if ((type == DigitType.Single) || (type == DigitType.SingleNumeral))
                {
                    digits.Add(new SeparatorDigitInfo(provider, 2L));
                }
                base.GenerateTenthsSeparator(digits, provider);
            }
        }

        protected internal override INumericsProvider GenerateThousandProvider() => 
            new CardinalGermanNumericsProvider();

        protected internal override void GenerateTrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagTrillion = true;
            this.GenerateLastDigits(digits, value);
            if (!base.FlagIntegerTrillion)
            {
                digits.Add(new SeparatorDigitInfo(this.GenerateTrillionProvider(), 3L));
            }
            base.FlagTrillion = false;
        }

        protected internal override INumericsProvider GenerateTrillionProvider() => 
            new CardinalGermanNumericsProvider();

        protected DigitInfo GetCardinalProvider(int number) => 
            !base.FlagMillion ? (!base.FlagBillion ? (!base.FlagTrillion ? (!base.FlagQuadrillion ? (!base.FlagQuintillion ? ((DigitInfo) new ThousandsDigitInfo(this.GenerateThousandProvider(), (long) number)) : ((DigitInfo) new QuintillionDigitInfo(this.GenerateQuintillionProvider(), (long) number))) : ((DigitInfo) new QuadrillionDigitInfo(this.GenerateQuadrillionProvider(), (long) number))) : ((DigitInfo) new TrillionDigitInfo(this.GenerateTrillionProvider(), (long) number))) : ((DigitInfo) new BillionDigitInfo(this.GenerateBillionProvider(), (long) number))) : ((DigitInfo) new MillionDigitInfo(this.GenerateMillionProvider(), (long) number));

        protected internal bool FlagGermanHundred { get; set; }

        protected internal bool FlagGermanTenths { get; set; }
    }
}

