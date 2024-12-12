namespace DevExpress.Office.NumberConverters
{
    using System;

    public abstract class DescriptiveEnglishUKNumberConverterBase : DescriptiveNumberConverterBase
    {
        protected DescriptiveEnglishUKNumberConverterBase()
        {
        }

        private void GenerateCustomSeparator(DigitInfoCollection digits)
        {
            INumericsProvider provider = new EnglishUKOptionalNumericsProvider();
            digits.Add(new SeparatorDigitInfo(provider, 0L));
        }

        protected internal override void GenerateSinglesSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (this.ShouldAddCustomSeparator(digits))
            {
                this.GenerateCustomSeparator(digits);
            }
            else
            {
                base.GenerateSinglesSeparator(digits, provider, value);
            }
        }

        protected internal override void GenerateTeensSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (this.ShouldAddCustomSeparator(digits))
            {
                this.GenerateCustomSeparator(digits);
            }
            else
            {
                base.GenerateTeensSeparator(digits, provider, value);
            }
        }

        protected internal override void GenerateTenthsSeparator(DigitInfoCollection digits, INumericsProvider provider)
        {
            if (this.ShouldAddCustomSeparator(digits))
            {
                this.GenerateCustomSeparator(digits);
            }
            else
            {
                base.GenerateTenthsSeparator(digits, provider);
            }
        }

        private bool ShouldAddCustomSeparator(DigitInfoCollection digits)
        {
            if (digits.Count == 0)
            {
                return false;
            }
            DigitType type = digits.Last.Type;
            return ((type == DigitType.Hundred) || (!base.IsValueGreaterHundred && (type != DigitType.Tenth)));
        }
    }
}

