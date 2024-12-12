namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalSwedishNumberConverter : OrdinalBasedNumberConverter
    {
        public override string ConvertNumberCore(long value) => 
            (((value % ((long) 10)) == 1L) || ((value % ((long) 10)) == 2L)) ? $"{value}:a" : $"{value}:e";

        protected internal override NumberingFormat Type =>
            NumberingFormat.Ordinal;
    }
}

