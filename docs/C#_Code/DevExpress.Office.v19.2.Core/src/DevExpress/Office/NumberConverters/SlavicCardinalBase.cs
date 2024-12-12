namespace DevExpress.Office.NumberConverters
{
    using System;

    public abstract class SlavicCardinalBase : SlavicBase
    {
        protected SlavicCardinalBase()
        {
        }

        protected internal override void GenerateHundredDigits(DigitInfoCollection digits, long value)
        {
            INumericsProvider provider = this.GenerateHundredProvider();
            this.GenerateHundredSeparator(digits, provider);
            digits.Add(new HundredsDigitInfo(provider, value));
        }
    }
}

