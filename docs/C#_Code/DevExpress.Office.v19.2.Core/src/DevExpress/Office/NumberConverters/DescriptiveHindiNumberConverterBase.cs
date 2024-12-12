namespace DevExpress.Office.NumberConverters
{
    using System;

    public abstract class DescriptiveHindiNumberConverterBase : DescriptiveNumberConverterBase
    {
        protected DescriptiveHindiNumberConverterBase()
        {
        }

        protected internal override void AddZero(DigitInfoCollection digits)
        {
            digits.Add(new SingleNumeralDigitInfo(this.GenerateSinglesProvider(), 1L));
        }

        protected internal override void GenerateBillionDigits(DigitInfoCollection digits, long value)
        {
            if (digits.Count != 0)
            {
                if ((value % 0x989680L) == 0)
                {
                    return;
                }
                value = value % 0x9184e72a000L;
            }
            if (value >= 0x174876e800L)
            {
                if (digits.Count != 0)
                {
                    this.GenerateSinglesSeparator(digits, this.GenerateSinglesProvider(), 0L);
                }
                digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value / 0x174876e800L));
                this.GenerateSinglesSeparator(digits, this.GenerateSinglesProvider(), 0L);
                digits.Add(new BillionDigitInfo(this.GenerateBillionProvider(), 1L));
                value = value % 0x174876e800L;
                if ((value % 0x174876e800L) == 0)
                {
                    return;
                }
            }
            if ((value >= 0x3b9aca00L) && (value < 0x174876e800L))
            {
                if (digits.Count != 0)
                {
                    this.GenerateSinglesSeparator(digits, this.GenerateSinglesProvider(), 0L);
                }
                digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value / 0x3b9aca00L));
                this.GenerateThousandSeparator(digits, this.GenerateThousandProvider(), value);
                digits.Add(new BillionDigitInfo(this.GenerateBillionProvider(), 0L));
            }
        }

        protected internal override INumericsProvider GenerateBillionProvider() => 
            new CardinalHindiNumericsProvider();

        protected internal override void GenerateDigitsInfo(DigitInfoCollection digits, long value)
        {
            if ((value / 0xde0b6b3a7640000L) != 0)
            {
                this.GenerateQuintillionDigits(digits, value);
            }
            if ((value / 0x38d7ea4c68000L) != 0)
            {
                this.GenerateQuadrillionDigits(digits, value);
            }
            if ((value / 0xe8d4a51000L) != 0)
            {
                this.GenerateTrillionDigits(digits, value);
            }
            if ((value / 0x3b9aca00L) != 0)
            {
                this.GenerateBillionDigits(digits, value);
            }
            if ((value / 0xf4240L) != 0)
            {
                this.GenerateMillionDigits(digits, value);
            }
            if ((value / 0x3e8L) != 0)
            {
                this.GenerateThousandDigits(digits, value);
            }
            value = value % 0x3e8L;
            if ((value / ((long) 100)) != 0)
            {
                this.GenerateHundredDigits(digits, value / ((long) 100));
            }
            value = value % ((long) 100);
            if ((value != 0) && (value > 0L))
            {
                this.GenerateSinglesDigits(digits, value);
            }
        }

        protected internal override void GenerateHundredDigits(DigitInfoCollection digits, long value)
        {
            if (digits.Count != 0)
            {
                digits.Add(new SeparatorDigitInfo(this.GenerateSinglesProvider(), 0L));
            }
            if (value < 10)
            {
                digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value));
            }
            this.GenerateHundredSeparator(digits, this.GenerateHundredProvider());
            digits.Add(new HundredsDigitInfo(this.GenerateHundredProvider(), 1L));
        }

        protected internal override INumericsProvider GenerateHundredProvider() => 
            new CardinalHindiNumericsProvider();

        protected internal override void GenerateMillionDigits(DigitInfoCollection digits, long value)
        {
            if (digits.Count != 0)
            {
                if ((value % 0x3b9aca00L) == 0)
                {
                    return;
                }
                value = value % 0x3b9aca00L;
            }
            if (value >= 0x989680L)
            {
                if (digits.Count != 0)
                {
                    this.GenerateSinglesSeparator(digits, this.GenerateSinglesProvider(), 0L);
                }
                digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value / 0x989680L));
                this.GenerateSinglesSeparator(digits, this.GenerateSinglesProvider(), 0L);
                digits.Add(new MillionDigitInfo(this.GenerateMillionProvider(), 0L));
            }
        }

        protected internal override INumericsProvider GenerateMillionProvider() => 
            new CardinalHindiNumericsProvider();

        protected internal override void GenerateQuadrillionDigits(DigitInfoCollection digits, long value)
        {
            if (value >= 0x16345785d8a0000L)
            {
                if (digits.Count != 0)
                {
                    this.GenerateSinglesSeparator(digits, this.GenerateSinglesProvider(), 0L);
                }
                digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value / 0x16345785d8a0000L));
                this.GenerateSinglesSeparator(digits, this.GenerateSinglesProvider(), 0L);
                digits.Add(new QuadrillionDigitInfo(this.GenerateQuadrillionProvider(), 1L));
                value = value % 0x16345785d8a0000L;
                if ((value % 0x16345785d8a0000L) == 0)
                {
                    return;
                }
            }
            if ((value >= 0x38d7ea4c68000L) && (value < 0x16345785d8a0000L))
            {
                if (digits.Count != 0)
                {
                    this.GenerateSinglesSeparator(digits, this.GenerateSinglesProvider(), 0L);
                }
                digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value / 0x38d7ea4c68000L));
                this.GenerateThousandSeparator(digits, this.GenerateThousandProvider(), value);
                digits.Add(new QuadrillionDigitInfo(this.GenerateQuadrillionProvider(), 0L));
            }
        }

        protected internal override INumericsProvider GenerateQuadrillionProvider() => 
            new CardinalHindiNumericsProvider();

        protected internal override void GenerateQuintillionDigits(DigitInfoCollection digits, long value)
        {
        }

        protected internal override INumericsProvider GenerateQuintillionProvider() => 
            new CardinalHindiNumericsProvider();

        protected internal override INumericsProvider GenerateSinglesProvider() => 
            new CardinalHindiNumericsProvider();

        protected internal override INumericsProvider GenerateTeensProvider() => 
            new CardinalHindiNumericsProvider();

        protected internal override INumericsProvider GenerateTenthsProvider() => 
            new CardinalHindiNumericsProvider();

        protected internal override void GenerateThousandDigits(DigitInfoCollection digits, long value)
        {
            if (digits.Count != 0)
            {
                if ((value % 0x989680L) == 0)
                {
                    return;
                }
                value = value % 0x989680L;
            }
            if (value >= 0x186a0L)
            {
                if (digits.Count != 0)
                {
                    this.GenerateSinglesSeparator(digits, this.GenerateSinglesProvider(), 0L);
                }
                digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value / 0x186a0L));
                this.GenerateSinglesSeparator(digits, this.GenerateSinglesProvider(), 0L);
                digits.Add(new HundredsDigitInfo(this.GenerateHundredProvider(), 2L));
                value = value % 0x186a0L;
                if ((value % 0x186a0L) == 0)
                {
                    return;
                }
            }
            if ((value >= 0x3e8L) && (value < 0x186a0L))
            {
                if (digits.Count != 0)
                {
                    this.GenerateSinglesSeparator(digits, this.GenerateSinglesProvider(), 0L);
                }
                digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value / 0x3e8L));
                this.GenerateThousandSeparator(digits, this.GenerateThousandProvider(), value);
                digits.Add(new ThousandsDigitInfo(this.GenerateThousandProvider(), 0L));
            }
        }

        protected internal override INumericsProvider GenerateThousandProvider() => 
            new CardinalHindiNumericsProvider();

        protected internal override void GenerateTrillionDigits(DigitInfoCollection digits, long value)
        {
            if (digits.Count != 0)
            {
                if ((value % 0x38d7ea4c68000L) == 0)
                {
                    return;
                }
                value = value % 0x38d7ea4c68000L;
            }
            if (value >= 0x9184e72a000L)
            {
                if (digits.Count != 0)
                {
                    this.GenerateSinglesSeparator(digits, this.GenerateSinglesProvider(), 0L);
                }
                digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value / 0x9184e72a000L));
                this.GenerateSinglesSeparator(digits, this.GenerateSinglesProvider(), 0L);
                digits.Add(new TrillionDigitInfo(this.GenerateTrillionProvider(), 0L));
            }
        }

        protected internal override INumericsProvider GenerateTrillionProvider() => 
            new CardinalHindiNumericsProvider();
    }
}

