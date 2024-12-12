namespace DevExpress.Office.NumberConverters
{
    using System;

    public class MillionDigitInfo : DigitInfo
    {
        public MillionDigitInfo(INumericsProvider provider, long number) : base(provider, number)
        {
        }

        protected internal override string[] GetNumerics() => 
            base.Provider.Million;

        public override DigitType Type =>
            DigitType.Million;
    }
}

