namespace DevExpress.Office.NumberConverters
{
    using System;

    public class SingleNumeralDigitInfo : DigitInfo
    {
        public SingleNumeralDigitInfo(INumericsProvider provider, long number) : base(provider, number - 1L)
        {
        }

        protected internal override string[] GetNumerics() => 
            base.Provider.SinglesNumeral;

        public override DigitType Type =>
            DigitType.SingleNumeral;
    }
}

