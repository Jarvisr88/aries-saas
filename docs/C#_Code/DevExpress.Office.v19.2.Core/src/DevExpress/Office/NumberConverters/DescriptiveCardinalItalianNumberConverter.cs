namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveCardinalItalianNumberConverter : DescriptiveItalianNumberConverterBase
    {
        protected internal override void GenerateDigits(DigitInfoCollection digits, long value)
        {
            base.GenerateDigits(digits, value);
            if ((digits.Count > 1) && (digits.Last.Type == DigitType.Single))
            {
                digits.Last.Provider = new CardinalItalianOptionalNumericsProvider();
            }
        }

        protected internal override void GenerateHundredSeparator(DigitInfoCollection digits, INumericsProvider provider)
        {
            if ((digits.Count != 0) && (base.OneHundredValue && (base.FlagThousand && !base.FlagIntegerThousand)))
            {
                digits.Add(new SeparatorDigitInfo(this.GenerateHundredProvider(), 2L));
            }
            else
            {
                if ((digits.Count != 0) && base.OneHundredValue)
                {
                    digits.Add(new SeparatorDigitInfo(this.GenerateHundredProvider(), 3L));
                }
                else if ((digits.Count != 0) && base.IsDigitInfoGreaterThousand(digits.Last))
                {
                    digits.Add(new SeparatorDigitInfo(provider, 2L));
                }
                base.GenerateHundredSeparator(digits, provider);
            }
        }

        protected internal override void GenerateSinglesDigits(DigitInfoCollection digits, long value)
        {
            if ((value == 3L) && (base.IsValueGreaterThousand && (digits.Count != 0)))
            {
                digits.Add(new SingleNumeralDigitInfo(new CardinalItalianOptionalNumericsProvider(), value));
            }
            else if ((value != 1L) && ((digits.Count != 0) && base.IsDigitInfoGreaterThousand(digits.Last)))
            {
                digits.Add(new SeparatorDigitInfo(this.GenerateSinglesProvider(), 2L));
                digits.Add(new SingleNumeralDigitInfo(this.GenerateSinglesProvider(), value));
            }
            else if ((value == 1L) && ((digits.Count != 0) && ((digits.Last.Type == DigitType.Thousand) || (digits.Last.Type == DigitType.Million))))
            {
                digits.Add(new SeparatorDigitInfo(this.GenerateSinglesProvider(), 3L));
                digits.Add(new SingleNumeralDigitInfo(this.GenerateSinglesProvider(), value));
            }
            else if ((value == 1L) && base.IsValueGreaterThousand)
            {
                digits.Add(new SingleNumeralDigitInfo(this.GenerateSinglesProvider(), (long) 11));
            }
            else
            {
                base.GenerateSinglesDigits(digits, value);
            }
        }

        protected internal override void GenerateTeensSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if ((digits.Count != 0) && base.IsDigitInfoGreaterThousand(digits.Last))
            {
                digits.Add(new SeparatorDigitInfo(provider, 2L));
            }
            else
            {
                base.GenerateTeensSeparator(digits, provider, value);
            }
        }

        protected internal override void GenerateTenthsSeparator(DigitInfoCollection digits, INumericsProvider provider)
        {
            if ((digits.Count != 0) && base.IsDigitInfoGreaterThousand(digits.Last))
            {
                digits.Add(new SeparatorDigitInfo(provider, 2L));
            }
            else
            {
                base.GenerateTenthsSeparator(digits, provider);
            }
        }

        protected internal override void GenerateThousandSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if ((digits.Count != 0) && base.IsDigitInfoGreaterThousand(digits.Last))
            {
                digits.Add(new SeparatorDigitInfo(provider, 2L));
            }
            else
            {
                base.GenerateTenthsSeparator(digits, provider);
            }
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.CardinalText;
    }
}

