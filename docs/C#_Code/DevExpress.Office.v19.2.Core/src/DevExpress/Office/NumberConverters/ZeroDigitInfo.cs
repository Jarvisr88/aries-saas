namespace DevExpress.Office.NumberConverters
{
    using System;

    public class ZeroDigitInfo : DigitInfo
    {
        public ZeroDigitInfo(INumericsProvider provider) : base(provider, (long) 9)
        {
        }

        protected internal override string[] GetNumerics() => 
            base.Provider.Singles;

        public override DigitType Type =>
            DigitType.Zero;
    }
}

