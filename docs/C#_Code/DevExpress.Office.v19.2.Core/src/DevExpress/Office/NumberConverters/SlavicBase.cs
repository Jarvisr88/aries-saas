namespace DevExpress.Office.NumberConverters
{
    using System;

    public abstract class SlavicBase : SomeLanguagesBased
    {
        protected SlavicBase()
        {
        }

        protected DigitInfo ChooseCardinalProvider(int number) => 
            !base.FlagMillion ? (!base.FlagBillion ? (!base.FlagTrillion ? (!base.FlagQuadrillion ? (!base.FlagQuintillion ? ((DigitInfo) new ThousandsDigitInfo(this.GenerateThousandProvider(), (long) number)) : ((DigitInfo) new QuintillionDigitInfo(this.GenerateQuintillionProvider(), (long) number))) : ((DigitInfo) new QuadrillionDigitInfo(this.GenerateQuadrillionProvider(), (long) number))) : ((DigitInfo) new TrillionDigitInfo(this.GenerateTrillionProvider(), (long) number))) : ((DigitInfo) new BillionDigitInfo(this.GenerateBillionProvider(), (long) number))) : ((DigitInfo) new MillionDigitInfo(this.GenerateMillionProvider(), (long) number));

        protected internal override void GenerateBillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagBillion = true;
            this.GenerateDigitsInfo(digits, value);
            this.GenerateBillionSeparator(digits, this.GenerateBillionProvider(), value);
            this.GenerateLastDigits(digits, value);
            base.FlagBillion = false;
        }

        protected void GenerateLastDigits(DigitInfoCollection digits, long value)
        {
            long num = value % ((long) 100);
            if ((num > 9) && (num < 0x15))
            {
                digits.Add(this.ChooseCardinalProvider(2));
            }
            else
            {
                num = num % ((long) 10);
                if (num == 1L)
                {
                    digits.Add(this.ChooseCardinalProvider(0));
                }
                if ((num > 1L) && (num < 5L))
                {
                    digits.Add(this.ChooseCardinalProvider(1));
                }
                if (((num > 4L) && (num <= 9)) || (num == 0))
                {
                    digits.Add(this.ChooseCardinalProvider(2));
                }
            }
        }

        protected internal override void GenerateMillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagMillion = true;
            this.GenerateDigitsInfo(digits, value);
            this.GenerateMillionSeparator(digits, this.GenerateMillionProvider(), value);
            this.GenerateLastDigits(digits, value);
            base.FlagMillion = false;
        }

        protected internal override void GenerateQuadrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuadrillion = true;
            this.GenerateDigitsInfo(digits, value);
            this.GenerateQuadrillionSeparator(digits, this.GenerateQuadrillionProvider(), value);
            this.GenerateLastDigits(digits, value);
            base.FlagQuadrillion = false;
        }

        protected internal override void GenerateQuintillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagQuintillion = true;
            this.GenerateDigitsInfo(digits, value);
            this.GenerateQuintillionSeparator(digits, this.GenerateQuintillionProvider(), value);
            this.GenerateLastDigits(digits, value);
            base.FlagQuintillion = false;
        }

        protected internal override void GenerateThousandDigits(DigitInfoCollection digits, long value)
        {
            base.FlagThousand = true;
            this.GenerateDigitsInfo(digits, value);
            this.GenerateThousandSeparator(digits, this.GenerateThousandProvider(), value);
            this.GenerateLastDigits(digits, value);
            base.FlagThousand = false;
        }

        protected internal override void GenerateTrillionDigits(DigitInfoCollection digits, long value)
        {
            base.FlagTrillion = true;
            this.GenerateDigitsInfo(digits, value);
            this.GenerateTrillionSeparator(digits, this.GenerateTrillionProvider(), value);
            this.GenerateLastDigits(digits, value);
            base.FlagTrillion = false;
        }
    }
}

