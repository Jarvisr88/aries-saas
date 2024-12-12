namespace DevExpress.Office.NumberConverters
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class DescriptiveGreekNumberConverterBase : DescriptiveNumberConverterBase
    {
        protected DescriptiveGreekNumberConverterBase()
        {
        }

        protected internal override void GenerateDigitsInfo(DigitInfoCollection digits, long value)
        {
            if ((value / 0xde0b6b3a7640000L) != 0)
            {
                this.GenerateQuintillionDigits(digits, value / 0xde0b6b3a7640000L);
            }
            value = value % 0xde0b6b3a7640000L;
            if ((value / 0x38d7ea4c68000L) != 0)
            {
                this.GenerateQuadrillionDigits(digits, value / 0x38d7ea4c68000L);
            }
            value = value % 0x38d7ea4c68000L;
            if ((value / 0xe8d4a51000L) != 0)
            {
                this.GenerateTrillionDigits(digits, value / 0xe8d4a51000L);
            }
            value = value % 0xe8d4a51000L;
            if ((value / 0x3b9aca00L) != 0)
            {
                this.GenerateBillionDigits(digits, value / 0x3b9aca00L);
            }
            value = value % 0x3b9aca00L;
            if ((value / 0xf4240L) != 0)
            {
                this.GenerateMillionDigits(digits, value / 0xf4240L);
            }
            value = value % 0xf4240L;
            if ((value / 0x3e8L) != 0)
            {
                this.GenerateThousandDigits(digits, value / 0x3e8L);
            }
            value = value % 0x3e8L;
            this.FlagOneFullHundred = ((value % ((long) 100)) == 0) && ((value / ((long) 100)) == 1L);
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

        protected internal override void GenerateTeensDigits(DigitInfoCollection digits, long value)
        {
            if (!base.FlagThousand)
            {
                base.GenerateTeensDigits(digits, value);
            }
            else
            {
                this.GenerateTeensSeparator(digits, this.GenerateTeensProvider(), value);
                digits.Add(new TeensDigitInfo(this.GetTeensOptionProvider((long) digits.Count), value % ((long) 10)));
            }
        }

        protected internal abstract INumericsProvider GetTeensOptionProvider(long digitCount);

        protected internal bool FlagOneFullHundred { get; set; }
    }
}

