namespace DevExpress.Office.NumberConverters
{
    using System;

    public class TenthsDigitInfo : DigitInfo
    {
        public TenthsDigitInfo(INumericsProvider provider, long number) : base(provider, number - 2L)
        {
        }

        protected internal override string[] GetNumerics() => 
            base.Provider.Tenths;

        public override DigitType Type =>
            DigitType.Tenth;
    }
}

