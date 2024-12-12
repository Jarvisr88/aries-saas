namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveOrdinalItalianNumberConverter : DescriptiveItalianNumberConverterBase
    {
        protected internal override void GenerateDigits(DigitInfoCollection digits, long value)
        {
            base.GenerateDigits(digits, value);
            if (digits.Count == 1)
            {
                digits.Last.Provider = new OrdinalItalianOptionalNumericsProvider();
            }
            else if ((value % 0x3e8L) > 10)
            {
                digits.Last.Provider = new OrdinalItalianNumericsProvider();
            }
        }

        protected internal override void GenerateHundredSeparator(DigitInfoCollection digits, INumericsProvider provider)
        {
            if ((digits.Count != 0) && base.IsDigitInfoGreaterHundred(digits.Last))
            {
                digits.Add(new SeparatorDigitInfo(new OrdinalItalianNumericsProvider(), 2L));
            }
            else
            {
                base.GenerateHundredSeparator(digits, provider);
            }
        }

        protected internal override void GenerateSinglesDigits(DigitInfoCollection digits, long value)
        {
            if ((digits.Count != 0) && ((digits.Last.Type == DigitType.Hundred) && ((value == 1L) || (value == 8L))))
            {
                digits.Last.Provider = new CardinalItalianOptionalNumericsProvider();
            }
            if ((value != 1L) || (!base.IsValueGreaterHundred || (digits.Count != 0)))
            {
                if ((value == 1L) && (base.IsValueGreaterThousand && (digits.Count != 0)))
                {
                    digits.Add(new SingleDigitInfo(new CardinalItalianNumericsProvider(), (long) 11));
                }
                else if ((digits.Count != 0) && ((value == 1L) && (base.IsValueGreaterHundred && base.IsDigitInfoGreaterHundred(digits.Last))))
                {
                    digits.Add(new SeparatorDigitInfo(new OrdinalItalianNumericsProvider(), 2L));
                }
                else if ((digits.Count != 0) && ((value != 1L) && (base.IsValueGreaterHundred && base.IsDigitInfoGreaterHundred(digits.Last))))
                {
                    digits.Add(new SeparatorDigitInfo(new OrdinalItalianNumericsProvider(), 2L));
                    digits.Add(new SingleDigitInfo(new CardinalItalianNumericsProvider(), value));
                }
                else if ((digits.Count == 0) || !base.IsDigitInfoGreaterHundred(digits.Last))
                {
                    base.GenerateSinglesDigits(digits, value);
                }
                else
                {
                    digits.Add(new SeparatorDigitInfo(new OrdinalItalianNumericsProvider(), 2L));
                    digits.Add(new SingleDigitInfo(new OrdinalItalianOptionalNumericsProvider(), value));
                }
            }
        }

        protected internal override void GenerateTeensSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if ((digits.Count != 0) && base.IsDigitInfoGreaterHundred(digits.Last))
            {
                digits.Add(new SeparatorDigitInfo(new OrdinalItalianNumericsProvider(), 2L));
            }
            else
            {
                base.GenerateTeensSeparator(digits, this.GenerateTeensProvider(), value);
            }
        }

        protected internal override void GenerateTenthsDigits(DigitInfoCollection digits, long value)
        {
            if ((digits.Count != 0) && ((digits.Last.Type == DigitType.Hundred) && ((value / ((long) 10)) == 8L)))
            {
                digits.Last.Provider = new CardinalItalianOptionalNumericsProvider();
            }
            base.GenerateTenthsDigits(digits, value);
        }

        protected internal override void GenerateTenthsSeparator(DigitInfoCollection digits, INumericsProvider provider)
        {
            if ((digits.Count != 0) && base.IsDigitInfoGreaterHundred(digits.Last))
            {
                digits.Add(new SeparatorDigitInfo(new OrdinalItalianNumericsProvider(), 2L));
            }
            else
            {
                base.GenerateTenthsSeparator(digits, provider);
            }
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.OrdinalText;
    }
}

