namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalGermanNumberConverter : OrdinalBasedNumberConverter
    {
        public override string ConvertNumberCore(long value) => 
            $"{value}.";

        protected internal override NumberingFormat Type =>
            NumberingFormat.Ordinal;
    }
}

