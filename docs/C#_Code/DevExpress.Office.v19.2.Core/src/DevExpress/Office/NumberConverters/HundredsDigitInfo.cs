namespace DevExpress.Office.NumberConverters
{
    using System;

    public class HundredsDigitInfo : DigitInfo
    {
        public HundredsDigitInfo(INumericsProvider provider, long number) : base(provider, number - 1L)
        {
        }

        protected internal override string[] GetNumerics() => 
            base.Provider.Hundreds;

        public override DigitType Type =>
            DigitType.Hundred;
    }
}

