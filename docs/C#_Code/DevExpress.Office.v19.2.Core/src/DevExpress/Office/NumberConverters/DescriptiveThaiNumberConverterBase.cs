namespace DevExpress.Office.NumberConverters
{
    using System;

    public abstract class DescriptiveThaiNumberConverterBase : DescriptiveNumberConverterBase
    {
        protected internal const long maxThaiValue = 0x2386f26fc10000L;

        protected DescriptiveThaiNumberConverterBase()
        {
        }

        protected internal override void GenerateBillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagBillion = true;
            if (value >= 0x186a0L)
            {
                this.GenerateHundredDigits(digits, value / 0x186a0L);
                if ((value % 0x186a0L) == 0)
                {
                    digits.Add(new MillionDigitInfo(this.GenerateMillionProvider(), 0L));
                }
                value = value % 0x186a0L;
            }
            if ((value >= 0x2710L) && (value < 0x186a0L))
            {
                this.GenerateTenthsDigits(digits, value / 0x3e8L);
                if (((value / 0x3e8L) % ((long) 10)) != 0)
                {
                    digits.Add(new ThousandsDigitInfo(new CardinalThaiOptionalNumericsProvider(), 2L));
                }
                if (((value % 0x3e8L) == 0) || ((value % 0x2710L) == 0))
                {
                    digits.Add(new MillionDigitInfo(this.GenerateMillionProvider(), 0L));
                }
            }
            if ((value >= 0x3e8L) && (value < 0x2710L))
            {
                if ((value % 0x3e8L) == 0)
                {
                    base.GenerateBillionDigits(digits, value / 0x3e8L);
                }
                else
                {
                    this.GenerateSinglesDigits(digits, value / 0x3e8L);
                    digits.Add(new ThousandsDigitInfo(new CardinalThaiOptionalNumericsProvider(), 2L));
                }
            }
            base.FlagBillion = false;
        }

        protected internal override INumericsProvider GenerateBillionProvider() => 
            new CardinalThaiNumericsProvider();

        protected internal override void GenerateDigitsInfo(DigitInfoCollection digits, long value)
        {
            if ((value / 0xde0b6b3a7640000L) != 0)
            {
                this.GenerateQuintillionDigits(digits, value / 0xf4240L);
            }
            value = value % 0xde0b6b3a7640000L;
            if ((value / 0x38d7ea4c68000L) != 0)
            {
                this.GenerateQuadrillionDigits(digits, value / 0xf4240L);
            }
            value = value % 0x38d7ea4c68000L;
            if ((value / 0xe8d4a51000L) != 0)
            {
                this.GenerateTrillionDigits(digits, value / 0xf4240L);
            }
            value = value % 0xe8d4a51000L;
            if ((value / 0x3b9aca00L) != 0)
            {
                this.GenerateBillionDigits(digits, value / 0xf4240L);
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

        protected internal override void GenerateHundredDigits(DigitInfoCollection digits, long value)
        {
            if (!base.FlagThousand && (!base.FlagBillion && !base.FlagQuadrillion))
            {
                base.GenerateHundredDigits(digits, value);
            }
            else
            {
                digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value));
                digits.Add(new ThousandsDigitInfo(new CardinalThaiOptionalNumericsProvider(), 1L));
            }
        }

        protected internal override INumericsProvider GenerateHundredProvider() => 
            new CardinalThaiNumericsProvider();

        protected internal override INumericsProvider GenerateMillionProvider() => 
            new CardinalThaiNumericsProvider();

        protected internal override void GenerateQuadrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuadrillion = true;
            if (value >= 0x174876e800L)
            {
                this.GenerateHundredDigits(digits, value / 0x174876e800L);
                if ((value % 0x174876e800L) == 0)
                {
                    digits.Add(new TrillionDigitInfo(this.GenerateTrillionProvider(), 0L));
                }
                value = value % 0x174876e800L;
            }
            if ((value >= 0x2540be400L) && (value < 0x174876e800L))
            {
                this.GenerateTenthsDigits(digits, value / 0x3b9aca00L);
                if ((value % 0x2540be400L) == 0)
                {
                    digits.Add(new TrillionDigitInfo(this.GenerateQuadrillionProvider(), 0L));
                }
                else if ((value % 0x3b9aca00L) == 0)
                {
                    digits.Add(new QuadrillionDigitInfo(this.GenerateQuadrillionProvider(), 0L));
                }
                else if (((value / 0x3b9aca00L) % ((long) 10)) == 0)
                {
                    if (((value / 0xf4240L) % 0x3e8L) != 0)
                    {
                        base.FlagQuadrillion = false;
                        return;
                    }
                    digits.Add(new MillionDigitInfo(this.GenerateMillionProvider(), 0L));
                }
                else if ((((value / 0x3e8L) % 0xf4240L) != 0) && (((value / 0xf4240L) % 0x3e8L) != 0))
                {
                    digits.Add(new ThousandsDigitInfo(new CardinalThaiOptionalNumericsProvider(), 2L));
                }
                else
                {
                    digits.Add(new BillionDigitInfo(this.GenerateBillionProvider(), 0L));
                }
            }
            if ((value >= 0x3b9aca00L) && (value < 0x2540be400L))
            {
                if ((value % 0x3b9aca00L) == 0)
                {
                    base.GenerateQuadrillionDigits(digits, value / 0x3b9aca00L);
                }
                else
                {
                    this.GenerateSinglesDigits(digits, value / 0x3b9aca00L);
                    digits.Add(new ThousandsDigitInfo(new CardinalThaiOptionalNumericsProvider(), 2L));
                    if (((value % 0x3b9aca00L) != 0) && (((value % 0x3b9aca00L) / 0xf4240L) == 0))
                    {
                        digits.Add(new MillionDigitInfo(this.GenerateMillionProvider(), 0L));
                    }
                }
            }
            base.FlagQuadrillion = false;
        }

        protected internal override INumericsProvider GenerateQuadrillionProvider() => 
            new CardinalThaiNumericsProvider();

        protected internal override void GenerateQuintillionDigits(DigitInfoCollection digits, long value)
        {
            if ((value % 0xe8d4a51000L) == 0)
            {
                base.GenerateQuintillionDigits(digits, value / 0xe8d4a51000L);
            }
            else
            {
                this.GenerateSinglesDigits(digits, value / 0xe8d4a51000L);
                if ((((value / 0xf4240L) % 0x3e8L) != 0) || (((value / 0x3b9aca00L) % 0x3e8L) != 0))
                {
                    digits.Add(new MillionDigitInfo(this.GenerateMillionProvider(), 0L));
                }
                else if ((value % 0xf4240L) != 0)
                {
                    digits.Add(new TrillionDigitInfo(this.GenerateTrillionProvider(), 0L));
                }
            }
        }

        protected internal override INumericsProvider GenerateQuintillionProvider() => 
            new CardinalThaiNumericsProvider();

        protected internal override void GenerateSinglesDigits(DigitInfoCollection digits, long value)
        {
            if ((digits.Count != 0) && ((value == 1L) && !base.FlagThousand))
            {
                digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), (long) 11));
            }
            else
            {
                base.GenerateSinglesDigits(digits, value);
            }
        }

        protected internal override INumericsProvider GenerateSinglesProvider() => 
            new CardinalThaiNumericsProvider();

        protected internal override void GenerateTeensDigits(DigitInfoCollection digits, long value)
        {
            if (!base.FlagThousand && !base.FlagBillion)
            {
                base.GenerateTeensDigits(digits, value);
            }
            else
            {
                digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), 1L));
                digits.Add(new ThousandsDigitInfo(new CardinalThaiOptionalNumericsProvider(), 0L));
                if ((value % ((long) 10)) != 0)
                {
                    digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value % ((long) 10)));
                }
            }
        }

        protected internal override INumericsProvider GenerateTeensProvider() => 
            new CardinalThaiNumericsProvider();

        protected internal override void GenerateTenthsDigits(DigitInfoCollection digits, long value)
        {
            if (!base.FlagThousand && (!base.FlagBillion && !base.FlagQuadrillion))
            {
                base.GenerateTenthsDigits(digits, value);
            }
            else
            {
                digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value / ((long) 10)));
                digits.Add(new ThousandsDigitInfo(new CardinalThaiOptionalNumericsProvider(), 0L));
                if ((value % ((long) 10)) != 0)
                {
                    digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value % ((long) 10)));
                }
            }
        }

        protected internal override INumericsProvider GenerateTenthsProvider() => 
            new CardinalThaiNumericsProvider();

        protected internal override void GenerateThousandDigits(DigitInfoCollection digits, long value)
        {
            base.FlagThousand = true;
            if ((value >= 100) && ((value % ((long) 10)) == 0))
            {
                this.GenerateHundredDigits(digits, value / ((long) 100));
                if ((value % ((long) 100)) != 0)
                {
                    this.GenerateThousandDigits(digits, value % ((long) 100));
                }
            }
            else if ((value < 100) && ((value % ((long) 10)) == 0))
            {
                this.GenerateTenthsDigits(digits, value % ((long) 100));
            }
            else
            {
                base.GenerateThousandDigits(digits, value);
            }
            base.FlagThousand = false;
        }

        protected internal override INumericsProvider GenerateThousandProvider() => 
            new CardinalThaiNumericsProvider();

        protected internal override void GenerateTrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagTrillion = true;
            if ((value % 0xf4240L) == 0)
            {
                base.GenerateTrillionDigits(digits, value / 0xf4240L);
            }
            else
            {
                if (value >= 0x5f5e100L)
                {
                    this.GenerateHundredDigits(digits, value / 0x5f5e100L);
                    value = value % 0x5f5e100L;
                }
                if ((value > 0x1312d00L) && (value < 0x5f5e100L))
                {
                    this.GenerateTenthsDigits(digits, value / 0xf4240L);
                }
                if ((value >= 0x989680L) && (value < 0x1312d00L))
                {
                    digits.Add(new TeensDigitInfo(this.GenerateTeensProvider(), (value % 0x989680L) / 0xf4240L));
                }
                if (value < 0x989680L)
                {
                    this.GenerateSinglesDigits(digits, value / 0xf4240L);
                }
                digits.Add(new MillionDigitInfo(this.GenerateMillionProvider(), 0L));
            }
            base.FlagTrillion = false;
        }

        protected internal override INumericsProvider GenerateTrillionProvider() => 
            new CardinalThaiNumericsProvider();
    }
}

