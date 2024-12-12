namespace DevExpress.Office.NumberConverters
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class SlavicOrdinalBase : SlavicBase
    {
        protected SlavicOrdinalBase()
        {
        }

        private bool CheckTypeIntegerBillion(DigitType type) => 
            (type == DigitType.Trillion) || ((type == DigitType.Quadrillion) || (type == DigitType.Quintillion));

        private bool CheckTypeIntegerMillion(DigitType type) => 
            (type == DigitType.Billion) || ((type == DigitType.Trillion) || ((type == DigitType.Quadrillion) || (type == DigitType.Quintillion)));

        private bool CheckTypeIntegerQuadrillion(DigitType type) => 
            type == DigitType.Quintillion;

        private bool CheckTypeIntegerThousand(DigitType type) => 
            (type == DigitType.Million) || ((type == DigitType.Billion) || ((type == DigitType.Trillion) || ((type == DigitType.Quadrillion) || (type == DigitType.Quintillion))));

        private bool CheckTypeIntegerTrillion(DigitType type) => 
            (type == DigitType.Quadrillion) || (type == DigitType.Quintillion);

        public DigitInfo ChooseOrdinalProvider() => 
            !base.FlagIntegerMillion ? (!base.FlagIntegerBillion ? (!base.FlagIntegerTrillion ? (!base.FlagIntegerQuadrillion ? (!base.FlagIntegerQuintillion ? ((DigitInfo) new ThousandsDigitInfo(this.GenerateThousandProvider(), 0L)) : ((DigitInfo) new QuintillionDigitInfo(this.GenerateQuintillionProvider(), 0L))) : ((DigitInfo) new QuadrillionDigitInfo(this.GenerateQuadrillionProvider(), 0L))) : ((DigitInfo) new TrillionDigitInfo(this.GenerateTrillionProvider(), 0L))) : ((DigitInfo) new BillionDigitInfo(this.GenerateBillionProvider(), 0L))) : ((DigitInfo) new MillionDigitInfo(this.GenerateMillionProvider(), 0L));

        protected internal override void GenerateBillionDigits(DigitInfoCollection digits, long value)
        {
            if (!base.FlagIntegerBillion)
            {
                base.GenerateBillionDigits(digits, value);
            }
            else
            {
                this.GenerateIntegerLastDigit(digits, value);
            }
        }

        protected internal override void GenerateDigits(DigitInfoCollection digits, long value)
        {
            base.GenerateDigits(digits, value);
            if (base.FlagIntegerThousand)
            {
                this.GenerateDigitsCore(digits, new CheckTypeDelegate(this.CheckTypeIntegerThousand));
            }
            else if (base.FlagIntegerMillion)
            {
                this.GenerateDigitsCore(digits, new CheckTypeDelegate(this.CheckTypeIntegerMillion));
            }
            else if (base.FlagIntegerBillion)
            {
                this.GenerateDigitsCore(digits, new CheckTypeDelegate(this.CheckTypeIntegerBillion));
            }
            else if (base.FlagIntegerTrillion)
            {
                this.GenerateDigitsCore(digits, new CheckTypeDelegate(this.CheckTypeIntegerTrillion));
            }
            else if (base.FlagIntegerQuadrillion)
            {
                this.GenerateDigitsCore(digits, new CheckTypeDelegate(this.CheckTypeIntegerQuadrillion));
            }
            else if (base.FlagIntegerQuintillion)
            {
                for (int i = 0; i <= (digits.Count - 1); i++)
                {
                    digits[i].Provider = this.GetProvider();
                }
            }
            digits.Last.Provider = this.GetOrdinalSlavicNumericsProvider();
        }

        private void GenerateDigitsCore(DigitInfoCollection digits, CheckTypeDelegate checkType)
        {
            for (int i = digits.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    digits[i].Provider = this.GetProvider();
                }
                else
                {
                    DigitType type = digits[i - 1].Type;
                    if (checkType(type))
                    {
                        break;
                    }
                    digits[i].Provider = this.GetProvider();
                }
            }
        }

        protected void GenerateIntegerLastDigit(DigitInfoCollection digits, long value)
        {
            if (value == 1L)
            {
                digits.Add(new SeparatorDigitInfo(new OrdinalRussianNumericsProvider(), 0L));
                digits.Add(this.ChooseOrdinalProvider());
            }
            else
            {
                this.GenerateDigitsInfo(digits, value);
                digits.Add(this.ChooseOrdinalProvider());
            }
        }

        protected internal override void GenerateMillionDigits(DigitInfoCollection digits, long value)
        {
            if (!base.FlagIntegerMillion)
            {
                base.GenerateMillionDigits(digits, value);
            }
            else
            {
                this.GenerateIntegerLastDigit(digits, value);
            }
        }

        protected internal override void GenerateQuadrillionDigits(DigitInfoCollection digits, long value)
        {
            if (!base.FlagIntegerQuadrillion)
            {
                base.GenerateQuadrillionDigits(digits, value);
            }
            else
            {
                this.GenerateIntegerLastDigit(digits, value);
            }
        }

        protected internal override void GenerateQuintillionDigits(DigitInfoCollection digits, long value)
        {
            if (!base.FlagIntegerQuintillion)
            {
                base.GenerateQuintillionDigits(digits, value);
            }
            else
            {
                this.GenerateIntegerLastDigit(digits, value);
            }
        }

        protected internal override void GenerateSinglesDigits(DigitInfoCollection digits, long value)
        {
            if ((digits.Count != 0) || ((value != 1L) || (!base.FlagIntegerThousand && (!base.FlagIntegerMillion && (!base.FlagIntegerBillion && (!base.FlagIntegerTrillion && (!base.FlagIntegerQuadrillion && !base.FlagIntegerQuintillion)))))))
            {
                base.GenerateSinglesDigits(digits, value);
            }
        }

        protected internal override void GenerateThousandDigits(DigitInfoCollection digits, long value)
        {
            if (!base.FlagIntegerThousand)
            {
                base.GenerateThousandDigits(digits, value);
            }
            else
            {
                this.GenerateIntegerLastDigit(digits, value);
            }
        }

        protected internal override void GenerateTrillionDigits(DigitInfoCollection digits, long value)
        {
            if (!base.FlagIntegerTrillion)
            {
                base.GenerateTrillionDigits(digits, value);
            }
            else
            {
                this.GenerateIntegerLastDigit(digits, value);
            }
        }

        protected internal abstract INumericsProvider GetOrdinalSlavicNumericsProvider();
        protected internal abstract INumericsProvider GetProvider();

        private delegate bool CheckTypeDelegate(DigitType type);
    }
}

