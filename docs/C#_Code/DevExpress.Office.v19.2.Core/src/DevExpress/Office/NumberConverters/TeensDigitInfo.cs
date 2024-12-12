namespace DevExpress.Office.NumberConverters
{
    using System;

    public class TeensDigitInfo : DigitInfo
    {
        public TeensDigitInfo(INumericsProvider provider, long number) : base(provider, number)
        {
        }

        protected internal override string[] GetNumerics() => 
            base.Provider.Teens;

        public override DigitType Type =>
            DigitType.Teen;
    }
}

