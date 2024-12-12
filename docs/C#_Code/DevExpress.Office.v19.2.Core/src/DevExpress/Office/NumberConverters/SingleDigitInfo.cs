namespace DevExpress.Office.NumberConverters
{
    using System;

    public class SingleDigitInfo : DigitInfo
    {
        public SingleDigitInfo(INumericsProvider provider, long number) : base(provider, number - 1L)
        {
        }

        protected internal override string[] GetNumerics() => 
            base.Provider.Singles;

        public override DigitType Type =>
            DigitType.Single;
    }
}

