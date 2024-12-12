namespace DevExpress.Office.NumberConverters
{
    using System;

    public class DecimalNumberConverter : OrdinalBasedNumberConverter
    {
        public override string ConvertNumberCore(long value) => 
            value.ToString();

        protected internal override NumberingFormat Type =>
            NumberingFormat.Decimal;
    }
}

