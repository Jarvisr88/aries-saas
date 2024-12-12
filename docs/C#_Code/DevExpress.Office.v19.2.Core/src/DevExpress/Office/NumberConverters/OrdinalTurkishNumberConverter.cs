namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalTurkishNumberConverter : OrdinalBasedNumberConverter
    {
        public override string ConvertNumberCore(long value) => 
            $"{value}.";

        protected internal override NumberingFormat Type =>
            NumberingFormat.Ordinal;
    }
}

