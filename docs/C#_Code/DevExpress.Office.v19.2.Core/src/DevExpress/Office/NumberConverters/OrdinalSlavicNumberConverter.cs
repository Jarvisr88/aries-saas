namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalSlavicNumberConverter : OrdinalBasedNumberConverter
    {
        public override string ConvertNumberCore(long value) => 
            $"{value}-й";

        protected internal override NumberingFormat Type =>
            NumberingFormat.Ordinal;
    }
}

