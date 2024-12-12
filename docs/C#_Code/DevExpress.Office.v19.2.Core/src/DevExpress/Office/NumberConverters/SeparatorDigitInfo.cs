namespace DevExpress.Office.NumberConverters
{
    using System;

    public class SeparatorDigitInfo : DigitInfo
    {
        public SeparatorDigitInfo(INumericsProvider provider, long number) : base(provider, number)
        {
        }

        protected internal override string[] GetNumerics() => 
            base.Provider.Separator;

        public override DigitType Type =>
            DigitType.Separator;
    }
}

