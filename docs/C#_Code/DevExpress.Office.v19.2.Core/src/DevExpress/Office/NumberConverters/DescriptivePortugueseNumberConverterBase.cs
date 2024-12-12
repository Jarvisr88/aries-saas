namespace DevExpress.Office.NumberConverters
{
    using System;

    public abstract class DescriptivePortugueseNumberConverterBase : DescriptiveNumberConverterBase
    {
        private bool oneHundredValue;
        private bool fullHundreds;

        protected DescriptivePortugueseNumberConverterBase()
        {
        }

        protected internal override void GenerateDigitsInfo(DigitInfoCollection digits, long value)
        {
            if ((value / 0xde0b6b3a7640000L) != 0)
            {
                this.GenerateQuintillionDigits(digits, value / 0xde0b6b3a7640000L);
                base.FlagQuintillion = false;
            }
            value = value % 0xde0b6b3a7640000L;
            if ((value / 0x38d7ea4c68000L) != 0)
            {
                this.GenerateQuadrillionDigits(digits, value / 0x38d7ea4c68000L);
                base.FlagQuadrillion = false;
            }
            value = value % 0x38d7ea4c68000L;
            if ((value / 0xe8d4a51000L) != 0)
            {
                this.GenerateTrillionDigits(digits, value / 0xe8d4a51000L);
                base.FlagTrillion = false;
            }
            value = value % 0xe8d4a51000L;
            if ((value / 0x3b9aca00L) != 0)
            {
                this.GenerateBillionDigits(digits, value / 0x3b9aca00L);
                base.FlagBillion = false;
            }
            value = value % 0x3b9aca00L;
            if ((value / 0xf4240L) != 0)
            {
                this.GenerateMillionDigits(digits, value / 0xf4240L);
                base.FlagMillion = false;
            }
            value = value % 0xf4240L;
            if ((value / 0x3e8L) != 0)
            {
                this.GenerateThousandDigits(digits, value / 0x3e8L);
                base.FlagThousand = false;
            }
            long num = value % 0x3e8L;
            if (num >= 100)
            {
                this.fullHundreds = (num % ((long) 100)) == 0L;
            }
            this.oneHundredValue = ((num / ((long) 100)) == 1L) && this.fullHundreds;
            value = value % 0x3e8L;
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

        protected internal abstract INumericsProvider GetHundredProvider(INumericsProvider provider);

        protected internal bool OneHundredValue =>
            this.oneHundredValue;

        protected internal bool FullHundreds =>
            this.fullHundreds;
    }
}

