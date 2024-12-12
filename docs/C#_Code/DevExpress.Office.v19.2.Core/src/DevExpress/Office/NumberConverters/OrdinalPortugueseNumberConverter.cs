namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalPortugueseNumberConverter : OrdinalBasedNumberConverter
    {
        public override string ConvertNumberCore(long value) => 
            $"{value}º";

        protected internal override NumberingFormat Type =>
            NumberingFormat.Ordinal;
    }
}

