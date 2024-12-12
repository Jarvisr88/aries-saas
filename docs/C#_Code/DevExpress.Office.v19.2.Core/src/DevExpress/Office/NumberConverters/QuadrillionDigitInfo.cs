namespace DevExpress.Office.NumberConverters
{
    using System;

    public class QuadrillionDigitInfo : DigitInfo
    {
        public QuadrillionDigitInfo(INumericsProvider provider, long number) : base(provider, number)
        {
        }

        protected internal override string[] GetNumerics() => 
            base.Provider.Quadrillion;

        public override DigitType Type =>
            DigitType.Quadrillion;
    }
}

