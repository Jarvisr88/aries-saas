namespace DevExpress.Office.NumberConverters
{
    using System;

    public class ThousandsDigitInfo : DigitInfo
    {
        public ThousandsDigitInfo(INumericsProvider provider, long number) : base(provider, number)
        {
        }

        protected internal override string[] GetNumerics() => 
            base.Provider.Thousands;

        public override DigitType Type =>
            DigitType.Thousand;
    }
}

