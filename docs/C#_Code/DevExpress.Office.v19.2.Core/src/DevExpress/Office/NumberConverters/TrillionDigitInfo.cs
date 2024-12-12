namespace DevExpress.Office.NumberConverters
{
    using System;

    public class TrillionDigitInfo : DigitInfo
    {
        public TrillionDigitInfo(INumericsProvider provider, long number) : base(provider, number)
        {
        }

        protected internal override string[] GetNumerics() => 
            base.Provider.Trillion;

        public override DigitType Type =>
            DigitType.Trillion;
    }
}

