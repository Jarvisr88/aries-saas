namespace DevExpress.Office.NumberConverters
{
    public class OrdinalHindiNumberConverter : DescriptiveOrdinalHindiNumberConverter
    {
        protected internal override NumberingFormat Type =>
            NumberingFormat.Ordinal;
    }
}

