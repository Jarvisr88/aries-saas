namespace DevExpress.Office.NumberConverters
{
    using System;

    public class NumberInDashNumberConverter : OrdinalBasedNumberConverter
    {
        public override string ConvertNumberCore(long value) => 
            $"- {value} -";

        protected internal override NumberingFormat Type =>
            NumberingFormat.NumberInDash;
    }
}

