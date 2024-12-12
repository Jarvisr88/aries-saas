namespace DevExpress.Office.NumberConverters
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class SomeLanguagesBased : DescriptiveNumberConverterBase
    {
        protected SomeLanguagesBased()
        {
        }

        public override string ConvertNumberCore(long value)
        {
            this.FlagIntegerThousand = false;
            this.FlagIntegerMillion = false;
            this.FlagIntegerBillion = false;
            this.FlagIntegerTrillion = false;
            this.FlagIntegerQuadrillion = false;
            this.FlagIntegerQuintillion = false;
            return base.ConvertNumberCore(value);
        }

        protected internal override void GenerateDigitsInfo(DigitInfoCollection digits, long value)
        {
            if ((value / 0xde0b6b3a7640000L) != 0)
            {
                this.FlagIntegerQuintillion = (value % 0xde0b6b3a7640000L) == 0L;
                this.GenerateQuintillionDigits(digits, value / 0xde0b6b3a7640000L);
            }
            value = value % 0xde0b6b3a7640000L;
            if ((value / 0x38d7ea4c68000L) != 0)
            {
                this.FlagIntegerQuadrillion = (value % 0x38d7ea4c68000L) == 0L;
                this.GenerateQuadrillionDigits(digits, value / 0x38d7ea4c68000L);
            }
            value = value % 0x38d7ea4c68000L;
            if ((value / 0xe8d4a51000L) != 0)
            {
                this.FlagIntegerTrillion = (value % 0xe8d4a51000L) == 0L;
                this.GenerateTrillionDigits(digits, value / 0xe8d4a51000L);
            }
            value = value % 0xe8d4a51000L;
            if ((value / 0x3b9aca00L) != 0)
            {
                this.FlagIntegerBillion = (value % 0x3b9aca00L) == 0L;
                this.GenerateBillionDigits(digits, value / 0x3b9aca00L);
                value = value % 0x3b9aca00L;
            }
            if ((value / 0xf4240L) != 0)
            {
                this.FlagIntegerMillion = (value % 0xf4240L) == 0L;
                this.GenerateMillionDigits(digits, value / 0xf4240L);
            }
            value = value % 0xf4240L;
            if ((value / 0x3e8L) != 0)
            {
                this.FlagIntegerThousand = (value % 0x3e8L) == 0L;
                this.GenerateThousandDigits(digits, value / 0x3e8L);
            }
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

        protected internal bool FlagIntegerThousand { get; set; }

        protected internal bool FlagIntegerMillion { get; set; }

        protected internal bool FlagIntegerBillion { get; set; }

        protected internal bool FlagIntegerTrillion { get; set; }

        protected internal bool FlagIntegerQuadrillion { get; set; }

        protected internal bool FlagIntegerQuintillion { get; set; }
    }
}

