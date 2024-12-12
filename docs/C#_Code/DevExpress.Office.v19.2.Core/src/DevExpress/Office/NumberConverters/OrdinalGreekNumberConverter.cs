namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalGreekNumberConverter : OrdinalBasedNumberConverter
    {
        public override string ConvertNumberCore(long value) => 
            $"{value}ο";

        protected internal override NumberingFormat Type =>
            NumberingFormat.Ordinal;
    }
}

