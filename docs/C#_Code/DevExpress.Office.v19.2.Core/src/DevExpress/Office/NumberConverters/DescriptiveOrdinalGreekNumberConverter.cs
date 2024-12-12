namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DescriptiveOrdinalGreekNumberConverter : DescriptiveGreekNumberConverterBase
    {
        protected internal override INumericsProvider GenerateBillionProvider() => 
            new OrdinalGreekNumericsProvider();

        protected internal override void GenerateBillionSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if ((digits.Count == 0) && (value == 1L))
            {
                digits.Add(new SeparatorDigitInfo(provider, 2L));
            }
            else
            {
                base.GenerateBillionSeparator(digits, provider, value);
            }
        }

        protected internal override void GenerateHundredDigits(DigitInfoCollection digits, long value)
        {
            if ((digits.Count == 0) || !base.FlagThousand)
            {
                base.GenerateHundredDigits(digits, value);
            }
            else
            {
                digits.Add(new SeparatorDigitInfo(this.GenerateHundredProvider(), 0L));
                digits.Add(new HundredsDigitInfo(this.GenerateHundredProvider(), value));
            }
        }

        protected internal override INumericsProvider GenerateHundredProvider() => 
            new OrdinalGreekNumericsProvider();

        protected internal override INumericsProvider GenerateMillionProvider() => 
            new OrdinalGreekNumericsProvider();

        protected internal override void GenerateMillionSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if ((digits.Count == 0) && (value == 1L))
            {
                digits.Add(new SeparatorDigitInfo(provider, 2L));
            }
            else
            {
                base.GenerateMillionSeparator(digits, provider, value);
            }
        }

        protected internal override INumericsProvider GenerateQuadrillionProvider() => 
            new OrdinalGreekNumericsProvider();

        protected internal override INumericsProvider GenerateQuintillionProvider() => 
            new OrdinalGreekNumericsProvider();

        protected internal override void GenerateSinglesDigits(DigitInfoCollection digits, long value)
        {
            if (base.IsValueGreaterHundred && ((digits.Count != 0) && base.IsDigitInfoGreaterThousand(digits.Last)))
            {
                digits.Add(new SeparatorDigitInfo(this.GenerateTenthsProvider(), 0L));
                digits.Add(new SingleNumeralDigitInfo(new OrdinalGreekOptionalNumericsProvider(), value));
            }
            else if ((digits.Count != 0) || !base.IsValueGreaterHundred)
            {
                base.GenerateSinglesDigits(digits, value);
            }
            else if (value != 1L)
            {
                digits.Add(new SingleNumeralDigitInfo(new OrdinalGreekOptionalNumericsProvider(), value));
            }
        }

        protected internal override INumericsProvider GenerateSinglesProvider() => 
            new OrdinalGreekNumericsProvider();

        protected internal override INumericsProvider GenerateTeensProvider() => 
            new OrdinalGreekNumericsProvider();

        protected internal override void GenerateTenthsDigits(DigitInfoCollection digits, long value)
        {
            if (!base.FlagThousand || ((value % ((long) 10)) != 0))
            {
                base.GenerateTenthsDigits(digits, value);
            }
            else
            {
                if (digits.Count != 0)
                {
                    digits.Add(new SeparatorDigitInfo(this.GenerateTenthsProvider(), 0L));
                }
                digits.Add(new TenthsDigitInfo(this.GenerateTenthsProvider(), value / ((long) 10)));
            }
        }

        protected internal override INumericsProvider GenerateTenthsProvider() => 
            new OrdinalGreekNumericsProvider();

        protected internal override INumericsProvider GenerateThousandProvider() => 
            new OrdinalGreekNumericsProvider();

        protected internal override void GenerateThousandSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if ((digits.Count != 0) && (value < 20))
            {
                digits.Add(new SeparatorDigitInfo(provider, 2L));
            }
            else
            {
                base.GenerateThousandSeparator(digits, provider, value);
            }
        }

        protected internal override INumericsProvider GenerateTrillionProvider() => 
            new OrdinalGreekNumericsProvider();

        protected internal override void GenerateTrillionSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if ((digits.Count != 0) && (value != 1L))
            {
                base.GenerateTrillionSeparator(digits, provider, value);
            }
        }

        protected internal override INumericsProvider GetTeensOptionProvider(long digitCount) => 
            (digitCount != 0) ? ((INumericsProvider) new OrdinalGreekNumericsProvider()) : ((INumericsProvider) new OrdinalGreekOptionalNumericsProvider());

        protected internal override NumberingFormat Type =>
            NumberingFormat.OrdinalText;
    }
}

