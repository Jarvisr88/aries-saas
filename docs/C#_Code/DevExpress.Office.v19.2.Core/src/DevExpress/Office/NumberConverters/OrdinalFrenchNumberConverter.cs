namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalFrenchNumberConverter : OrdinalBasedNumberConverter
    {
        public override string ConvertNumberCore(long value) => 
            (value != 1L) ? $"{value}e" : $"{value}er";

        protected internal override NumberingFormat Type =>
            NumberingFormat.Ordinal;
    }
}

