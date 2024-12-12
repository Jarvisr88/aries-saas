namespace DevExpress.Office.NumberConverters
{
    using System;

    public class QuintillionDigitInfo : DigitInfo
    {
        public QuintillionDigitInfo(INumericsProvider provider, long number) : base(provider, number)
        {
        }

        protected internal override string[] GetNumerics() => 
            base.Provider.Quintillion;

        public override DigitType Type =>
            DigitType.Quintillion;
    }
}

