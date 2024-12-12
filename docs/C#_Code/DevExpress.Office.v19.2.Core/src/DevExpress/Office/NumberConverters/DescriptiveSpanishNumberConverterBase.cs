namespace DevExpress.Office.NumberConverters
{
    using System;

    public abstract class DescriptiveSpanishNumberConverterBase : DescriptiveNumberConverterBase
    {
        private bool oneHundredValue;
        private bool hundredOneValue;

        protected DescriptiveSpanishNumberConverterBase()
        {
        }

        protected internal override void GenerateDigitsInfo(DigitInfoCollection digits, long value)
        {
            long num = value % 0x3e8L;
            this.oneHundredValue = ((num / ((long) 100)) == 1L) && ((num % ((long) 100)) == 0L);
            this.hundredOneValue = ((num / ((long) 100)) == 1L) && ((num % ((long) 100)) == 1L);
            base.GenerateDigitsInfo(digits, value);
        }

        protected internal override void GenerateThousandSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (value == 1L)
            {
                digits.Add(new SeparatorDigitInfo(provider, 2L));
            }
            else
            {
                base.GenerateThousandSeparator(digits, provider, value);
            }
        }

        protected internal bool OneHundredValue =>
            this.oneHundredValue;

        protected internal bool HundredOneValue =>
            this.hundredOneValue;
    }
}

