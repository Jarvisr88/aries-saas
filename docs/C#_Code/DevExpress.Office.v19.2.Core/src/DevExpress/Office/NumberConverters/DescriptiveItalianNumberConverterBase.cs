namespace DevExpress.Office.NumberConverters
{
    using System;

    public abstract class DescriptiveItalianNumberConverterBase : SomeLanguagesBased
    {
        private bool flagOptionalProvider;
        private bool oneHundredValue;

        protected DescriptiveItalianNumberConverterBase()
        {
        }

        protected DigitInfo ChooseCardinalProvider(INumericsProvider provider, int number) => 
            !base.FlagMillion ? (!base.FlagBillion ? (!base.FlagTrillion ? (!base.FlagQuadrillion ? (!base.FlagQuintillion ? ((DigitInfo) new ThousandsDigitInfo(provider, (long) number)) : ((DigitInfo) new QuintillionDigitInfo(provider, (long) number))) : ((DigitInfo) new QuadrillionDigitInfo(provider, (long) number))) : ((DigitInfo) new TrillionDigitInfo(provider, (long) number))) : ((DigitInfo) new BillionDigitInfo(provider, (long) number))) : ((DigitInfo) new MillionDigitInfo(provider, (long) number));

        protected internal override void GenerateBillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagBillion = true;
            this.GenerateLastDigits(digits, value);
            base.FlagBillion = false;
        }

        protected internal override INumericsProvider GenerateBillionProvider() => 
            new CardinalItalianNumericsProvider();

        protected internal override void GenerateDigitsInfo(DigitInfoCollection digits, long value)
        {
            if ((value / 0xde0b6b3a7640000L) != 0)
            {
                base.FlagIntegerQuintillion = (value % 0xde0b6b3a7640000L) == 0L;
                this.GenerateQuintillionDigits(digits, value / 0xde0b6b3a7640000L);
            }
            value = value % 0xde0b6b3a7640000L;
            if ((value / 0x38d7ea4c68000L) != 0)
            {
                base.FlagIntegerQuadrillion = (value % 0x38d7ea4c68000L) == 0L;
                this.GenerateQuadrillionDigits(digits, value / 0x38d7ea4c68000L);
            }
            value = value % 0x38d7ea4c68000L;
            if ((value / 0xe8d4a51000L) != 0)
            {
                base.FlagIntegerTrillion = (value % 0xe8d4a51000L) == 0L;
                this.GenerateTrillionDigits(digits, value / 0xe8d4a51000L);
            }
            value = value % 0xe8d4a51000L;
            if ((value / 0x3b9aca00L) != 0)
            {
                base.FlagIntegerBillion = (value % 0x3b9aca00L) == 0L;
                this.GenerateBillionDigits(digits, value / 0x3b9aca00L);
                value = value % 0x3b9aca00L;
            }
            if ((value / 0xf4240L) != 0)
            {
                base.FlagIntegerMillion = (value % 0xf4240L) == 0L;
                this.GenerateMillionDigits(digits, value / 0xf4240L);
            }
            value = value % 0xf4240L;
            if ((value / 0x3e8L) != 0)
            {
                base.FlagIntegerThousand = (value % 0x3e8L) == 0L;
                this.GenerateThousandDigits(digits, value / 0x3e8L);
            }
            long num = value % 0x3e8L;
            value = value % 0x3e8L;
            this.oneHundredValue = (num % ((long) 100)) == 0L;
            if ((value / ((long) 100)) != 0)
            {
                this.GenerateHundredDigits(digits, value / ((long) 100));
            }
            value = value % ((long) 100);
            if (value != 0)
            {
                if (value >= 20)
                {
                    this.GenerateTenthsDigits(digits, value);
                }
                else if (value >= 10)
                {
                    this.GenerateTeensDigits(digits, value % ((long) 10));
                }
                else
                {
                    this.GenerateSinglesDigits(digits, value);
                }
            }
        }

        protected internal override INumericsProvider GenerateHundredProvider() => 
            new CardinalItalianNumericsProvider();

        protected void GenerateLastDigits(DigitInfoCollection digits, long value)
        {
            this.GenerateDigitsInfo(digits, value);
            if (this.Type == NumberingFormat.CardinalText)
            {
                digits.Add(new SeparatorDigitInfo(this.GenerateQuintillionProvider(), 2L));
            }
            this.GenerateQuintillionSeparator(digits, this.GenerateQuintillionProvider(), value);
            if (this.Type == NumberingFormat.OrdinalText)
            {
                digits.Add(this.ChooseCardinalProvider(new OrdinalItalianNumericsProvider(), 0));
            }
            else
            {
                int number = (((value % ((long) 10)) != 1L) || ((value % ((long) 100)) == 11)) ? 1 : 0;
                digits.Add(this.ChooseCardinalProvider(new CardinalItalianNumericsProvider(), number));
            }
        }

        protected internal override void GenerateMillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagMillion = true;
            this.GenerateLastDigits(digits, value);
            base.FlagMillion = false;
        }

        protected internal override INumericsProvider GenerateMillionProvider() => 
            new CardinalItalianNumericsProvider();

        protected internal override void GenerateQuadrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuadrillion = true;
            this.GenerateLastDigits(digits, value);
            base.FlagQuadrillion = false;
        }

        protected internal override INumericsProvider GenerateQuadrillionProvider() => 
            new CardinalItalianNumericsProvider();

        protected internal override void GenerateQuintillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuintillion = true;
            this.GenerateLastDigits(digits, value);
            base.FlagQuintillion = false;
        }

        protected internal override INumericsProvider GenerateQuintillionProvider() => 
            new CardinalItalianNumericsProvider();

        protected internal override INumericsProvider GenerateSinglesProvider() => 
            new CardinalItalianNumericsProvider();

        protected internal override INumericsProvider GenerateTeensProvider() => 
            new CardinalItalianNumericsProvider();

        protected internal override void GenerateTenthsDigits(DigitInfoCollection digits, long value)
        {
            this.flagOptionalProvider = ((value % ((long) 10)) == 1L) || ((value % ((long) 10)) == 8L);
            base.GenerateTenthsDigits(digits, value);
        }

        protected internal override INumericsProvider GenerateTenthsProvider() => 
            !this.flagOptionalProvider ? ((INumericsProvider) new CardinalItalianNumericsProvider()) : ((INumericsProvider) new CardinalItalianOptionalNumericsProvider());

        protected internal override void GenerateThousandDigits(DigitInfoCollection digits, long value)
        {
            base.FlagThousand = true;
            if (this.Type == NumberingFormat.OrdinalText)
            {
                this.GenerateDigitsInfo(digits, value);
                digits.Add(new ThousandsDigitInfo(new OrdinalItalianNumericsProvider(), 0L));
                base.FlagThousand = false;
            }
            else
            {
                if (value != 1L)
                {
                    base.GenerateThousandDigits(digits, value);
                }
                else
                {
                    this.GenerateThousandSeparator(digits, this.GenerateThousandProvider(), value);
                    digits.Add(new ThousandsDigitInfo(this.GenerateThousandProvider(), value));
                }
                base.FlagThousand = false;
            }
        }

        protected internal override INumericsProvider GenerateThousandProvider() => 
            new CardinalItalianNumericsProvider();

        protected internal override void GenerateTrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagTrillion = true;
            this.GenerateLastDigits(digits, value);
            base.FlagTrillion = false;
        }

        protected internal override INumericsProvider GenerateTrillionProvider() => 
            new CardinalItalianNumericsProvider();

        protected internal bool OneHundredValue =>
            this.oneHundredValue;
    }
}

