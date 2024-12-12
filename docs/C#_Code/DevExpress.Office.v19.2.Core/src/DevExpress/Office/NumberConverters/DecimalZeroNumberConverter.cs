namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DecimalZeroNumberConverter : OrdinalBasedNumberConverter
    {
        public override string ConvertNumberCore(long value) => 
            (value >= 10) ? value.ToString() : $"0{value}";

        protected internal override NumberingFormat Type =>
            NumberingFormat.DecimalZero;
    }
}

