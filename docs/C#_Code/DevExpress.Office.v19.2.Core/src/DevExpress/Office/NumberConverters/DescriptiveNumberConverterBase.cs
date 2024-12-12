namespace DevExpress.Office.NumberConverters
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public abstract class DescriptiveNumberConverterBase : OrdinalBasedNumberConverter
    {
        protected DescriptiveNumberConverterBase()
        {
        }

        protected internal virtual void AddZero(DigitInfoCollection digits)
        {
            digits.Add(new ZeroDigitInfo(this.GenerateSinglesProvider()));
        }

        protected internal virtual string ConvertDigitsToString(DigitInfoCollection digits)
        {
            StringBuilder builder = new StringBuilder();
            int count = digits.Count;
            for (int i = 0; i < count; i++)
            {
                builder.Append(digits[i].ConvertToString());
            }
            if (builder.Length > 0)
            {
                builder[0] = char.ToUpper(builder[0]);
            }
            return builder.ToString();
        }

        public override string ConvertNumberCore(long value)
        {
            DigitInfoCollection digits = new DigitInfoCollection();
            this.GenerateDigits(digits, value);
            return this.ConvertDigitsToString(digits);
        }

        protected internal virtual void GenerateBillionDigits(DigitInfoCollection digits, long value)
        {
            this.FlagBillion = true;
            this.GenerateDigitsInfo(digits, value);
            this.GenerateBillionSeparator(digits, this.GenerateBillionProvider(), value);
            digits.Add(new BillionDigitInfo(this.GenerateBillionProvider(), 0L));
            this.FlagBillion = false;
        }

        protected internal virtual INumericsProvider GenerateBillionProvider() => 
            new CardinalEnglishNumericsProvider();

        protected internal virtual void GenerateBillionSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (digits.Count != 0)
            {
                digits.Add(new SeparatorDigitInfo(provider, 0L));
            }
        }

        protected internal virtual void GenerateDigits(DigitInfoCollection digits, long value)
        {
            this.GenerateDigitsInfo(digits, value);
            if (digits.Count == 0)
            {
                this.AddZero(digits);
            }
        }

        protected internal virtual void GenerateDigitsInfo(DigitInfoCollection digits, long value)
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

        protected internal virtual void GenerateHundredDigits(DigitInfoCollection digits, long value)
        {
            this.GenerateHundredSeparator(digits, this.GenerateHundredProvider());
            digits.Add(new HundredsDigitInfo(this.GenerateHundredProvider(), value));
        }

        protected internal virtual INumericsProvider GenerateHundredProvider() => 
            new CardinalEnglishNumericsProvider();

        protected internal virtual void GenerateHundredSeparator(DigitInfoCollection digits, INumericsProvider provider)
        {
            if (digits.Count != 0)
            {
                digits.Add(new SeparatorDigitInfo(provider, 0L));
            }
        }

        protected internal virtual void GenerateMillionDigits(DigitInfoCollection digits, long value)
        {
            this.FlagMillion = true;
            this.GenerateDigitsInfo(digits, value);
            this.GenerateMillionSeparator(digits, this.GenerateMillionProvider(), value);
            digits.Add(new MillionDigitInfo(this.GenerateMillionProvider(), 0L));
            this.FlagMillion = false;
        }

        protected internal virtual INumericsProvider GenerateMillionProvider() => 
            new CardinalEnglishNumericsProvider();

        protected internal virtual void GenerateMillionSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (digits.Count != 0)
            {
                digits.Add(new SeparatorDigitInfo(provider, 0L));
            }
        }

        protected internal virtual void GenerateQuadrillionDigits(DigitInfoCollection digits, long value)
        {
            this.FlagQuadrillion = true;
            this.GenerateDigitsInfo(digits, value);
            this.GenerateQuadrillionSeparator(digits, this.GenerateQuadrillionProvider(), value);
            digits.Add(new QuadrillionDigitInfo(this.GenerateQuadrillionProvider(), 0L));
            this.FlagQuadrillion = false;
        }

        protected internal virtual INumericsProvider GenerateQuadrillionProvider() => 
            new CardinalEnglishNumericsProvider();

        protected internal virtual void GenerateQuadrillionSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (digits.Count != 0)
            {
                digits.Add(new SeparatorDigitInfo(provider, 0L));
            }
        }

        protected internal virtual void GenerateQuintillionDigits(DigitInfoCollection digits, long value)
        {
            this.FlagQuintillion = true;
            this.GenerateDigitsInfo(digits, value);
            this.GenerateQuintillionSeparator(digits, this.GenerateQuintillionProvider(), value);
            digits.Add(new QuintillionDigitInfo(this.GenerateQuintillionProvider(), 0L));
            this.FlagQuintillion = false;
        }

        protected internal virtual INumericsProvider GenerateQuintillionProvider() => 
            new CardinalEnglishNumericsProvider();

        protected internal virtual void GenerateQuintillionSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (digits.Count != 0)
            {
                digits.Add(new SeparatorDigitInfo(provider, 0L));
            }
        }

        protected internal virtual void GenerateSinglesDigits(DigitInfoCollection digits, long value)
        {
            if (value != 0)
            {
                this.GenerateSinglesSeparator(digits, this.GenerateSinglesProvider(), value);
                if (this.FlagThousand)
                {
                    digits.Add(new SingleNumeralDigitInfo(this.GenerateSinglesProvider(), value));
                }
                else
                {
                    digits.Add(new SingleDigitInfo(this.GenerateSinglesProvider(), value));
                }
                this.FlagThousand = false;
            }
        }

        protected internal virtual INumericsProvider GenerateSinglesProvider() => 
            new CardinalEnglishNumericsProvider();

        protected internal virtual void GenerateSinglesSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (digits.Count != 0)
            {
                if (digits.Last.Type == DigitType.Tenth)
                {
                    digits.Add(new SeparatorDigitInfo(provider, 1L));
                }
                else
                {
                    digits.Add(new SeparatorDigitInfo(provider, 0L));
                }
            }
        }

        protected internal virtual void GenerateTeensDigits(DigitInfoCollection digits, long value)
        {
            this.GenerateTeensSeparator(digits, this.GenerateTeensProvider(), value);
            digits.Add(new TeensDigitInfo(this.GenerateTeensProvider(), value));
        }

        protected internal virtual INumericsProvider GenerateTeensProvider() => 
            new CardinalEnglishNumericsProvider();

        protected internal virtual void GenerateTeensSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (digits.Count != 0)
            {
                digits.Add(new SeparatorDigitInfo(provider, 0L));
            }
        }

        protected internal virtual void GenerateTenthsDigits(DigitInfoCollection digits, long value)
        {
            this.GenerateTenthsSeparator(digits, this.GenerateTenthsProvider());
            digits.Add(new TenthsDigitInfo(this.GenerateTenthsProvider(), value / ((long) 10)));
            this.GenerateSinglesDigits(digits, value % ((long) 10));
        }

        protected internal virtual INumericsProvider GenerateTenthsProvider() => 
            new CardinalEnglishNumericsProvider();

        protected internal virtual void GenerateTenthsSeparator(DigitInfoCollection digits, INumericsProvider provider)
        {
            if (digits.Count != 0)
            {
                digits.Add(new SeparatorDigitInfo(provider, 0L));
            }
        }

        protected internal virtual void GenerateThousandDigits(DigitInfoCollection digits, long value)
        {
            this.FlagThousand = true;
            this.GenerateDigitsInfo(digits, value);
            this.GenerateThousandSeparator(digits, this.GenerateThousandProvider(), value);
            digits.Add(new ThousandsDigitInfo(this.GenerateThousandProvider(), 0L));
            this.FlagThousand = false;
        }

        protected internal virtual INumericsProvider GenerateThousandProvider() => 
            new CardinalEnglishNumericsProvider();

        protected internal virtual void GenerateThousandSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (digits.Count != 0)
            {
                digits.Add(new SeparatorDigitInfo(provider, 0L));
            }
        }

        protected internal virtual void GenerateTrillionDigits(DigitInfoCollection digits, long value)
        {
            this.FlagTrillion = true;
            this.GenerateDigitsInfo(digits, value);
            this.GenerateTrillionSeparator(digits, this.GenerateTrillionProvider(), value);
            digits.Add(new TrillionDigitInfo(this.GenerateTrillionProvider(), 0L));
            this.FlagTrillion = false;
        }

        protected internal virtual INumericsProvider GenerateTrillionProvider() => 
            new CardinalEnglishNumericsProvider();

        protected internal virtual void GenerateTrillionSeparator(DigitInfoCollection digits, INumericsProvider provider, long value)
        {
            if (digits.Count != 0)
            {
                digits.Add(new SeparatorDigitInfo(provider, 0L));
            }
        }

        protected internal bool IsDigitInfoGreaterHundred(DigitInfo info) => 
            (info.Type == DigitType.Thousand) || this.IsDigitInfoGreaterThousand(info);

        protected internal bool IsDigitInfoGreaterThousand(DigitInfo info)
        {
            DigitType type = info.Type;
            return ((type == DigitType.Million) || ((type == DigitType.Billion) || ((type == DigitType.Trillion) || ((type == DigitType.Quadrillion) || (type == DigitType.Quintillion)))));
        }

        protected internal bool FlagThousand { get; set; }

        protected internal bool FlagMillion { get; set; }

        protected internal bool FlagBillion { get; set; }

        protected internal bool FlagTrillion { get; set; }

        protected internal bool FlagQuadrillion { get; set; }

        protected internal bool FlagQuintillion { get; set; }

        protected internal bool IsValueGreaterThousand =>
            this.FlagMillion || (this.FlagBillion || (this.FlagTrillion || (this.FlagQuadrillion || this.FlagQuintillion)));

        protected internal bool IsValueGreaterHundred =>
            this.FlagThousand || this.IsValueGreaterThousand;

        protected internal override long MinValue =>
            0L;
    }
}

