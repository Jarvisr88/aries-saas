namespace DevExpress.Office.NumberConverters
{
    using System;

    public class BillionDigitInfo : DigitInfo
    {
        public BillionDigitInfo(INumericsProvider provider, long number) : base(provider, number)
        {
        }

        protected internal override string[] GetNumerics() => 
            base.Provider.Billion;

        public override DigitType Type =>
            DigitType.Billion;
    }
}

